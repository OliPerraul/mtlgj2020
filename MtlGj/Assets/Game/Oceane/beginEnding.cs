using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class beginEnding : MonoBehaviour
{
    // Start is called before the first frame update
   public void SwitchScene(string Scene)
    {
        SceneManager.LoadScene(Scene);
        
       // gameObject.GetComponent<AudioSource>().Play();
    }
    public void quit() {

        Debug.Log("Quit the game");
		Application.Quit();
	}
}
