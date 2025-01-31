using System.Collections.Generic;
using Core;
using UnityEngine;
using UnityEngine.Serialization;

namespace Enemies
{
    public class DumbAI : MonoBehaviour, IGameStagePipelineItem
    {
        protected CharacterState CharacterState;
        protected GridMovement GridMovement;
        protected GridMovement PlayerGridMovement;
        protected ActionDecider ActionDecider;
        
        [SerializeField]
        protected GameState.GameStage pipelineStage = GameState.GameStage.Decisionmaking;

        public virtual void  Awake()
        {
            CharacterState = GetComponent<CharacterState>();
            GridMovement = GetComponent<GridMovement>();
            ActionDecider = GetComponent<ActionDecider>();
            GameState.RegisterPipelineItem(this, pipelineStage);
        }

        public virtual void Start()
        {
            PlayerGridMovement = The.Me.GetComponent<GridMovement>();
        }

        public virtual void Trigger()
        {
            GameState.PipelineItemProcessed();
        }

        protected GridHelper.GridDirection FindRandValidDirection()
        {
            foreach (var direction in GridHelper.FourDirections())
            {
                Vector2Int nextLocation = GridHelper.DirectionToLocation(GridMovement.gridPosition, direction);
                if (GridMovement.GetNavigation().IsFree(nextLocation))
                {
                    return direction;
                }
            }

            return GridHelper.GetRandDirection();
        }

        public bool IsAlive()
        {
            return CharacterState.IsAlive;
        }
    }
}
