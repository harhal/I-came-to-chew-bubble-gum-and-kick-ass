using System;
using Core;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class GameStageDebugger : MonoBehaviour
{
    [SerializeField]
    private TMP_Text textMesh;
    private void OnValidate()
    {
        if (!textMesh)
        {
            textMesh = GetComponent<TMP_Text>();
        }
    }

    void Update()
    {
        textMesh.SetText(GameState.CurrentGameStage.ToString());
    }
}
