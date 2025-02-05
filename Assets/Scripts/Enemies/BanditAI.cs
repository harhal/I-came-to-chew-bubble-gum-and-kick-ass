using Core;
using UnityEngine;

namespace Enemies
{
    public class BanditAI : DumbAI
    {
        private AttackProcessor _attackProcessor;

        [SerializeField] private int percentToShoot = 60;

        [SerializeField] private int percentToPlayDumb = 20;

        public override void Awake()
        {
            base.Awake();
            _attackProcessor = GetComponent<AttackProcessor>();
        }

        public override void Trigger()
        {
            for (int distance = 1; distance <= _attackProcessor.range; distance++)
            {
                if (PlayerGridMovement.gridPosition ==
                    GridHelper.DirectionToLocation(GridMovement.gridPosition, GridMovement.LookAtDirection, distance))
                {
                    ActionDecider.SetDesiredAction(ActionDecider.ActionType.Attack);
                    base.Trigger();
                    return;
                }
            }

            if ((PlayerGridMovement.gridPosition - GridMovement.gridPosition).magnitude <= _attackProcessor.range)
            {
                if (GameState.StableRandomGenerator.NextInt(100) < percentToShoot)
                {
                    ActionDecider.SetDesiredAction(ActionDecider.ActionType.Attack);
                    base.Trigger();
                    return;
                }
            }
            
            var bPlayDumb = GameState.StableRandomGenerator.NextInt(100) < percentToPlayDumb;

            var path = GridPathBuilder.FindPath(GridMovement.GetNavigation(), GridMovement.gridPosition,
                PlayerGridMovement.gridPosition, true);
                
            var moveDirection = (bPlayDumb || path == null)
                ? FindRandValidDirection()
                : path.GetFirstDirection();

            ActionDecider.SetDesiredAction(ActionDecider.ActionType.Move, moveDirection);
            base.Trigger();
        }
    }
}
