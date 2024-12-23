using System;
using System.Collections.Generic;
using FoodSystem;
using Helpers;
using UnityEngine;

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

        private void OnEnable()
        {
            AddListeners();
        }

        private void OnDisable()
        {
            RemoveListeners();
        }

        public override void ConfigureSelf(int x, int y)
        {
            head.ConfigureSelf(x, y);
            head.SetLocalPosition(-x, -y);
            var vector = Utilities.GetDirectionVector(_direction);
            tail.ConfigureSelf(x + vector.x, y + vector.y);
            tail.SetLocalPosition(-x + vector.x, -y + vector.y);
            _layer = 0;
            visuals.transform.localPosition += Vector3.up *.6f;
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

            if (newX < 0) newX = gridWidth - 1;
            else if (newX >= gridWidth) newX = 0;
            if (newY < 0) newY = gridHeight - 1;
            else if (newY >= gridHeight) newY = 0;
            
            if (_turnPoints.Count == 0 || _turnPoints.Peek().Direction != _direction)
            {
                _turnPoints.Enqueue(new TurnPoint(new Vector2Int(head.X, head.Y), _direction));
            }
            
            head.MoveTo(newX, newY);
            if (Grid.IsCellHasFood(newX, newY, out Food food))
            {
                food.OnConsume(this);
            }
            
            if (head.NextPart != null)
            {
                head.NextPart.Follow(head, _turnPoints);
            }
        }

        public void Grow()
        {
            var temp = Instantiate(bodyPart, visuals.transform);
            _bodyParts.Insert(1, temp);
            var oldNextPart = head.NextPart;
            head.SetNextPart(temp);
            temp.SetPreviousPart(head);
            temp.SetNextPart(oldNextPart);
            oldNextPart.SetPreviousPart(temp);
            
            temp.SetLayer(Utilities.BlockLayer);
            temp.InjectGrid(Grid);
            var vector = Utilities.GetDirectionVector(_direction);
            temp.ConfigureSelf(head.X + vector.x, head.Y + vector.y);
            temp.SetLocalPosition(-head.X - vector.x, -head.Y - vector.y);
            oldNextPart.SetLocalPosition(temp.X - vector.x, temp.Y - vector.y);
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
            _direction = e.direction;
            head.transform.rotation = Quaternion.Euler(Utilities.GetRotationByDirection(_direction));
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
