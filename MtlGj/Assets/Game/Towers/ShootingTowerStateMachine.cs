using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cirrus.Extensions;

namespace MTLGJ
{
    public class ShootingTowerState : TowerState
    {
        public ShootingTower ShootingTower => (ShootingTower)_context[0];

        public override Tower Tower => ShootingTower;

        public ShootingTowerStateMachine StateMachine => (ShootingTowerStateMachine)_context[1];

        public ShootingTowerState(
            bool isStart,
            params object[] context) : base(
            isStart,
            context)
        {
            //_timer = new Cirrus.Timer(1, start: false, repeat: true);
            //_timer.OnTimeLimitHandler += OnTimeOut;
        }
    }


    public class ShootingTowerActive : ShootingTowerState
    {
        public override int ID => (int)TowerStateID.Active;

        //private Vector2Int dest;
        private Cirrus.Timer _timer;

        private List<Enemy> _enemies = new List<Enemy>();        

        public ShootingTowerActive(
            bool isStart,
            params object[] context) : base(
            isStart,
            context)
        {
            _timer = new Cirrus.Timer(1, start: false, repeat: true);
            _timer.OnTimeLimitHandler += OnTimeOut;
        }

        public override void Enter(params object[] args)
        {
            base.Enter(args);
            _timer.Start(ShootingTower.Frequency);

            _enemies.Clear();
            ShootingTower.Colliderlistener.OnTriggerEnter2DHandler += OnTriggerEnter;
            ShootingTower.Colliderlistener.OnTriggerExit2DHandler += OnTriggerExit;
        }

        public override void Exit()
        {
            base.Exit();

            ShootingTower.Colliderlistener.OnTriggerEnter2DHandler -= OnTriggerEnter;
            ShootingTower.Colliderlistener.OnTriggerExit2DHandler -= OnTriggerExit;

        }

        public void OnTriggerEnter(Collider2D colldier)
        {
            var en = colldier.GetComponentInParent<Enemy>();
            if(en != null)
            _enemies.Add(en);
        }

        public void OnTriggerExit(Collider2D colldier)
        {
            var en = colldier.GetComponentInParent<Enemy>();
            _enemies.Remove(en);
        }

        private void OnTimeOut()
        {

            Shoot();

        }

        void Shoot()
        {
            if (_enemies.Count == 0)
                return;

            var tg = _enemies.OrderBy(x => 
                (x.Transform.position - Tower.Transform.position).magnitude)
                .FirstOrDefault();

            Vector2 velocity = (tg.Transform.position - ShootingTower.firePoint.position).normalized * ShootingTower.BulletForce;


            // TODO optimiz
            GameObject bullet = ShootingTower.bulletPrefab.gameObject.Create(ShootingTower.firePoint.position, null);
            //Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            Bullet b = bullet.GetComponent<Bullet>();
            b.SetDamage(ShootingTower.BulletDamage);
            b.SetDir((tg.Transform.position - Tower.Transform.position).normalized);
            b.SetForce(ShootingTower.BulletForce);
            b.SetTarget(tg);
            b.SetHoming(ShootingTower.Homing);
        }

    }

    public class ShootingTowerStateMachine : TowerStateMachine
    {
        [SerializeField]
        private ShootingTower _shootingTower;

        public override void Awake()
        {
            base.Awake();

            // TODO broken etc
            Add(new TowerIdle(false, _shootingTower, this));
            Add(new ShootingTowerActive(true, _shootingTower, this));
        }

    }
}
