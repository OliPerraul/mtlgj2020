using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManagerScript : MonoBehaviour {

    public static AudioClip GameOver;

    public static AudioClip inventor;

    static AudioSource audioSrc;

    // Use this for initialization
    void Start() {

        GameOver = Resources.Load<AudioClip>("GameOver");

        inventor = Resources.Load<AudioClip>("inventor");

        audioSrc = GetComponent<AudioSource>();

    }


    public static void PlaySound(string clip) {
        switch (clip) {
            case "GameOver":
                audioSrc.PlayOneShot(GameOver);
                break;
            case "inventor":
                audioSrc.PlayOneShot(inventor);
                break;

        }
    }

}
