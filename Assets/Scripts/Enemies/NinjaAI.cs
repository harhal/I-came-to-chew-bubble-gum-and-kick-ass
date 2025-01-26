using Core;

namespace Enemies
{
    public class NinjaAI : DumbAI
    {
        public override void Trigger()
        {
            for (int d = 0; d < 4; d++)
            {
                for (int i = 1; i < 4; i++)
                {
                    GridMovement.GridDirection direction = (GridMovement.GridDirection)d;
                    if (PlayerGridMovement.GridPosition ==
                        GridMovement.DirectionToLocation(direction, i))
                    {
                        ActionDecider.SetDesiredAction(ActionDecider.ActionType.Attack, direction);
                        base.Trigger();
                        return;
                    }
                }
            }

            GridMovement.GridDirection chargeDirection = FindMoveDirection();
            ActionDecider.SetDesiredAction(ActionDecider.ActionType.Attack, chargeDirection);
            base.Trigger();
        }
    }
}
