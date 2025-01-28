using System.Collections;
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
                StartCoroutine(LoadNextScene(currentScene.name));
                return;
            }
            if (GameState.CurrentGameStage == GameState.GameStage.AreaCleared)
            {
                GameState.Reset();
                StartCoroutine(LoadNextScene(nextScene));
            }
        }

        IEnumerator LoadNextScene(string sceneName)
        {
            yield return new WaitForSeconds(2f);
            
            SceneManager.LoadScene(nextScene);
        }
    }
}
