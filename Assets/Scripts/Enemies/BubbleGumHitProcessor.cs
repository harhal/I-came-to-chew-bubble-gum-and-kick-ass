using Core;
using UnityEngine;

namespace Enemies
{
    public class BubbleGumHitProcessor : MonoBehaviour, IGameStagePipelineItem
    {
        private Animator _animator;
        private CharacterState _characterState;

        private static readonly int StuckParam = Animator.StringToHash("Stuck");

        private int _LeftHitDuration = 0;

        [SerializeField]
        private int fullHitDuration = 3;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _characterState = GetComponent<CharacterState>();
            GameState.RegisterPipelineItem(this, GameState.GameStage.PrePlayerActions);
        }

        public bool IsStuck()
        {
            return _LeftHitDuration > 0;
        }

        public void ReceiveHit()
        {
            _LeftHitDuration = fullHitDuration;
            if (!_animator)
            {
                return;
            }
        
            _animator.SetBool(StuckParam, true);
        }

        public void Trigger()
        {
            if (_LeftHitDuration == 0)
            {
                GameState.PipelineItemProcessed();
                return;
            }
            
            _LeftHitDuration--;
            if (_LeftHitDuration > 0)
            {
                GameState.PipelineItemProcessed();
                return;
            }
            
            if (!_animator)
            {
                GameState.PipelineItemProcessed();
                return;
            }

            _animator.SetBool(StuckParam, false);
            GameState.PipelineItemProcessed();
        }

        public bool IsAlive()
        {
            return _characterState.IsAlive;
        }
    }
}
