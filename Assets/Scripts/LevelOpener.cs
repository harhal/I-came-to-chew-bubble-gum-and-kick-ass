using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelOpener : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }
    
    void Update()
    {
        if (Input.anyKey)
        {
            SceneManager.LoadScene("Lvl1");
        }
        else
        {
        }
    }
}
