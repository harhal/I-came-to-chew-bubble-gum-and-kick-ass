using UnityEngine;

namespace Core
{
    public class The : MonoBehaviour
    {
        public static GameObject Me;
        public static GameObject Grid;
        
        [SerializeField]
        private bool theMe;
        
        [SerializeField]
        private bool theGrid;
        
        void Start()
        {
            if (theMe)
            {
                Me = gameObject;
            }
            
            if (theGrid)
            {
                Grid = gameObject;
            }
        }
    }
}
