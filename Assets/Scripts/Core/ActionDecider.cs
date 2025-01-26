using BubbleGumGuy;
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
        
        private ActionType _action;
        private GridMovement.GridDirection _actionDirection;
        
        [SerializeField]
        private GameState.GameStage actionStage = GameState.GameStage.PostPlayerActions;
        
        private GridMovement _gridMovement;
        private KickProcessor _kickProcessor;
        private PopProcessor _popProcessor;
        private AttackProcessor _attackProcessor;
        
        void Awake()
        {
            GameState.RegisterPipelineItem(this, actionStage);        
            _gridMovement = GetComponent<GridMovement>();
            _kickProcessor = GetComponent<KickProcessor>();
            _popProcessor = GetComponent<PopProcessor>();
            _attackProcessor = GetComponent<AttackProcessor>();
        }

        public GridMovement GetGridMovement()
        {
            return _gridMovement;
        }

        public void Trigger()
        {
            if (!_gridMovement)
            {
                GameState.PipelineItemProcessed();
                return;
            }

            if (_action == ActionType.Move && _kickProcessor)
            {
                if (!GridOccupation.IsFree(_gridMovement.DirectionToLocation(_actionDirection)))
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
                        _attackProcessor.Attack();
                    }
                    break;
                case ActionType.Kick:
                    if (_kickProcessor)
                    {
                        _kickProcessor.Kick(_actionDirection);
                    }
                    break;
                default:
                    GameState.PipelineItemProcessed();
                    break;
            }
        }

        public bool SetDesiredAction(ActionType action, GridMovement.GridDirection direction = GridMovement.GridDirection.East)
        {
            _action = action;
            _actionDirection = direction;
            return true;
        }

        public void ActionFinished()
        {
            GameState.PipelineItemProcessed();
        }
    }
}
