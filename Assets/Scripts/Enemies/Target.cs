using Core;
using UnityEngine;

namespace Enemies
{
    public class Target : MonoBehaviour
    {
        private CharacterState _characterState;

        private static int _targetCounter = 0;
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Awake()
        {
            _targetCounter++;
            _characterState = gameObject.GetComponent<CharacterState>();
        }
    
        void Update()
        {
            if (!_characterState.IsAlive)
            {
                _targetCounter--;
                Destroy(this);
                if (_targetCounter == 0)
                {
                    GameState.SetState(GameState.GameStage.AreaCleared);
                }
            }
        }
    }
}
