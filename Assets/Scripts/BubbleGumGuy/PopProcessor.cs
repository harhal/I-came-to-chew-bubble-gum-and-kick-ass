using Core;
using Enemies;
using UnityEngine;

namespace BubbleGumGuy
{
    public class PopProcessor : MonoBehaviour
    {
        private static readonly int PopTrigger = Animator.StringToHash("Pop");
        private Animator _animator;
        private GridMovement _gridMovement;

        void Awake()
        {
            _animator = GetComponent<Animator>();
            _gridMovement = GetComponent<GridMovement>();
        }

        public void Pop()
        {
            if (_animator)
            {
                _animator.SetTrigger(PopTrigger);
            }
        }

        public void PopHit()
        {
            var hitCell = GridHelper.DirectionToLocation(_gridMovement.gridPosition, _gridMovement.LookAtDirection);

            var victim = _gridMovement.GetNavigation().GetOccupation(hitCell);
            if (!victim)
            {
                //TODO: PlayHitSound
                return;
            }
            
            var victimBubbleGumHitProcessor = victim.GetComponent<BubbleGumHitProcessor>();
            if (victimBubbleGumHitProcessor)
            {
                victimBubbleGumHitProcessor.ReceiveHit();
            }
        }
    }
}
