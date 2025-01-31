using System;
using Core;
using UnityEngine;

namespace Enemies
{
    public class KickHitProcessor : MonoBehaviour
    {
        private GridHelper.GridDirection _actionDirection;
        private Animator _animator;
        private GridMovement _gridMovement;
        private CharacterState _characterState;
        private Action _onKickFinished;

        private static readonly int KickedTrigger = Animator.StringToHash("Kicked");

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _gridMovement = GetComponent<GridMovement>();
            _characterState = GetComponent<CharacterState>();
        }

        public void ReceiveHit(GridHelper.GridDirection actionDirection, Action onKickFinished)
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
            var nextCell = GridHelper.DirectionToLocation(_gridMovement.gridPosition, _actionDirection);
            if (_gridMovement.MoveTo(nextCell))
            {
                return;
            }
            
            _characterState.StartDie();

            var collidedWith = _gridMovement.GetNavigation().GetOccupation(nextCell);
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
