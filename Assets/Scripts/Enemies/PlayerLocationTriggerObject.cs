using Core;
using Unity.Mathematics;
using UnityEngine;

namespace Enemies
{
    public class PlayerLocationTriggerObject : TriggerObject, IGameStagePipelineItem
    {
        private bool _triggered = false;
        private GridMovement _playerMovement;
        
        [SerializeField]
        private Vector2Int minTriggerArea;
        [SerializeField]
        private Vector2Int maxTriggerArea;

        private void Start()
        {
            GameState.RegisterPipelineItem(this, GameState.GameStage.Spawn);
            _playerMovement = The.Me.GetComponent<GridMovement>();
        }

        public void Trigger()
        {
            var minX = math.min(minTriggerArea.x, maxTriggerArea.x);
            var maxX = math.max(minTriggerArea.x, maxTriggerArea.x);
            var minY = math.min(minTriggerArea.y, maxTriggerArea.y);
            var maxY = math.max(minTriggerArea.y, maxTriggerArea.y);

            var playerPosition = _playerMovement.gridPosition;

            if (playerPosition.x >= minX && playerPosition.y >= minY && playerPosition.x <= maxX &&
                playerPosition.y <= maxY)
            {
                _triggered = true;
                OnTriggered();
            }
        }

        public bool IsAlive()
        {
            return _triggered;
        }

        private void OnDrawGizmosSelected()
        {
            var grids = FindObjectsByType<Grid>(FindObjectsInactive.Exclude, FindObjectsSortMode.None);
            if (grids.Length <= 0)
            {
                return;
            }
            
            var grid = grids[0];
                
            var minX = math.min(minTriggerArea.x, maxTriggerArea.x);
            var maxX = math.max(minTriggerArea.x, maxTriggerArea.x);
            var minY = math.min(minTriggerArea.y, maxTriggerArea.y);
            var maxY = math.max(minTriggerArea.y, maxTriggerArea.y);
            var sizeX = maxX - minX;
            var sizeY = maxY - minY;

            var min = grid.GetCellCenterWorld(new Vector3Int(minX, minY, 0));
            var max = grid.GetCellCenterWorld(new Vector3Int(maxX, maxY, 0));
                
            Gizmos.DrawCube((min + max) / 2,  Vector3.Scale(grid.cellSize, new Vector3(sizeX, sizeY, 1) / 2));
        }
    }
}
