using UnityEngine;
using System.Collections;

using Pathfinding = NesScripts.Controls.PathFind;
using NesScripts.Controls.PathFind;
using Cirrus.Extensions;
using System.Collections.Generic;

namespace MTLGJ
{
    [System.Serializable]
    public enum EnemyStateID : int
    {
        Start,
        Marching,
        Attack,
        Idle
    }


    public abstract class EnemyState : Cirrus.FSM.State
    {
        public override int ID => -1;//(int)EnemyStateID.Default;

        public Enemy Enemy => (Enemy)_context[0];

        private List<Point> _path;//= new List<Point>()

        //[SerializeField]
        //protected List<NesScripts.Controls.PathFind.Point> _path;

        protected Vector2Int _finalDestination;

        protected Vector3 _nextDestination;

        protected int _currentPathPositionIndex = 0;

        //protected Timer _timer;

        //public virtual Character Character => _character;

        //private Character _character;


        public EnemyState(
            bool isStart,
            params object[] context) : base(
            isStart,
            context)
        {

        }    



        public override void Enter(params object[] args)
        {
            //Level.Instance.Ends.Ge


            var tile = Level.Instance.Tilemap.GetTile(Level.Instance.Ends.Random());
            //Debug.Log("");

            _path = Pathfinding.Pathfinding.FindPath(
                Level.Instance.PathindingGrid, 
                Enemy.PathfindPosition.ToPathFindingPoint(),
                Level.Instance.Ends.Random().FromCellToPathfindingPosition().ToPathFindingPoint()
                );
        }


        public void FollowPath()
        {
            Enemy.Axis = (_nextDestination - Enemy.Transform.position).normalized;                   

            if (Enemy.Transform.position.IsCloseEnough(
                _nextDestination))
            {
                //Enemy.PathfindPosition =
                //    _path[_currentPathPositionIndex].Position;

                _currentPathPositionIndex++;


                _nextDestination =
                    _path[_currentPathPositionIndex]
                        .Position
                        .FromPathfindToCellPosition()
                        .FromCellToWorldPosition();
            }

            //Enemy.Direction =
            //    _nextDestination.x < Character.Transform.position.x ?
            //    -1 :
            //    1;
        }


        public override void Exit() { }

        public override void BeginUpdate()
        {

        }

        public override void EndUpdate() { }

    }

    public class Start : EnemyState
    {
        public override int ID => (int)EnemyStateID.Start;

        public Start(
            bool isStart,
            params object[] context) : base(
            isStart,
            context)
        {
            //Level.Instance.RuletileMap.WorldToCell(Enemy.transform.position)
        }

        public override void Enter(params object[] args)
        {
            base.Enter(args);
        }
    }

    public class Attack : EnemyState
    {
        public override int ID => (int) EnemyStateID.Attack;

        public Attack(
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



    public class Idle : EnemyState
    {
        public override int ID => (int)EnemyStateID.Idle;

        public Idle(
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


    public class Marching : EnemyState
    {
        public override int ID => (int)EnemyStateID.Marching;

        private Vector2Int dest;

        public Marching(
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



    public class EnemyStateMachine : Cirrus.FSM.BaseMachine
    {
        //[SerializeField]
        //private Avatar _character;

        [SerializeField]
        private Enemy _enemy;

        public override void Awake()
        {
            base.Awake();
            
            Add(new Start(true, _enemy));
            Add(new Marching(false, _enemy));
            Add(new Attack(false, _enemy));
            Add(new Idle(false, _enemy));
            
        }

        public override void Start()
        {
            base.Start();
            
        }
    }

}