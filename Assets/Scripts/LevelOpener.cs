using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelOpener : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }
    
    private bool keyPressed = false;
    void Update()
    {
        if (Input.anyKey)
        {
            SceneManager.LoadScene("SampleScene");
            //keyPressed = true; 
        }
        else
        {
            // put your camera move code here 
        }
    }
}
