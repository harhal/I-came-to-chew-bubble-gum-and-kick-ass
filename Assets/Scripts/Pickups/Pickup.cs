using System.Collections.Generic;
using Core;
using UnityEngine;
using UnityEngine.Serialization;

namespace Pickups
{
    public class Pickup : BaseGridPlacable
    {
        public enum Type
        {
            Shotgun,
            Bubblegum
        }
        
        private static List<Pickup> _allPickups;

        [SerializeField]
        public Type pickupType;

        void Awake()
        {
            _allPickups.Add(this);
        }

        public static Pickup GetPickupAtLocation(Vector2Int location)
        {
            _allPickups.RemoveAll(pickup => pickup);
            
            foreach (var pickup in _allPickups)
            {
                if (pickup.gridPosition == location)
                {
                    return pickup;
                }
            }
            return null;
        }
    }
}
