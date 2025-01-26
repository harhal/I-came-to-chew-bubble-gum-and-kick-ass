using Core;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

namespace BubbleGumGuy
{
    public class BggEventsHandler : MonoBehaviour
    {
        [SerializeField] 
        private string nextScene = "Lvl2";
        
        private void OnCharacterSpawned()
        {
            GameState.SetState(GameState.GameStage.Decisionmaking);
        }

        void Update()
        {
            if (GameState.CurrentGameStage == GameState.GameStage.CharacterDead)
            {
                GameState.Reset();
                Scene currentScene = SceneManager.GetActiveScene();
                SceneManager.LoadScene(currentScene.name);
                return;
            }
            if (GameState.CurrentGameStage == GameState.GameStage.AreaCleared)
            {
                GameState.Reset();
                SceneManager.LoadScene(nextScene);
            }
        }
    }
}
