using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace MTLGJ
{

    [System.Serializable]
    public class SessionSettings
    {
        [SerializeField]
        public int MaxLives = 10;
    };

    public class Session
    {
        [HideInInspector]
        public int Score = 0;

        [HideInInspector]
        public int Money = 0;

        [SerializeField]
        public int Lives = 10;

        public Session()
        {
            Lives = Game.Instance.SessionSettings.MaxLives;
        }
    };


    public class Game : MonoBehaviour
    {
        public static Game Instance;

        [SerializeField]
        public SessionSettings SessionSettings;
        
        public Session Session;

        private void Awake()
        {
            Instance = this;
        }
    }
}