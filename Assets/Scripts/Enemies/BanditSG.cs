using Core;
using UnityEngine;
using UnityEngine.Serialization;

namespace Enemies
{
    public class BanditSg : MonoBehaviour
    {
        [SerializeField] 
        private bool bHasSg = false;
        
        private static readonly int SgVarName = Animator.StringToHash("HasSG");
        
        void Start()
        {
            var animator = GetComponent<Animator>();
            
            if (animator)
            {
                animator.SetBool(SgVarName, bHasSg);
            }

            var attackProcessor = GetComponent<AttackProcessor>();
            if (attackProcessor && bHasSg)
            {
                attackProcessor.range = 3;
            }
        }
    }
}
