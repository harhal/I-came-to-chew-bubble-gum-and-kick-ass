using System;
using Core;
using Pickups;
using UnityEngine;
using UnityEngine.Serialization;

namespace BubbleGumGuy
{
    public class Inventory : MonoBehaviour
    {
        private static readonly int HasSgFlag = Animator.StringToHash("HasSG");

        [SerializeField]
        private int bubbleGumCount = 20;
        
        [SerializeField]
        private int bubbleGumMax = 20;
        
        [SerializeField]
        private int shotgunAmmo = 0;
        
        [SerializeField]
        private int shotgunAmmoMax = 3;

        private GridMovement _gridMovement;
        private Animator _animator;
        private CharacterState _characterState;

        public float GetBubbleGumPercentage()
        {
            return (float)bubbleGumCount / (float)bubbleGumMax;
        }

        public int GetAmmo()
        {
            return shotgunAmmo;
        }

        private void Awake()
        {
            _gridMovement = GetComponent<GridMovement>();
            _animator = GetComponent<Animator>();
            _characterState = GetComponent<CharacterState>();
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
                    if (_animator)
                    {
                        _animator.SetBool(HasSgFlag, true);
                    }
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            
            Destroy(pickup.gameObject);
        }

        void SpendBubbleGum()
        {
            bubbleGumCount--;
            if (bubbleGumCount == 0)
            {
                _characterState.StartDie();
            }
        }

        public void SpendAmmo()
        {
            shotgunAmmo--;
            if (shotgunAmmo == 0)
            {
                _animator.SetBool(HasSgFlag, false);
            }
        }
    }
}
