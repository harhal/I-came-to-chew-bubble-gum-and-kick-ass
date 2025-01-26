using Enemies;
using UnityEngine;
using UnityEngine.Serialization;

namespace Core
{
    public class AttackProcessor : MonoBehaviour
    {
        private static readonly int AttackTrigger = Animator.StringToHash("Attack");
        protected Animator Animator;
        protected GridMovement GridMovement;

        public int range = 1;

        void Awake()
        {
            Animator = GetComponent<Animator>();
            GridMovement = GetComponent<GridMovement>();
        }

        public virtual void Attack(GridMovement.GridDirection direction)
        {
            if (Animator)
            {
                Animator.SetTrigger(AttackTrigger);
            }
        }

        public virtual void AttackHit()
        {
            for (var distance = 1; distance <= range; distance++)
            {
                var hitCell = GridMovement.DirectionToLocation(GridMovement.LookAtDirection, distance);

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
