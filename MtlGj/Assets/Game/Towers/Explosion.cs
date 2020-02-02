using Cirrus.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MTLGJ
{
    public class Explosion : MonoBehaviour
    {
        [SerializeField]
        private ParticleSystem _particleSystem;

        [SerializeField]
        private ParticleSystemRenderer _particleSystemRenderer;

        [SerializeField]
        private float _time = 5f;

        public Cirrus.Timer _timer;    

        public void Awake()
        {
            _timer = new Cirrus.Timer(_time, start: false, repeat: false);
            _timer.OnTimeLimitHandler += OnTimeout;

            _particleSystem.Play();
            _particleSystemRenderer.sortingLayerName = "Foreground.Particles";
        }

        public void Start()
        {
            _particleSystem.Play();
            _timer.Start();
        }

        public void OnTimeout()
        {
            gameObject.Destroy();
        }

    }
}
