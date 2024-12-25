using System.Collections.Generic;
using DG.Tweening;
using Helpers;
using UnityEngine;
using UnityEngine.Serialization;

namespace SnakeSystem
{
    public class SnakePart : BaseTile
    {
        public SnakeBodyPart NextPart { get; private set; }
        public SnakeBodyPart PreviousPart { get; private set; }
        
        [FormerlySerializedAs("_previousX")] public int previousX;
        [FormerlySerializedAs("_previousY")] public int previousY;

        public void SetNextPart(SnakeBodyPart nextPart)
        {
            NextPart = nextPart;
        }

        public void SetPreviousPart(SnakeBodyPart previousPart)
        {
            PreviousPart = previousPart;
        }

        public void MoveTo(int newX, int newY, Direction direction, int bodyIndex)
        {
            previousX = X;
            previousY = Y;
            Grid.ClearTileOfParentCell(this);
            SetXCoordinate(newX);
            SetYCoordinate(newY);
            Grid.PlaceTileToParentCell(this);
            
            var target = Grid.GetCellTargetByCoordinate(newX, newY);
            transform.DOMove(target.position, Utilities.Tick / 4f).SetEase(Ease.Linear);
        }
        
        public virtual void Follow(SnakePart leader, Queue<TurnPoint> turnPoints, Direction direction, int bodyIndex)
        {
        }
    }
}