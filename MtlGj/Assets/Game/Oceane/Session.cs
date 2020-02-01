using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

using UnityEditor;
using UnityEngine.UI;
using UnityEngine.Tilemaps;

public class Session : MonoBehaviour
{
    private int vie = 5;

    public GameObject player;
    public Canvas exitPanel;

    private float fadeDuration = 1f;

    private bool m_IsPlayerAtExit = false;
    private float m_Timer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter(TilemapCollider2D other)
    {
  

        Debug.Log("that OK00");
       if (other.gameObject == player)
        {
            m_IsPlayerAtExit = true;
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (m_IsPlayerAtExit) {
           Time.timeScale = 0f;

           exitPanel.gameObject.SetActive(true);


        }
    }
}
