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
            if (PlayerGridMovement.GridPosition == GridMovement.DirectionToLocation(GridMovement.LookAtDirection))
            {
                if (GameState.StableRandomGenerator.NextInt(100) < percentToShoot)
                {
                    ActionDecider.SetDesiredAction(ActionDecider.ActionType.Attack);
                    base.Trigger();
                    return;
                }
            }
        
            if ((PlayerGridMovement.GridPosition - GridMovement.GridPosition).magnitude <= _attackProcessor.range)
            {
                if (GameState.StableRandomGenerator.NextInt(100) < percentToShoot)
                {
                    ActionDecider.SetDesiredAction(ActionDecider.ActionType.Attack);
                    base.Trigger();
                    return;
                }
            }

            GridMovement.GridDirection moveDirection = (GridMovement.GridDirection)GameState.StableRandomGenerator.NextInt(4);
            ActionDecider.SetDesiredAction(ActionDecider.ActionType.Move, moveDirection);
            base.Trigger();
        }
    }
}
