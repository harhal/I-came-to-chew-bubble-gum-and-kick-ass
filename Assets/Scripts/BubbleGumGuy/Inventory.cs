using System;
using Core;
using Pickups;
using UnityEngine;
using UnityEngine.Serialization;

namespace BubbleGumGuy
{
    public class Inventory : MonoBehaviour
    {
        [SerializeField]
        private int bubbleGumCount = 20;
        
        [SerializeField]
        private int bubbleGumMax = 20;
        
        [SerializeField]
        private int shotgunAmmo = 0;
        
        [SerializeField]
        private int shotgunAmmoMax = 3;

        private GridMovement _gridMovement;

        public float GetBubbleGumPercentage()
        {
            return bubbleGumMax / (float)bubbleGumCount;
        }

        private void Awake()
        {
            _gridMovement = GetComponent<GridMovement>();
            ActionDecider actionDecider = GetComponent<ActionDecider>();
            actionDecider.SubscribeOnRelease(CheckPickup);
        }

        void CheckPickup()
        {
            var pickup = Pickup.GetPickupAtLocation(_gridMovement.GetGridPosition());
            if (!pickup)
            {
                return;
            }
            switch (pickup.pickupType)
            {
                case Pickup.Type.Bubblegum:
                    bubbleGumCount = bubbleGumMax;
                    break;
                case Pickup.Type.Shotgun:
                    shotgunAmmo = shotgunAmmoMax;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            
            Destroy(pickup.gameObject);
        }
    }
}
