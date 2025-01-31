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
                
                var chargeDirection = path.GetFirstDirection();
                
                int chargeLength = 0;
                foreach (var direction in path.GetDirections())
                {
                    if (direction != chargeDirection)
                    {
                        break;
                    }
                    
                    chargeLength++;
                }

                GetComponent<NinjaAttackProcessor>().leftCharge = chargeLength;
                ActionDecider.SetDesiredAction(ActionDecider.ActionType.Attack, chargeDirection);
            }
            base.Trigger();
        }
    }
}
