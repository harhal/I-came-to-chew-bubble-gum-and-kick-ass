using System;
using Core;
using UnityEngine;

namespace Enemies
{
    public class KickHitProcessor : MonoBehaviour
    {
        private GridMovement.GridDirection _actionDirection;
        private Animator _animator;
        private GridMovement _gridMovement;
        private CharacterState _characterState;
        private Action _onKickFinished;

        private static readonly int KickedTrigger = Animator.StringToHash("Kicked");
        private static readonly int DirectionParam = Animator.StringToHash("Direction");

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _gridMovement = GetComponent<GridMovement>();
            _characterState = GetComponent<CharacterState>();
        }

        public void ReceiveHit(GridMovement.GridDirection actionDirection, Action onKickFinished)
        {
            if (!_animator || !_gridMovement || !_characterState)
            {
                _onKickFinished.Invoke();
                return;
            }
            
            _onKickFinished = onKickFinished;
            _actionDirection = actionDirection;
            _gridMovement.OrientTo(_actionDirection);
            _animator.SetTrigger(KickedTrigger);
        }

        public void MoveKickedMe()
        {
            var nextCell = _gridMovement.DirectionToLocation(_actionDirection);
            if (_gridMovement.MoveTo(nextCell))
            {
                return;
            }
            
            _characterState.StartDie();

            var collidedWith = GridOccupation.GetOccupation(nextCell);
            if (collidedWith)
            {
                var otherVictimState = collidedWith.GetComponent<CharacterState>();
                if (otherVictimState)
                {
                    otherVictimState.StartDie();
                }
            }
            _onKickFinished.Invoke();
        }

        public void HitFinished()
        {
            _onKickFinished.Invoke();
        }
    }
}
