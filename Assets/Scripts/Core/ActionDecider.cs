using System;
using BubbleGumGuy;
using Enemies;
using UnityEngine;

namespace Core
{
    public class ActionDecider : MonoBehaviour, IGameStagePipelineItem
    {
        public enum ActionType
        {
            Move,
            Attack,
            Pop,
            Kick
        }

        private event Action<ActionType, GridHelper.GridDirection> onActionDecided;
        private event Action onActionReleased;
            
        private ActionType _action;
        private GridHelper.GridDirection _actionDirection;
        
        [SerializeField]
        private GameState.GameStage actionStage = GameState.GameStage.PostPlayerActions;
        
        private GridMovement _gridMovement;
        private KickProcessor _kickProcessor;
        private PopProcessor _popProcessor;
        private AttackProcessor _attackProcessor;
        private CharacterState _characterState;
        private BubbleGumHitProcessor _bubbleGumHitProcessor;


        void Awake()
        {
            GameState.RegisterPipelineItem(this, actionStage);        
            _gridMovement = GetComponent<GridMovement>();
            _kickProcessor = GetComponent<KickProcessor>();
            _popProcessor = GetComponent<PopProcessor>();
            _attackProcessor = GetComponent<AttackProcessor>();
            _characterState = GetComponent<CharacterState>();
            _bubbleGumHitProcessor = GetComponent<BubbleGumHitProcessor>();
        }

        public void SubscribeOnDecision(Action<ActionType, GridHelper.GridDirection> onActionDecided)
        {
            this.onActionDecided += onActionDecided;
        }

        public void SubscribeOnRelease(Action onActionReleased)
        {
            this.onActionReleased += onActionReleased;
        }
        
        public GridMovement GetGridMovement()
        {
            return _gridMovement;
        }

        public void Trigger()
        {
            if (!_gridMovement)
            {
                if (onActionReleased != null)
                {
                    onActionReleased.Invoke();
                }
                GameState.PipelineItemProcessed();
                return;
            }

            if (_bubbleGumHitProcessor && _bubbleGumHitProcessor.IsStuck())
            {
                if (onActionReleased != null)
                {
                    onActionReleased.Invoke();
                }
                GameState.PipelineItemProcessed();
                return;
            }

            if (_action == ActionType.Move && _kickProcessor)
            {
                if (!_gridMovement.GetNavigation().IsFree(GridHelper.DirectionToLocation(_gridMovement.gridPosition, _actionDirection)))
                {
                    _action = ActionType.Kick;
                }
                
            }

            switch (_action)
            {
                case ActionType.Move:
                {
                    if (!_gridMovement.DeferredGo(_actionDirection))
                    {
                        if (_gridMovement.GetNavigation().GetOccupation(GridHelper.DirectionToLocation(_gridMovement.gridPosition, _actionDirection)) == The.Me)
                        {
                            if (_attackProcessor)
                            {
                                _attackProcessor.Attack(_actionDirection);
                                break;
                            }
                        }
                        
                        if (onActionReleased != null)
                        {
                            onActionReleased.Invoke();
                        }
                        GameState.PipelineItemProcessed();
                    }
                }
                    break;
                case ActionType.Pop:
                    if (_popProcessor)
                    {
                        _popProcessor.Pop();
                    }
                    break;
                case ActionType.Attack:
                    if (_attackProcessor)
                    {
                        _attackProcessor.Attack(_actionDirection);
                    }
                    break;
                case ActionType.Kick:
                    if (_kickProcessor)
                    {
                        _kickProcessor.Kick(_actionDirection);
                    }
                    break;
                default:
                    if (onActionReleased != null)
                    {
                        onActionReleased.Invoke();
                    }
                    GameState.PipelineItemProcessed();
                    break;
            }
        }

        public bool SetDesiredAction(ActionType action, GridHelper.GridDirection direction)
        {
            _action = action;
            _actionDirection = direction;
            
            if (onActionDecided != null)
            {
                onActionDecided.Invoke(_action, _actionDirection);
            }
            
            _gridMovement.OrientTo(_actionDirection);
            
            return true;
        }

        public bool SetDesiredAction(ActionType action)
        {
            return SetDesiredAction(action, _gridMovement.LookAtDirection);
        }

        public void ActionFinished()
        {
            if (onActionReleased != null)
            {
                onActionReleased.Invoke();
            }
            GameState.PipelineItemProcessed();
        }

        public bool IsAlive()
        {
            return _characterState.IsAlive;
        }
    }
}
