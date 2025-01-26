using Core;
using UnityEngine;

namespace Enemies
{
    public class NinjaAttackProcessor : AttackProcessor
    {
        public override void Attack(GridMovement.GridDirection direction)
        {
            GridMovement.OrientTo(direction);
            base.Attack(direction);
        }

        public override void AttackHit()
        {
            var hitCell = GridMovement.DirectionToLocation(GridMovement.LookAtDirection);

            var victim = GridOccupation.GetOccupation(hitCell);
            if (victim)
            {
                if (victim != The.Me)
                {
                    return;
                }
                
                var myState = victim.GetComponent<CharacterState>();
                if (myState)
                {
                    myState.StartDie();
                }
            }

            GridMovement.MoveTo(hitCell);
        }
    }
}
