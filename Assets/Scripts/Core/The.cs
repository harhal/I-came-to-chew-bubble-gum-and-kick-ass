using UnityEngine;

namespace Core
{
    public class The : MonoBehaviour
    {
        public static GameObject Me;
        public static GameObject Grid;
        
        [SerializeField]
        public bool theMe;
        
        [SerializeField]
        public bool theGrid;
        
        void Awake()
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
