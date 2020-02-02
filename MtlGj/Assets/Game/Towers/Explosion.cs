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
        private float _damage;

        [SerializeField]
        private ParticleSystem _particleSystem;

        [SerializeField]
        private ParticleSystemRenderer _particleSystemRenderer;

        [SerializeField]
        private float _time = 5f;

        public Cirrus.Timer _timer;

        [SerializeField]
        private ColliderListener _colliderListener;

        public void Awake()
        {
            _timer = new Cirrus.Timer(_time, start: false, repeat: false);
            _timer.OnTimeLimitHandler += OnTimeout;

            _colliderListener.OnTriggerStayHandler += OnDoTriggerStay;

            _particleSystem.Play();
            //_particleSystemRenderer.sortingLayerName = "Foreground.Particles";
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

        private List<BaseObject> _hits = new List<BaseObject>();

        public void OnDoTriggerStay(Collider2D other)
        {
            var tower = other.GetComponentInParent<Tower>();

            if (tower != null && !_hits.Contains(tower))
            {
                tower.ApplyDamage(_damage);
                _hits.Add(tower);
            }

            var en = other.GetComponentInParent<Enemy>();

            if (en != null && !_hits.Contains(en))
            {
                en.ApplyDamage(_damage);
                _hits.Add(en);
            }
        }
    }
}
