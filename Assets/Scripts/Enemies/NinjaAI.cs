using System.Linq;
using Core;
using UnityEngine;

namespace Enemies
{
    public class NinjaAI : DumbAI
    {
        [SerializeField] 
        private int percentToPlayDumb = 20;
        
        // ReSharper disable Unity.PerformanceAnalysis
        public override void Trigger()
        {
            var bPlayDumb = GameState.StableRandomGenerator.NextInt(100) < percentToPlayDumb;
            
            if (bPlayDumb)
            {
                var chargeDirection = FindRandValidDirection();
                GetComponent<NinjaAttackProcessor>().leftCharge = 3;
                ActionDecider.SetDesiredAction(ActionDecider.ActionType.Attack, chargeDirection);
            }
            else
            {
                var path = GridPathBuilder.FindPath(GridMovement.GetNavigation(), GridMovement.gridPosition,
                    PlayerGridMovement.gridPosition, true);
                
                GridHelper.GridDirection chargeDirection;
                int chargeLength = 0;
                
                if (path != null)
                {
                    chargeDirection = path.GetFirstDirection();

                    chargeLength += path.GetDirections().TakeWhile(direction => direction == chargeDirection).Count();
                }
                else
                {
                    chargeDirection = GridHelper.GetRandDirection();
                    chargeLength = 1;
                }

                GetComponent<NinjaAttackProcessor>().leftCharge = chargeLength;
                ActionDecider.SetDesiredAction(ActionDecider.ActionType.Attack, chargeDirection);
            }
            base.Trigger();
        }
    }
}
