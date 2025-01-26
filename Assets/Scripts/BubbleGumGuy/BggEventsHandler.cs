using Core;
using UnityEngine;

namespace BubbleGumGuy
{
    public class BggEventsHandler : MonoBehaviour
    {
        private void OnCharacterSpawned()
        {
            GameState.SetState(GameState.GameStage.Decisionmaking);
        }
    }
}
