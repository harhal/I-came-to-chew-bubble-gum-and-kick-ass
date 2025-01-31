using Core;

namespace Enemies
{
    public class NinjaAttackProcessor : AttackProcessor
    {
        public override void Attack(GridHelper.GridDirection direction)
        {
            GridMovement.OrientTo(direction);
            base.Attack(direction);
        }

        public override void AttackHit()
        {
            var hitCell = GridHelper.DirectionToLocation(GridMovement.gridPosition, GridMovement.LookAtDirection);

            var victim = GridMovement.GetNavigation().GetOccupation(hitCell);
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
