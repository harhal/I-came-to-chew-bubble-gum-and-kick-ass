using System;
using BubbleGumGuy;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.Tilemaps;

namespace Core
{
    public class GridMovement : MonoBehaviour
    {
        private static readonly int MoveTrigger = Animator.StringToHash("Move");
        private static readonly int DirectionParam = Animator.StringToHash("Direction");

        public enum GridDirection
        {
            East,
            South,
            North,
            West
        }
        
        private Grid _theGrid;
        private Tilemap _tilemap;
        private Animator _animator;
        private Vector2Int _deferredDestination;
        
        [SerializeField]
        private Vector2Int startGridPosition;
        
        [SerializeField]
        private Vector2 cellOffset;

        [SerializeField]
        public Vector2Int GridPosition;
        public GridDirection LookAtDirection { get; private set; }

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        private void Start()
        {
            _theGrid = The.Grid.GetComponent<Grid>();
            _tilemap = The.Grid.GetComponentInChildren<Tilemap>();
            MoveTo(startGridPosition);
        }
        
        public Vector2Int DirectionToLocation(GridDirection direction, int distance = 1)
        {
            switch (direction)
            {
                case GridDirection.North: 
                    return new Vector2Int(GridPosition.x, GridPosition.y + distance);
                case GridDirection.South:
                    return  new Vector2Int(GridPosition.x, GridPosition.y - distance);
                case GridDirection.East:
                    return  new Vector2Int(GridPosition.x + distance, GridPosition.y);
                case GridDirection.West:
                    return  new Vector2Int(GridPosition.x - distance, GridPosition.y);
                default:
                    return new Vector2Int();
            }
        }

        bool CanMoveTo(Vector2Int position)
        {
            if (!GridOccupation.IsFree(position))
            {
                return false;
            }
            
            var gridPositionV3 = new Vector3Int(position.x, position.y, 0);
            
            TileBase tile = _tilemap.GetTile(gridPositionV3);
            TileData tileData = default;
            tile.GetTileData(gridPositionV3, _tilemap, ref tileData);
            if (tileData.colliderType == Tile.ColliderType.Grid)
            {
                return false;
            }
            
            return true;
        }

        public bool MoveTo(Vector2Int position)
        {
            if (!CanMoveTo(position))
            {
                return false;
            }
            
            var gridPositionV3 = new Vector3Int(position.x, position.y, 0);
            
            var worldLocation = _theGrid.GetCellCenterWorld(gridPositionV3) + 
                                new Vector3(cellOffset.x, cellOffset.y, 0);
            transform.position = worldLocation;
            
            if (!GridOccupation.IsFree(GridPosition))
            {
                GridOccupation.Free(GridPosition);
            }
            
            GridPosition = position;
            
            GridOccupation.Occupy(GridPosition, gameObject);
            
            return true;
        }

        public bool DeferredGo(GridDirection direction, int distance = 1)
        {
            var destLocation = DirectionToLocation(direction, distance);
            
            if (!CanMoveTo(destLocation))
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

        public void OrientTo(GridDirection direction)
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
