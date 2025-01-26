using Core;
using UnityEngine;

namespace Enemies
{
    public class BanditAI : DumbAI
    {
        private AttackProcessor _attackProcessor;

        [SerializeField] 
        private int percentToShoot = 60;

        public override void Awake()
        {
            base.Awake();
            _attackProcessor = GetComponent<AttackProcessor>();
        }

        public override void Trigger()
        {
            if (PlayerGridMovement.gridPosition == GridMovement.DirectionToLocation(GridMovement.LookAtDirection))
            {
                ActionDecider.SetDesiredAction(ActionDecider.ActionType.Attack);
                base.Trigger();
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

            GridMovement.GridDirection moveDirection = FindMoveDirection();
            ActionDecider.SetDesiredAction(ActionDecider.ActionType.Move, moveDirection);
            base.Trigger();
        }
    }
}
