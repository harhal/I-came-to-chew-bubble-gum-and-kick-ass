using Core;
using UnityEngine;

namespace Enemies
{
    public class TurnTriggerObject : TriggerObject, IGameStagePipelineItem
    {
        [SerializeField] 
        private int turn = 0;
        
        public void Trigger()
        {
            if (GameState.Turn == turn)
            {
                OnTriggered();
            }
        }

        public bool IsAlive()
        {
            return GameState.Turn <= turn;
        }
    }
}
