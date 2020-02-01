using UnityEngine;
using System.Collections;

using Pathfinding = NesScripts.Controls.PathFind;
using NesScripts.Controls.PathFind;
using Cirrus.Extensions;
using System.Collections.Generic;

namespace MTLGJ
{
    [System.Serializable]
    public enum GameStateID : int
    {
        Intermission,
        InRound,
        StartRound,
        StartSession
    }


    public abstract class GameState : Cirrus.FSM.State
    {
        public override int ID => -1;//(int)EnemyStateID.Default;

        public GameStateMachine StateMachine => (GameStateMachine)_context[0];

        private List<Point> _path;//= new List<Point>()

        //[SerializeField]
        //protected List<NesScripts.Controls.PathFind.Point> _path;

        protected Vector2Int _finalDestination;

        protected Vector3 _nextDestination;

        protected int _currentPathPositionIndex = 0;

        //protected Timer _timer;

        //public virtual Character Character => _character;

        //private Character _character;


        public GameState(
            bool isStart,
            params object[] context) : base(
            isStart,
            context)
        {

        }
    }

    public class StartSession : GameState
    {
        public override int ID => (int)GameStateID.StartSession;

        public StartSession(
            bool isStart,
            params object[] context) : base(
            isStart,
            context)
        {

        }

        public override void Enter(params object[] args)
        {
            base.Enter(args);

            Game.Instance.session = new Session();

            StateMachine.TrySetState(GameStateID.StartRound);
        }
    }

    public class Intermission : GameState
    {
        public override int ID => (int)GameStateID.Intermission;

        public Intermission(
            bool isStart,
            params object[] context) : base(
            isStart,
            context)
        {

        }

        public override void Enter(params object[] args)
        {
            base.Enter(args);
        }
    }

    public class StartRound : GameState
    {
        public override int ID => (int)GameStateID.StartRound;

        //private Vector2Int dest;
        private Cirrus.Timer _timer;

        public StartRound(
            bool isStart,
            params object[] context) : base(
            isStart,
            context)
        {
            _timer = new Cirrus.Timer(1, start:false, repeat:true);
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

    public class InRound : GameState
    {
        public override int ID => (int)GameStateID.InRound;

        //private Vector2Int dest;

        private Cirrus.Timer _timer = new Cirrus.Timer(start:false, repeat:true);

        public InRound(
            bool isStart,
            params object[] context) : base(
            isStart,
            context)
        {
            _timer.OnTimeLimitHandler += OnTimeout;
        }

        private int _spwnIdx = 0;

        public void SpawnNext()
        {
            _spwnIdx = 0;
            var en = Game.Instance.session.Wave.Groups[_spwnIdx].Enemies[_spwnIdx];

            en.Create(
                Level.Instance.Starts.Random().FromCellToWorldPosition(),
                Level.Instance.transform);

            _timer.Start(Game.Instance.session.Wave.Groups[_spwnIdx].Frequency);

            _spwnIdx++;
        }

        public override void Enter(params object[] args)
        {
            base.Enter(args);

            SpawnNext();        
        }

        public void OnTimeout()
        {
            SpawnNext();

            if (_spwnIdx >= Game.Instance.session.Wave.Groups.Count)
            {
                _timer.Stop();
                StateMachine.TrySetState(GameStateID.Intermission);
            }
        }
    }



    public class GameStateMachine : Cirrus.FSM.BaseMachine
    {
        //[SerializeField]
        //private Avatar _character;

        public override void Awake()
        {
            base.Awake();

            Add(new StartSession(true, this));
            Add(new StartRound(false, this));
            Add(new InRound(false, this));
            Add(new Intermission(false, this));
        }

        public override void Start()
        {
            base.Start();

        }
    }

}