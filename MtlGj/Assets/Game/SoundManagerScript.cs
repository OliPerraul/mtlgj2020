using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManagerScript : MonoBehaviour {

    public static AudioClip GameOver;

    public static AudioClip inventor;

    public static AudioClip magical;

    static AudioSource audioSrc;

    // Use this for initialization
    void Start() {

        GameOver = Resources.Load<AudioClip>("GameOver");

        inventor = Resources.Load<AudioClip>("inventor");

        magical = Resources.Load<AudioClip>("magical_1");

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
            case "magical_1":
                audioSrc.PlayOneShot(magical);
                break;

        }
    }

}
