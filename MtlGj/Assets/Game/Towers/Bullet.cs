using Cirrus;
using Cirrus.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace MTLGJ
{
    // TODO pool
    public class Bullet : MonoBehaviour
    {
        private float _dmg;

        private Timer _timer = new Timer(start: false);

        [SerializeField]
        private float _timeout = 5f;

        public void Awake()
        {
            _timer.OnTimeLimitHandler += OnTimeout;
        }

        public void OnDestroy()
        {
            _timer.OnTimeLimitHandler -= OnTimeout;
        }


        public void Start()
        {
            _timer.Start(_timeout);
        }

        public void SetDamage(float dmg)
        {
            _dmg = dmg;
        }

        private bool _finished = false;

        public void OnCollisionEnter2D(Collision2D collision)
        {
            var en = collision.transform.GetComponent<Enemy>();
            if (en != null)
            {
                en.ApplyDamage(_dmg);
                gameObject.Destroy();
            }    
         }

        public void OnTriggerEnter2D(Collider2D collision)
        {
            var en = collision.transform.GetComponent<Enemy>();
            if (en != null)
            {
                _finished = false;
                en.ApplyDamage(_dmg);
                
                gameObject.Destroy();
            }
        }

        public void OnTimeout()
        {
            gameObject.Destroy();
        }

        private Vector3 _dir;

        private float _force;

        public void SetDir(Vector3 dir)
        {
            _dir = dir;
        }

        public void SetForce(float force)
        {
            _force = force;
        }

        private Enemy _tg;

        private float _initDist = 1;

        public void SetTarget(Enemy tg)
        {
            _tg = tg;

            _initDist = (_tg.transform.position - transform.position).magnitude;

        }

        public void Update()
        {
            if (_finished)
                return;

            if (gameObject == null)
                return;

            if (_tg == null)
                return;

            if (_tg.gameObject == null)
                return;


            var dir = (_tg.transform.position - transform.position);

            float w1 = _initDist == 0 ? 0 : dir.magnitude / _initDist;
            float w2 = 1 - w1;
        
            transform.Translate(_dir * _force * w1 * Time.smoothDeltaTime);
            transform.Translate(dir.normalized *  _force  * w2 * Time.smoothDeltaTime);

        }
    }
}