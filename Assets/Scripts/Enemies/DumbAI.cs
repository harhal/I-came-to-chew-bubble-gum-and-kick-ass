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

        protected GridMovement.GridDirection FindMoveDirection()
        {
            List<GridMovement.GridDirection> sock = new List<GridMovement.GridDirection>
            {
                GridMovement.GridDirection.North,
                GridMovement.GridDirection.South,
                GridMovement.GridDirection.East,
                GridMovement.GridDirection.West
            };

            do
            {
                var direction = sock[GameState.StableRandomGenerator.NextInt(sock.Count)];
                Vector2Int nextLocation = GridMovement.DirectionToLocation(direction);
                if (GridMovement.CanMoveTo(nextLocation))
                {
                    return direction;
                }
            } while (sock.Count > 1);

            return sock[0];
        }

        public bool IsAlive()
        {
            return CharacterState.IsAlive;
        }
    }
}
