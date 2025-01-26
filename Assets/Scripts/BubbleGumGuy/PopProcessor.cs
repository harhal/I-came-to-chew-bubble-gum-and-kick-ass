using UnityEngine;

namespace BubbleGumGuy
{
    public class PopProcessor : MonoBehaviour
    {
        private static readonly int PopTrigger = Animator.StringToHash("Pop");
        private Animator _animator;

        void Awake()
        {
            _animator = GetComponent<Animator>();
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
            
        }
    }
}
