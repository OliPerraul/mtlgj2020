using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace MTLGJ
{
    public class HUD : MonoBehaviour
    {
        [SerializeField]
        private UnityEngine.UI.Text _lives;    

        [SerializeField]
        private UnityEngine.UI.Text _wave;

        [SerializeField]
        private UnityEngine.UI.Text _remaining;

        [SerializeField]
        private UnityEngine.UI.Text _resources;

        public void Awake()
        {
            Game.Instance.Session.OnValueChangedHandler += OnSessionChanged;
            //Level.Instance.OnEnemyDiedHandler += OnEnemyDied;
        }

        public void OnSessionChanged(Session session)
        {
            session.Lives.OnValueChangedHandler += OnLivedChanged;
            OnLivedChanged(session.Lives.Value);

            session.WaveIndex.OnValueChangedHandler += OnWaveIndexChanged;
            OnWaveIndexChanged(session.WaveIndex.Value);

            session.RemainingInWave.OnValueChangedHandler += OnRemaingInWave;
            OnRemaingInWave(session.RemainingInWave.Value);

            session.ResourcesAmount.OnValueChangedHandler += OnResourcesChanged;
            OnResourcesChanged(session.ResourcesAmount.Value);
        }

        public void OnRemaingInWave(int remainig)
        {
            _remaining.text = remainig.ToString();
        }

        public void OnLivedChanged(int lives)
        {
            _lives.text = lives.ToString();
        }

        public void OnResourcesChanged(int resources)
        {
            _resources.text = resources.ToString();
        }

        public void OnWaveIndexChanged(int wave)
        {
            _wave.text = wave.ToString();

            //Game.Instance.Session.Value.RemainingInWave.Value =
            //    Game.Instance.Session.Value.Wave.Groups.Sum(x => x.Enemies.Count);
        }
    }

}

