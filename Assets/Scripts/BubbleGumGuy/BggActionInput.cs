using System;
using Core;
using UnityEngine;
using UnityEngine.InputSystem;

namespace BubbleGumGuy
{
    public class BggActionInput : MonoBehaviour, IGameStagePipelineItem
    {
        [SerializeField]
        private GameState.GameStage inputStage = GameState.GameStage.Input;

        private ActionDecider _actionDecider;
        private CharacterState _characterState;

        public event Action OnInputStageEntered;

        void Awake()
        {
            _actionDecider = GetComponent<ActionDecider>();
            _characterState = GetComponent<CharacterState>();
            GameState.RegisterPipelineItem(this, inputStage);
        }

        public void OnMove(InputAction.CallbackContext context)
        {
            Vector2 inputDirection = context.ReadValue<Vector2>();
            if (inputDirection.magnitude < 0.1f)
            {
                return;
            }
            
            OnMove(inputDirection);
        }

        public void OnMove(Vector2 inputDirection)
        {
            if (GameState.CurrentGameStage != inputStage)
            {
                return;
            }

            if (Math.Abs(inputDirection.x) > Math.Abs(inputDirection.y))
            {
                if (inputDirection.x > 0)
                {
                    _actionDecider.SetDesiredAction(ActionDecider.ActionType.Move, GridHelper.GridDirection.East);
                    GameState.PipelineItemProcessed();
                }
                else
                {
                    _actionDecider.SetDesiredAction(ActionDecider.ActionType.Move, GridHelper.GridDirection.West);
                    GameState.PipelineItemProcessed();
                }
            }
            else
            {
                if (inputDirection.y > 0)
                {
                    _actionDecider.SetDesiredAction(ActionDecider.ActionType.Move, GridHelper.GridDirection.North);
                    GameState.PipelineItemProcessed();
                }
                else
                {
                    _actionDecider.SetDesiredAction(ActionDecider.ActionType.Move, GridHelper.GridDirection.South);
                    GameState.PipelineItemProcessed();
                }
            }
        }

        public void OnAttack(InputAction.CallbackContext context)
        {
            OnAttack();
        }

        public void OnAttack()
        {
            if (GameState.CurrentGameStage != inputStage)
            {
                return;
            }
            
            _actionDecider.SetDesiredAction(ActionDecider.ActionType.Attack);
            GameState.PipelineItemProcessed();
        }

        public void OnPop(InputAction.CallbackContext context)
        {
            OnPop();
        }

        public void OnPop()
        {
            if (GameState.CurrentGameStage != inputStage)
            {
                return;
            }
            
            _actionDecider.SetDesiredAction(ActionDecider.ActionType.Pop);
            GameState.PipelineItemProcessed();
        }

        public void Trigger()
        {
            OnInputStageEntered?.Invoke();
        }

        public bool IsAlive()
        {
            return _characterState.IsAlive;
        }
    }
}
