using Core;
using UnityEngine;

namespace BubbleGumGuy
{
    public class KickProcessor : MonoBehaviour
    {
        private static readonly int KickTrigger = Animator.StringToHash("Kick");
        private static readonly int Direction = Animator.StringToHash("Direction");
        
        private Animator _animator;

        void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        public void Kick(GridMovement.GridDirection actionDirection)
        {
            if (_animator)
            {
                _animator.SetInteger(Direction, (int)actionDirection);
                _animator.SetTrigger(KickTrigger);
            }
        }

        public void KickHit()
        {
            
        }
    }
}
