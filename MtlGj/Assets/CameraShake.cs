using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public static CameraShake Instance;

    // Start is called before the first frame update
    void Awake()
    {
        Instance = this;
    }

    public void Shake()
    {
        iTween.ShakePosition(gameObject, iTween.Hash("y", 0.7f, "time", 1.5f, "delay", 0.0f));
    }
}
