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
                    GridHelper.GridDirection direction = (GridHelper.GridDirection)d;
                    if (PlayerGridMovement.gridPosition ==
                        GridHelper.DirectionToLocation(GridMovement.gridPosition, direction, i))
                    {
                        ActionDecider.SetDesiredAction(ActionDecider.ActionType.Attack, direction);
                        base.Trigger();
                        return;
                    }
                }
            }

            GridHelper.GridDirection chargeDirection = FindRandValidDirection();
            ActionDecider.SetDesiredAction(ActionDecider.ActionType.Attack, chargeDirection);
            base.Trigger();
        }
    }
}
