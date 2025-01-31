using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

namespace Core
{
    public static class GridHelper
    {
        public enum GridDirection
        {
            East,
            South,
            North,
            West
        }
        
        public static Vector2Int DirectionToLocation(Vector2Int from, GridDirection direction, int distance = 1)
        {
            switch (direction)
            {
                case GridDirection.North: 
                    return new Vector2Int(from.x, from.y + distance);
                case GridDirection.South:
                    return  new Vector2Int(from.x, from.y - distance);
                case GridDirection.East:
                    return  new Vector2Int(from.x + distance, from.y);
                case GridDirection.West:
                    return  new Vector2Int(from.x - distance, from.y);
                default:
                    return new Vector2Int();
            }
        }

        public static GridDirection GetDirectionFromTo(Vector2Int from, Vector2Int to)
        {
            var delta = to - from;

            var absX = Mathf.Abs(delta.x);
            var absY = Mathf.Abs(delta.y);

            if (absY > absX)
            {
                return delta.y > 0 ? GridDirection.North : GridDirection.South;
            }
            else
            {
                return delta.x > 0 ? GridDirection.East : GridDirection.West;
            }
        }

        public static IEnumerable<GridDirection> FourDirections(bool bRandomOrder = false)
        {
            if (bRandomOrder)
            {
                var fourDirections = new List<GridDirection>
                {
                    GridDirection.East,
                    GridDirection.South,
                    GridDirection.North,
                    GridDirection.West
                };

                while (fourDirections.Count > 0)
                {
                    var randIdx = GameState.StableRandomGenerator.NextInt(fourDirections.Count);
                    var direction = fourDirections[randIdx];
                    fourDirections.RemoveAtSwapBack(randIdx);
                    
                    yield return direction;
                }
            }
            else
            {
                yield return GridDirection.North;
                yield return GridDirection.South;
                yield return GridDirection.East;
                yield return GridDirection.West;
            }
        }

        public static GridDirection GetRandDirection()
        {
            return (GridDirection)GameState.StableRandomGenerator.NextInt(4);
        }
    }
}