using BubbleGumGuy;
using UnityEngine;

namespace Core
{
    public class AttackProcessor : MonoBehaviour
    {
        private static readonly int AttackTrigger = Animator.StringToHash("Attack");
        protected Animator Animator;
        protected GridMovement GridMovement;
        private Inventory _inventory;

        public int range = 1;

        void Awake()
        {
            Animator = GetComponent<Animator>();
            GridMovement = GetComponent<GridMovement>();
            _inventory = GetComponent<Inventory>();
        }

        public virtual void Attack(GridHelper.GridDirection direction)
        {
            if (Animator)
            {
                Animator.SetTrigger(AttackTrigger);
            }
        }

        public virtual void AttackHit()
        {
            if (_inventory)
            {
                _inventory.SpendAmmo();
            }
            
            for (var distance = 1; distance <= range; distance++)
            {
                var hitCell = GridHelper.DirectionToLocation(GridMovement.gridPosition, GridMovement.LookAtDirection, distance);

                var victim = GridMovement.GetNavigation().GetOccupation(hitCell);
                if (!victim)
                {
                    //TODO: PlayHitSound
                    continue;
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
