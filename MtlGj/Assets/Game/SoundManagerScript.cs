using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManagerScript : MonoBehaviour {

    public static AudioClip demonDeath;

    static AudioSource audioSrc;

    // Use this for initialization
    void Start() {

        demonDeath = Resources.Load<AudioClip>("demonDeath");

        audioSrc = GetComponent<AudioSource>();

    }


    public static void PlaySound(string clip) {
        switch (clip) {
            case "demonDeath":
                audioSrc.PlayOneShot(demonDeath);
                break;
          
        }
    }

}
