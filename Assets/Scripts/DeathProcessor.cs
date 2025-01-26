using Core;
using UnityEngine;

public class DeathProcessor : MonoBehaviour
{
    public void OnDeath()
    {
        GameState.SetState(GameState.GameStage.CharacterDead);
    }

}
