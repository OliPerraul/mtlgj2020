using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            _timer.Start();
        }

        private void OnTimeOut()
        {
            CountDown.Instance.Number--;

            if (CountDown.Instance.Number < -1)
            {
                _timer.Stop();
                StateMachine.TrySetState(GameStateID.InRound);
            }
        }
    }

    public class ShootingTowerStateMachine : TowerStateMachine
    {
        //[Serializable]
        private ShootingTower _shootingTower;

        public override void Awake()
        {
            base.Awake();

            Add(new ShootingTowerActive(false, _shootingTower, this));
        }

    }
}
