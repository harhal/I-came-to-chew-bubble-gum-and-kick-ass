using Enemies;
using UnityEngine;
using UnityEngine.Serialization;

namespace Core
{
    public class AttackProcessor : MonoBehaviour
    {
        private static readonly int AttackTrigger = Animator.StringToHash("Attack");
        private Animator _animator;
        private GridMovement _gridMovement;

        public int range = 1;

        void Awake()
        {
            _animator = GetComponent<Animator>();
            _gridMovement = GetComponent<GridMovement>();
        }

        public void Attack()
        {
            if (_animator)
            {
                _animator.SetTrigger(AttackTrigger);
            }
        }

        public virtual void AttackHit()
        {
            for (int _range = 1; _range <= range; _range++)
            {
                var hitCell = _gridMovement.DirectionToLocation(_gridMovement.LookAtDirection, _range);

                var victim = GridOccupation.GetOccupation(hitCell);
                if (!victim)
                {
                    //TODO: PlayHitSound
                    return;
                }

                var victimCharacterState = victim.GetComponent<CharacterState>();
                if (victimCharacterState)
                {
                    victimCharacterState.StartDie();
                }
            }
        }
    }
}
