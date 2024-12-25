using System;
using System.Collections.Generic;
using FoodSystem;
using Helpers;
using UnityEngine;
using UnityEngine.SceneManagement;
using AudioType = Helpers.AudioType;

namespace SnakeSystem
{
    public class Snake : BaseTile
    {
        [SerializeField] private GameObject visuals;
        [SerializeField] private SnakeBodyPart head;
        [SerializeField] private SnakeBodyPart tail;
        [SerializeField] private SnakeBodyPart bodyPart;

        private Queue<TurnPoint> _turnPoints = new Queue<TurnPoint>();
        private Direction _direction = Direction.Up;
        private List<SnakeBodyPart> _bodyParts;

        private bool EditingLevel => SceneManager.GetActiveScene().name == "LevelEditor";

        private void OnEnable()
        {
            AddListeners();
        }

        private void OnDisable()
        {
            RemoveListeners();
        }
        
        public override void InjectGrid(Grid grid)
        {
            Grid = grid;
        }

        public override void ConfigureSelf(int x, int y)
        {
            head.ConfigureSelf(x, y);
            head.SetLocalPosition(-x, -y);
            var vector = Utilities.GetDirectionVector(_direction);
            tail.ConfigureSelf(UpdateXIfOutOfEdge(x - vector.x, Grid.Width), UpdateYIfOutOfEdge(y - vector.y, Grid.Height));
            tail.SetLocalPosition(-x - vector.x, -y - vector.y);
            if(!EditingLevel) tail.gameObject.SetActive(true);
            _layer = 0;
        }

        public void Initialize()
        {
            _bodyParts = new List<SnakeBodyPart>();
            _bodyParts.Add(head);
            _bodyParts.Add(tail);
            head.SetNextPart(tail);
            tail.SetPreviousPart(head);

            foreach (var part in _bodyParts)
            {
                part.InjectGrid(Grid);
            }
        }

        public void Move()
        {
            Vector2Int moveDirection = Utilities.GetDirectionVector(_direction);
            int newX = head.X + moveDirection.x;
            int newY = head.Y + moveDirection.y;

            int gridWidth = Grid.Width;
            int gridHeight = Grid.Height;
            newX = UpdateXIfOutOfEdge(newX, gridWidth);
            newY = UpdateYIfOutOfEdge(newY, gridHeight);
            AudioManager.Instance.PlayClip(AudioType.Move);

            if (Grid.HasCrashed(newX, newY))
            {
                AudioManager.Instance.PlayClip(AudioType.Fail);
                EventBus.Instance.Trigger(new OnGameOver(false));
                return;
            }

            head.MoveTo(newX, newY, _direction, 0);

            if (Grid.IsCellHasFood(newX, newY, out Food food))
            {
                AudioManager.Instance.PlayClip(AudioType.IncreaseLevel);
                food.OnConsume(this);
            }

            if (head.NextPart != null)
            {
                head.NextPart.Follow(head, _turnPoints, _direction, _bodyParts.FindIndex(x=>  x == head.NextPart));
            }
        }
        
        public void Grow()
        {
            var newPart = Instantiate(bodyPart, visuals.transform);
            _bodyParts.Insert(_bodyParts.Count-2, newPart);
            var oldPreviousPart = tail.PreviousPart;
            oldPreviousPart.SetNextPart(newPart);
            newPart.SetPreviousPart(oldPreviousPart);
            newPart.SetNextPart(tail);
            tail.SetPreviousPart(newPart);
            
            newPart.SetLayer(Utilities.BlockLayer);
            newPart.InjectGrid(Grid);
            newPart.ConfigureSelf(tail.X, tail.Y);
            var tailLocalPosition = tail.transform.localPosition;
            newPart.transform.localPosition = tailLocalPosition;
            var directionVector = Utilities.GetDirectionVector(_direction);
            //todo: update coordinate for tail
            tail.ConfigureSelf(newPart.X - directionVector.x, newPart.Y - directionVector.y);
            tail.transform.localPosition = newPart.transform.localPosition;
        }
        
        public void SetDirection(Direction direction)
        {
            _direction = direction;
            SetRotation();
        }
    
        public void UpdateDirection()
        {
            _direction = (Direction)(((int)_direction + 1) % Enum.GetValues(typeof(Direction)).Length);
            SetRotation();
        }

        private void SetRotation()
        {
            visuals.transform.localRotation = Quaternion.Euler(Utilities.GetRotationByDirection(_direction));
        }

        private void HandleOnDirectionChanged(OnDirectionUpdated e)
        {
            if (_direction == e.direction || Utilities.IsOppositeDirection(_direction, e.direction)) return;
            _direction = e.direction;
            head.transform.rotation = Quaternion.Euler(Utilities.GetRotationByDirection(_direction));
            var turnPoint = new TurnPoint(new Vector2Int(head.X, head.Y), _direction);
            _turnPoints.Enqueue(turnPoint);
        }
        
        public override SaveData CreateTileData()
        {
            var data = new SnakeData
            {
                x = _x,
                y = _y,
                initialDirection = _direction
            };
            return data;
        }

        private int UpdateXIfOutOfEdge(int newX, int gridWidth)
        {
            if (newX < 0) newX = gridWidth - 1;
            else if (newX >= gridWidth) newX = 0;
            return newX;
        }
        
        private int UpdateYIfOutOfEdge(int newY, int gridHeight)
        {
            if (newY < 0) newY = gridHeight - 1;
            else if (newY >= gridHeight) newY = 0;
            return newY;
        }
        
        private void AddListeners()
        {
            EventBus.Instance.Register<OnDirectionUpdated>(HandleOnDirectionChanged);
        }

        private void RemoveListeners()
        {
            EventBus.Instance.Unregister<OnDirectionUpdated>(HandleOnDirectionChanged);
        }
    }
}
