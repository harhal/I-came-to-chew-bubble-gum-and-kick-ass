using Core;
using Enemies;
using UnityEngine;

namespace BubbleGumGuy
{
    public class KickProcessor : MonoBehaviour
    {
        private static readonly int KickTrigger = Animator.StringToHash("Kick");
        private static readonly int DirectionParam = Animator.StringToHash("Direction");
        
        private Animator _animator;
        private GridMovement _gridMovement;
        private GridMovement.GridDirection _actionDirection;

        private int _activeAnimations = 0;

        void Awake()
        {
            _animator = GetComponent<Animator>();
            _gridMovement = GetComponent<GridMovement>();
        }

        public void Kick(GridMovement.GridDirection actionDirection)
        {
            _actionDirection = actionDirection;
            if (_animator)
            {
                _gridMovement.OrientTo(_actionDirection);
                _animator.SetTrigger(KickTrigger);
                _activeAnimations++;
            }
        }

        public void KickHit()
        {
            var hitCell = _gridMovement.DirectionToLocation(_actionDirection);

            var victim = GridOccupation.GetOccupation(hitCell);
            if (!victim)
            {
                //TODO: PlayHitSound
                return;
            }
            
            var victimKickHitProcessor = victim.GetComponent<KickHitProcessor>();
            if (!victimKickHitProcessor)
            {
                var victimCharacterState = victim.GetComponent<CharacterState>();
                if (victimCharacterState)
                {
                    victimCharacterState.StartDie();
                }
                return;
            }

            _activeAnimations++;
            victimKickHitProcessor.ReceiveHit(_actionDirection, OnAnimationFinished);
        }

        void OnAnimationFinished()
        {
            _activeAnimations--;
            if (_activeAnimations == 0)
            {
                GameState.PipelineItemProcessed();
            }
        }
    }
}
