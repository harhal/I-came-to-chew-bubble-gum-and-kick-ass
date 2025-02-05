using System;
using UnityEngine;

namespace Enemies
{
    public class TriggerObject : MonoBehaviour
    {
        private event Action Triggered;
    
        public void SubscribeOnTriggered(Action onTriggered)
        {
            Triggered += onTriggered;
        }

        // ReSharper disable Unity.PerformanceAnalysis
        protected void OnTriggered()
        {
            Triggered?.Invoke();
        }
    }
}
