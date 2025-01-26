using Core;
using UnityEngine;

namespace Enemies
{
    public class CharacterState : MonoBehaviour
    {
        [SerializeField]
        bool bDestroyOnDeath = true;

        public bool IsAlive { get; private set; } = true;

        private static readonly int DeathTrigger = Animator.StringToHash("Death");

        public void StartDie()
        {
            IsAlive = false;
            
            var animator = GetComponent<Animator>();
            
            if (animator)
            {
                animator.SetTrigger(DeathTrigger);
            }
        }

        public void Decay()
        {
            if (bDestroyOnDeath)
            {
                Destroy(gameObject);
            }
        }
    }
}
