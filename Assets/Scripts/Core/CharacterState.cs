using UnityEngine;

namespace Core
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
            
            var gridMovement = GetComponent<GridMovement>();
            if (gridMovement)
            {
                GridOccupation.Free(gridMovement.gridPosition);
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
