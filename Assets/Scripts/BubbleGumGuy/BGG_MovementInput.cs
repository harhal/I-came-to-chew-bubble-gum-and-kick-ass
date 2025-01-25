using Core;
using UnityEngine;

namespace BubbleGumGuy
{
    public class BggMovementInput : MonoBehaviour
    {
        private GridMovement _gridMovement;
    
        void Start()
        {
            _gridMovement = GetComponent<GridMovement>();
        }

        void Update()
        {
            if (!_gridMovement)
            {
                return;
            }
            
            if (Input.GetKeyDown(KeyCode.W))
            {
                _gridMovement.Go(GridMovement.GridDirection.Up);
            }
            
            if (Input.GetKeyDown(KeyCode.S))
            {
                _gridMovement.Go(GridMovement.GridDirection.Down);
            }
            
            if (Input.GetKeyDown(KeyCode.D))
            {
                _gridMovement.Go(GridMovement.GridDirection.Right);
            }
            
            if (Input.GetKeyDown(KeyCode.A))
            {
                _gridMovement.Go(GridMovement.GridDirection.Left);
            }
        }
    }
}
