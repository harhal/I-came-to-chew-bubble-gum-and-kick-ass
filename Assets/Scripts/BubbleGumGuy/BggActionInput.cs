using Core;
using UnityEngine;

namespace BubbleGumGuy
{
    public class BggActionInput : MonoBehaviour, IGameStagePipelineItem
    {
        [SerializeField]
        private GameState.GameStage inputStage = GameState.GameStage.Input;

        private ActionDecider _actionDecider;
    
        void Awake()
        {
            _actionDecider = GetComponent<ActionDecider>();
            GameState.RegisterPipelineItem(this, inputStage);
        }

        void Update()
        {
            if (GameState.CurrentGameStage != inputStage)
            {
                return;
            }
            
            if (Input.GetKeyDown(KeyCode.W))
            {
                _actionDecider.SetDesiredAction(ActionDecider.ActionType.Move, GridMovement.GridDirection.North);
                GameState.PipelineItemProcessed();
            }
            
            if (Input.GetKeyDown(KeyCode.S))
            {
                _actionDecider.SetDesiredAction(ActionDecider.ActionType.Move, GridMovement.GridDirection.South);
                GameState.PipelineItemProcessed();
            }
            
            if (Input.GetKeyDown(KeyCode.D))
            {
                _actionDecider.SetDesiredAction(ActionDecider.ActionType.Move, GridMovement.GridDirection.East);
                GameState.PipelineItemProcessed();
            }
            
            if (Input.GetKeyDown(KeyCode.A))
            {
                _actionDecider.SetDesiredAction(ActionDecider.ActionType.Move, GridMovement.GridDirection.West);
                GameState.PipelineItemProcessed();
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                _actionDecider.SetDesiredAction(ActionDecider.ActionType.Attack);
                GameState.PipelineItemProcessed();
            }

            if (Input.GetKeyDown(KeyCode.LeftControl))
            {
                _actionDecider.SetDesiredAction(ActionDecider.ActionType.Pop);
                GameState.PipelineItemProcessed();
            }
        }

        public void Trigger()
        {
        }
    }
}
