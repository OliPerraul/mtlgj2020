using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseScene : MonoBehaviour
{
    // Start is called before the first frame update
    public void pauseSceneTrue(){
                Time.timeScale = 0.0f;
                  //DontDestroyOnLoad(transform.PauseGameLevel);
    }
    public void pauseSceneFalse(){
                Time.timeScale = 1.0f;
                 // SceneManager.LoadScene(newGameLevel);
    }
}
