using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class beginEnding : MonoBehaviour
{

    private bool change = false;
    public AudioSource audioSource;
    private string Scene = "";

   
    // Start is called before the first frame update
    public void SwitchScene(string Scene)
    {
        SoundManagerScript.PlaySound("magical_1");
        Time.timeScale = 1.0f;
        this.Scene = Scene;
        change = true;


        
       // gameObject.GetComponent<AudioSource>().Play();
    }

    public void quit() {

        SoundManagerScript.PlaySound("magical_1");
        Debug.Log("Quit the game");
		Application.Quit();
	}
    void Update(){
        if(change && !audioSource.isPlaying) { 
            SceneManager.LoadScene(Scene);
            change = false;
        }
    }
}
