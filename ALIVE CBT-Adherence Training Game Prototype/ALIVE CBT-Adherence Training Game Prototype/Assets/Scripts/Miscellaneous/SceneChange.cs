using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour {
    
    // Function to change scene
    public void ChangeScene(string SceneName)
    {
        SceneManager.LoadScene(SceneName);
    }

    // Function to Quit Application
    public void QuitApp()
    {
        Application.Quit();
    }
}
