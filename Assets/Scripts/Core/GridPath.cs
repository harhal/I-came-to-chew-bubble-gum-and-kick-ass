using System.Collections.Generic;
using UnityEngine;

namespace Core
{
    public class GridPath
    {
        private readonly List<Vector2Int> _steps;
        public GridPath(Vector2Int?[,] cameFromMatrix, Vector2Int start, Vector2Int end)
        {
            _steps = new List<Vector2Int>();
            var cellCursor = end;
            while (cellCursor != start)
            {
                _steps.Add(cellCursor);
                Debug.Assert(cameFromMatrix[cellCursor.x, cellCursor.y] != null, "cameFromMatrix" + cellCursor.ToString() + " != null");
                cellCursor = cameFromMatrix[cellCursor.x, cellCursor.y].Value;
            }
            _steps.Add(cellCursor);
        }
        
        public IEnumerable<GridHelper.GridDirection> GetDirections()
        {
            Debug.Assert(_steps.Count > 1, "Way is too short");
            
            for (var index = 1; index < _steps.Count; index++)
            {
                var direction = GridHelper.GetDirectionFromTo(_steps[^index], _steps[^(index + 1)]);
                yield return direction;
            }
        }
        
        public GridHelper.GridDirection GetFirstDirection()
        {
            Debug.Assert(_steps.Count > 1, "Way is too short");

            var direction = GridHelper.GetDirectionFromTo(_steps[^1], _steps[^2]);
            return direction;
        }
    }
}