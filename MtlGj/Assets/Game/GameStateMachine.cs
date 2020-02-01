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
        Start
    }


    public abstract class GameState : Cirrus.FSM.State
    {
        public override int ID => -1;//(int)EnemyStateID.Default;

        //public Enemy Enemy => (Enemy)_context[0];

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

    public class GameStart : GameState
    {
        public override int ID => (int)GameStateID.Start;

        public GameStart(
            bool isStart,
            params object[] context) : base(
            isStart,
            context)
        {

        }

        public override void Enter(params object[] args)
        {
            base.Enter(args);

            Game.Instance.Session = new Session();
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

    public class InRound : GameState
    {
        public override int ID => (int)GameStateID.InRound;

        //private Vector2Int dest;

        public InRound(
            bool isStart,
            params object[] context) : base(
            isStart,
            context)
        {

        }

        //public override void Enter(params object[] args)
        //{
        //    base.Enter(args);
        //}
    }



    public class GameStateMachine : Cirrus.FSM.BaseMachine
    {
        //[SerializeField]
        //private Avatar _character;

        public override void Awake()
        {
            base.Awake();

            Add(new GameStart(true));
            Add(new EnemyMarching(false));
            Add(new EnemyAttack(false));
            Add(new EnemyIdle(false));

        }

        public override void Start()
        {
            base.Start();

        }
    }

}