using UnityEngine;

namespace Core
{
    public class GridMovement : BaseGridPlacable
    {
        private static readonly int MoveTrigger = Animator.StringToHash("Move");
        private static readonly int DirectionParam = Animator.StringToHash("Direction");

        private Grid _theGrid;
        private GridNavigation _gridNavigation;
        private Animator _animator;
        private Vector2Int _deferredDestination;
        
        public GridHelper.GridDirection LookAtDirection { get; private set; }

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        private void Start()
        {
            _theGrid = The.Grid.GetComponent<Grid>();
            _gridNavigation = The.Grid.GetComponent<GridNavigation>();
            MoveTo(gridPosition);
        }

        public  GridNavigation GetNavigation()
        {
            return _gridNavigation;
        }

        public bool MoveTo(Vector2Int position)
        {
            if (!_gridNavigation.IsFree(position))
            {
                return false;
            }
            
            var gridPositionV3 = new Vector3Int(position.x, position.y, 0);
            
            var worldLocation = _theGrid.GetCellCenterWorld(gridPositionV3) + 
                                new Vector3(cellOffset.x, cellOffset.y, 0);
            transform.position = worldLocation;
            
            if (!_gridNavigation.IsFree(gridPosition))
            {
                _gridNavigation.Free(gridPosition);
            }
            
            gridPosition = position;
            
            _gridNavigation.Occupy(gridPosition, gameObject);
            
            return true;
        }

        public bool DeferredGo(GridHelper.GridDirection direction, int distance = 1)
        {
            var destLocation = GridHelper.DirectionToLocation(gridPosition, direction, distance);
            
            if (!_gridNavigation.IsFree(destLocation))
            {
                return false;
            }

            _deferredDestination = destLocation;
            
            OrientTo(direction);
            
            if (_animator)
            {
                _animator.SetTrigger(MoveTrigger);
            }

            return true;
        }

        public void OrientTo(GridHelper.GridDirection direction)
        {
            LookAtDirection = direction;
            
            if (_animator)
            {
                _animator.SetInteger(DirectionParam, (int)direction);
            }
        }

        public void ReleasedDeferredMove()
        {
            MoveTo(_deferredDestination);
        }
    }
}
