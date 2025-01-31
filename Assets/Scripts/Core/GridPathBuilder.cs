using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Debug = System.Diagnostics.Debug;

namespace Core
{
    public class GridPathBuilder
    {
        private const int FrameBudget = 100;

        public static IEnumerator PathfinderCoroutine(GridNavigation gridNavigation, Vector2Int start, Vector2Int end,
            Action<GridPath> onPathComputed, bool bRandomizeDirections = false)
        {
            if (start == end)
            {
                yield break;
            }
            
            var cameFrom = new Vector2Int?[gridNavigation!.GetGridSize().x, gridNavigation!.GetGridSize().y];
            var processQueue = new Queue<Vector2Int>();
            processQueue.Enqueue(start);
            Vector2Int? cellCursor = null;
            var budgetCounter = FrameBudget;
            
            while (cellCursor != end)
            {
                if (processQueue.Count == 0)
                {
                    yield break;
                }
                
                cellCursor = processQueue.Dequeue();
                foreach (var direction in GridHelper.FourDirections(bRandomizeDirections))
                {
                    var nextCell = GridHelper.DirectionToLocation(cellCursor.Value, direction);

                    if (nextCell == end)
                    {
                        cameFrom[nextCell.x, nextCell.y] = cellCursor;
                        onPathComputed.Invoke(new GridPath(cameFrom, start, end));
                        yield break;
                    }

                    if (cameFrom[nextCell.x, nextCell.y] != null)
                    {
                        continue;
                    }

                    if (!gridNavigation.IsFree(nextCell))
                    {
                        continue;
                    }

                    cameFrom[nextCell.x, nextCell.y] = cellCursor;
                }

                budgetCounter--;
                if (budgetCounter == 0)
                {
                    yield return null;
                }
            }
        }

        public static GridPath FindPath(GridNavigation gridNavigation, Vector2Int start, Vector2Int end,
        bool bRandomizeDirections = false)
        {
            if (start == end)
            {
                return null;
            }
            
            var cameFrom = new Vector2Int?[gridNavigation!.GetGridSize().x, gridNavigation!.GetGridSize().y];
            var processQueue = new Queue<Vector2Int>();
            processQueue.Enqueue(start);
            Vector2Int? cellCursor = null;
            
            while (cellCursor != end)
            {
                if (processQueue.Count == 0)
                {
                    return null;
                }
                
                cellCursor = processQueue.Dequeue();
                foreach (var direction in GridHelper.FourDirections(bRandomizeDirections))
                {
                    var nextCell = GridHelper.DirectionToLocation(cellCursor.Value, direction);

                    if (!gridNavigation.IsValidLocation(nextCell))
                    {
                        continue;
                    }

                    if (nextCell == end)
                    {
                        cameFrom[nextCell.x, nextCell.y] = cellCursor;
                        return new GridPath(cameFrom, start, end);
                    }

                    if (cameFrom[nextCell.x, nextCell.y] != null)
                    {
                        continue;
                    }

                    if (!gridNavigation.IsFree(nextCell))
                    {
                        continue;
                    }

                    cameFrom[nextCell.x, nextCell.y] = cellCursor;
                    processQueue.Enqueue(nextCell);
                }
            }
            
            return null;
        }
    }
}