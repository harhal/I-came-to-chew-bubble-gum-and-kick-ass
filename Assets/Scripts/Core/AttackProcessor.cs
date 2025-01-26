using UnityEngine;

namespace Core
{
    public class AttackProcessor : MonoBehaviour
    {
        private static readonly int AttackTrigger = Animator.StringToHash("Attack");
        private Animator _animator;

        void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        public void Attack()
        {
            if (_animator)
            {
                _animator.SetTrigger(AttackTrigger);
            }
        }

        public virtual void AttackHit()
        {
        
        }
    }
}
