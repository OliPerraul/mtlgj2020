using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManagerScript : MonoBehaviour {

    public static AudioClip GameOver;

    public static AudioClip inventor;

    public static AudioClip magical;

    public static AudioClip craft;

    public static AudioClip update;

    static AudioSource audioSrc;

    // Use this for initialization
    void Start() {

        GameOver = Resources.Load<AudioClip>("GameOver");

        inventor = Resources.Load<AudioClip>("inventor");

        magical = Resources.Load<AudioClip>("magical_1");

        craft = Resources.Load<AudioClip>("Hammer");

        update = Resources.Load<AudioClip>("update");

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
            case "Hammer":
                audioSrc.PlayOneShot(craft);
                break;
            case "update":
                audioSrc.PlayOneShot(update);
                break;

        }
    }

}
