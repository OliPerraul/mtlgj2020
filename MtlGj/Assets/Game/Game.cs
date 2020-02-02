using Cirrus.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


namespace MTLGJ
{
    public class Session
    {
        public GameObject canvas; 
        [SerializeField]
        public Cirrus.Events.ObservableInt Lives = new Cirrus.Events.ObservableInt();

        public Cirrus.Events.ObservableInt ResourcesAmount = new Cirrus.Events.ObservableInt();

        public Cirrus.Events.ObservableInt WaveIndex = new Cirrus.Events.ObservableInt();

        public Wave Wave => GameResources.Instance.SessionSettings.Waves[
            (WaveIndex.Value-1).Mod(
                GameResources.Instance.SessionSettings.Waves.Count)];  

        public Cirrus.Events.ObservableInt RemainingInWave = new Cirrus.Events.ObservableInt();

        public float Difficulty => 1f + (((float)WaveIndex.Value) / GameResources.Instance.SessionSettings.Waves.Count);

        public Session()
        {
            Lives.Value = GameResources.Instance.SessionSettings.MaxLives;
            Lives.OnValueChangedHandler += OnLivedChanged;
            ResourcesAmount.Value = GameResources.Instance.SessionSettings.InitialResourcesAmount;
            Level.Instance.OnEnemyRemovedHandler += OnEneemyDied;
            WaveIndex.OnValueChangedHandler += OnWaveIndexChanged;
        }

        // TODO OCEANE
        public void OnLivedChanged(int lives)
        {
            if (lives == 0)
            {
                Time.timeScale = 0f;
                GameObject.Find("CanvasStop").SetActive(true);
                // TODO end the game
            }
        }

        public void OnEneemyDied(Enemy en)
        {
            RemainingInWave.Value = RemainingInWave.Value == 0 ? 0 : RemainingInWave.Value - 1;
        }

        public void OnWaveIndexChanged(int wave)
        {
            RemainingInWave.Value =
                Wave.Enemies.Count;
        }
    };

    public class Game : MonoBehaviour
    {
        public static Game Instance;

        [SerializeField]
        public Cirrus.Events.ObservableValue<Session> Session = new Cirrus.Events.ObservableValue<Session>();

        private void Awake()
        {
            Instance = this;
        }

        private void Update()
        {
            //hbar.SetSize(session.Lives);


        }
    }
}