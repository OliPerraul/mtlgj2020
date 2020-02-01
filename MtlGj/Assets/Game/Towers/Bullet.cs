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

        public void OnCollisionEnter2D(Collision2D collision)
        {
            var en = collision.transform.GetComponent<Enemy>();
            if (en != null)
            {
                en.ApplyDamage(_dmg);
                gameObject.Destroy();
            }    
         }

        public void OnTriggerEnter(Collider2D collision)
        {
            var en = collision.transform.GetComponent<Enemy>();
            if (en != null)
            {
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

        public void SetTarget(Enemy tg)
        {
            _tg = tg;
        }

        public void Update()
        {
            //transform.position += _dir * _force * Time.deltaTime;
            transform.Translate(_dir * _force * Time.smoothDeltaTime);

            if (_tg != null)
            {
                //transform.position += _dir * _force * Time.deltaTime;
                transform.Translate((_tg.transform.position - transform.position).normalized * 2f * Time.smoothDeltaTime);
            }

        }
    }
}