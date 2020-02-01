using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

using UnityEditor;
using UnityEngine.UI;

public class Session : MonoBehaviour
{
    private int vie = 5;

    public GameObject player;
    public Canvas exitBackgroundImageCanvasGroup;

    private float fadeDuration = 1f;

    private bool m_IsPlayerAtExit = false;
    private float m_Timer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
     /*   if (other.gameObject == player)
        {
            m_IsPlayerAtExit = true;
        }*/
    }
    // Update is called once per frame
    void Update()
    {
        if (m_IsPlayerAtExit) {
            m_Timer += Time.deltaTime;

             exitBackgroundImageCanvasGroup.enabled = true;


        }
    }
}
