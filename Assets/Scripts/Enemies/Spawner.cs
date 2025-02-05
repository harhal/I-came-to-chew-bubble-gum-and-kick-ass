using UnityEngine;

namespace Enemies
{
    public class Spawner : MonoBehaviour
    {
        private static readonly int SpawnTrigger = Animator.StringToHash("Spawn");

        [SerializeField]
        private TriggerObject spawnTrigger;

        [SerializeField]
        private GameObject[] objectsToSpawn;
    
        private void OnValidate()
        {
            if (!spawnTrigger)
            {
                spawnTrigger = GetComponent<TriggerObject>();
            }
        }

        private void Start()
        {
            if (spawnTrigger)
            {
                foreach (var obj in objectsToSpawn)
                {
                    obj.SetActive(false);
                }
                
                spawnTrigger.SubscribeOnTriggered(() =>
                {
                    foreach (var obj in objectsToSpawn)
                    {
                        obj.SetActive(true);
                    }
                });
            }
        }
    }
}
