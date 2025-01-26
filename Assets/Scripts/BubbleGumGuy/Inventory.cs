using UnityEngine;

namespace BubbleGumGuy
{
    public class Inventory : MonoBehaviour
    {
        [SerializeField]
        private int BubbleGumCount = 20;
        
        [SerializeField]
        private int BubbleGumMax = 20;
        
        [SerializeField]
        private int shotgunAmmo = 0;

        public float GetBubbleGumPercentage()
        {
            return BubbleGumMax / (float)BubbleGumCount;
        }
    }
}
