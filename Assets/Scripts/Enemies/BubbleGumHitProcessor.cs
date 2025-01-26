using Core;
using UnityEngine;

namespace Enemies
{
    public class BubbleGumHitProcessor : MonoBehaviour, IGameStagePipelineItem
    {
        private Animator _animator;
        private CharacterState _characterState;

        private static readonly int StuckParam = Animator.StringToHash("Stuck");

        private bool _isStuck;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _characterState = GetComponent<CharacterState>();
            GameState.RegisterPipelineItem(this, GameState.GameStage.PrePlayerActions);
        }

        public bool IsStuck()
        {
            return _isStuck;
        }

        public void ReceiveHit()
        {
            _isStuck = true;
            if (!_animator)
            {
                return;
            }
        
            _animator.SetBool(StuckParam, _isStuck);
        }

        public void Trigger()
        {
            if (!_isStuck)
            {
                GameState.PipelineItemProcessed();
                return;
            }
            
            _isStuck = false;
            if (!_animator)
            {
                GameState.PipelineItemProcessed();
                return;
            }

            _animator.SetBool(StuckParam, _isStuck);
            GameState.PipelineItemProcessed();
        }

        public bool IsAlive()
        {
            return _characterState.IsAlive;
        }
    }
}
