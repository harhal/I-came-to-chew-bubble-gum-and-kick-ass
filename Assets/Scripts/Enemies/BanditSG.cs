using UnityEngine;
using UnityEngine.Serialization;

namespace Enemies
{
    public class BanditSG : MonoBehaviour
    {
        [SerializeField] 
        private bool bHasSg = false;
        
        private static readonly int SgVarName = Animator.StringToHash("HasSG");
        
        void Start()
        {
            var animator = GetComponent<Animator>();
            
            if (animator)
            {
                animator.SetTrigger(SgVarName);
            }
        }
    }
}
