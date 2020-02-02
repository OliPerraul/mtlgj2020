using UnityEngine;
using System.Collections;

using Pathfinding = NesScripts.Controls.PathFind;
using NesScripts.Controls.PathFind;
using Cirrus.Extensions;
using System.Collections.Generic;
using System.Linq;
using System;

using Pathfind = NesScripts.Controls.PathFind;

namespace MTLGJ
{
    [Serializable]
    public enum EnemyStateID : int
    {
        Start,
        Marching,
        Attack,
        InAttack,
        Withdraw,
        Idle
    }

    public abstract class EnemyState : Cirrus.FSM.State
    {
        public override int ID => -1;

        public Enemy Enemy => (Enemy)_context[0];

        public EnemyStateMachine StateMachine => (EnemyStateMachine)_context[1];

        protected List<Point> _path;

        protected Vector2Int _finalDestination;

        protected Vector3 _nextDestination;

        protected int _currentPathPositionIndex = 0;

        public EnemyState(
            bool isStart,
            params object[] context) : base(
            isStart,
            context)
        {

        }

        public virtual void OnTilemapCellChanged(Vector3Int cellPos, bool walkable)
        {
            if (Cirrus.Numeric.Chance.CheckIsTrue(Enemy.ChanceDecideOnTowerPlaced))
            {
                Decide();
                return;
            }

            if (_path == null)
            {
                Decide();
                return;
            }


            for (int i = 0; i < _path.Count; i++)
            {
                if (_path[i].Position.x < 0)
                {
                    continue;
                }

                var cel = _path[i].Position.FromPathfindToCellPosition();

                if (Level.Instance.Tilemap.GetTile(cel) == null)
                    continue;

                if (((GGJTile)Level.Instance.Tilemap.GetTile(cel)).ID == TileID.Building)
                {
                    Decide();
                    return;
                };
            }
        }

        public void Decide()
        {
            if (Level.Instance.Towers.Count == 0)
            {
                StateMachine.TrySetState(EnemyStateID.Marching);
                return;

            }

            var li = new EnemyStateID[] { EnemyStateID.Attack, EnemyStateID.Marching };

            while (true)
            {
                if (li.Random() == EnemyStateID.Marching)
                {
                    if (!Cirrus.Numeric.Chance.CheckIsTrue(Enemy.ChanceMarch))
                        continue;

                    StateMachine.TrySetState(EnemyStateID.Marching);
                    return;
                }
                else
                {
                    if (!Cirrus.Numeric.Chance.CheckIsTrue(Enemy.ChanceAttack))
                        continue;

                    StateMachine.TrySetState(EnemyStateID.Attack);
                    return;
                }
            }
        }

        public override void OnMachineDestroyed()
        {
            base.OnMachineDestroyed();

            Level.Instance.OnTilemapCellChangedHandler -= OnTilemapCellChanged;
        }

        public override void Enter(params object[] args)
        {
            Level.Instance.OnTilemapCellChangedHandler += OnTilemapCellChanged;
        }

        public override void Exit()
        {
            Level.Instance.OnTilemapCellChangedHandler -= OnTilemapCellChanged;
        }


        public void Mark(Vector3Int cel)
        {
            if (StateMachine.prevCelSet)
            {
                StateMachine.prevCelSet = false;

                Level.Instance.SetCharacterCel(StateMachine.prevCel, false);
            }

            if (Level.Instance.Tilemap.GetTile(cel) == null)
                return;

            if (((GGJTile)Level.Instance.Tilemap.GetTile(cel)).ID == TileID.Start)
                return;

            if (((GGJTile)Level.Instance.Tilemap.GetTile(cel)).ID == TileID.End)
                return;

            if (((GGJTile)Level.Instance.Tilemap.GetTile(cel)).ID == TileID.Character)
                return;

            StateMachine.prevCelSet = true;

            StateMachine.prevCel = cel;

            StateMachine.prevTile = Level.Instance.Tilemap.GetTile(
             cel) == null ? null : (GGJTile)Level.Instance.Tilemap.GetTile(
             cel);

            Level.Instance.SetCharacterCel(cel, true);
        }

        public virtual bool OnMaybeArrivedToDestination()
        {
            return false;
        }

        public void FollowPath()
        {
            if (_path == null)
            {
                Decide();
                return;
            }

            if (_currentPathPositionIndex >= _path.Count - 1)
                return;

            if (Enemy.Transform.position.IsCloseEnough(
                _nextDestination, 0.5f))
            {
                // DELETE ENEMY HERE

                Mark(_path[_currentPathPositionIndex].Position.FromPathfindToCellPosition());

                if (OnMaybeArrivedToDestination())
                {
                    Mark(_path[_currentPathPositionIndex].Position.FromPathfindToCellPosition());
                    return;
                }
   
                _currentPathPositionIndex++;

                _nextDestination =
                    _path[_currentPathPositionIndex]
                        .Position
                        .FromPathfindToCellPosition()
                        .FromCellToWorldPosition();
            }

            var npos =
                Vector3.MoveTowards(
                    Enemy.rbody.position,
                    _nextDestination,
                    Enemy.MoveSpeed);

            Enemy.isoRenderer.SetDirection((_nextDestination - Enemy.Transform.position).normalized);
            Enemy.rbody.MovePosition(npos);
        }

        public override void BeginUpdate()
        {
            FollowPath();
        }

        public override void EndUpdate() { }

        public virtual void OnCollisionEnter2D(Collision2D x)
        {

        }
    }

    public class EnemyStart : EnemyState
    {
        public override int ID => (int)EnemyStateID.Start;

        public EnemyStart(
            bool isStart,
            params object[] context) : base(
            isStart,
            context)
        {

        }

        public override void BeginUpdate()
        {
            //base.BeginUpdate();
        }

        public override void Enter(params object[] args)
        {
            base.Enter(args);

            Decide();
        }
    }

    public class EnemyInAttack : EnemyState
    {
        public override int ID => (int)EnemyStateID.InAttack;

        public override string Name => "In Attack";

        private Cirrus.Timer _timer;

        private Tower _tower;

        
        public EnemyInAttack(
             bool isStart,
             params object[] context) : base(
             isStart,
             context)
        {
            _timer = new Cirrus.Timer(start: false, repeat: true);
            _timer.OnTimeLimitHandler += OnTimeouAttackt;
        }

        public void OnTimeouAttackt()
        {
            Attack();
        }

        public override void Enter(params object[] args)
        {
            base.Enter(args);

            _tower = (Tower)args[0];
            _timer.Start(Enemy.AttackFrequence);
        }

        public override void Exit()
        {
            _timer.Stop();
        }


        public override void BeginUpdate()
        {
            //base.BeginUpdate();
        }

        public void Attack()
        {
            if (Enemy.gameObject == null)
                return;

            if (Enemy.SpriteRenderer.gameObject == null)
                return;
            


            iTween.PunchPosition(
                Enemy.SpriteRenderer.gameObject,
                Enemy.Transform.position + (_tower.Transform.position - Enemy.Transform.position).normalized * Enemy.AttackRange,
                Enemy.AttackSpeed);

            _tower.ApplyDamage(Enemy.Damage);
        }
    }


    public class EnemyAttack : EnemyState
    {
        public override int ID => (int)EnemyStateID.Attack;

        private Tower _target;

        private Node _dest;

        public EnemyAttack(
            bool isStart,
            params object[] context) : base(
            isStart,
            context)
        {

        }

        public override bool OnMaybeArrivedToDestination()
        {
            if (_path.Count == 0)
                return false;

            if (_target == null)
                return false;

            if (_currentPathPositionIndex >= _path.Count-2)
            {
                StateMachine.TrySetState(EnemyStateID.InAttack, _target);
                return true;
            }

            if ( (Enemy.Transform.position - _target.Transform.position).magnitude <= 2)
            {
                StateMachine.TrySetState(EnemyStateID.InAttack, _target);
                return true;
            }


            return false;
        }

        public IEnumerable<Node> GetNeighbours(
            Node node,
            Pathfind.Pathfinding.DistanceType distanceType)
        {
            var neighbours = Level.Instance.PathindingGrid.GetNeighbours(
                node,
                distanceType
                );

            return neighbours;
        }

        public Node GetNearestNeighbour(
            BaseObject target,
            Pathfind.Pathfinding.DistanceType distanceType)
        {
            var neighbours = Level.Instance.PathindingGrid.GetNeighbours(
                target.Node,
                distanceType
                );

            float min = Mathf.Infinity;

            Node nearestNeighbour = null;

            foreach (var neigh in neighbours)
            {
                if (!neigh.walkable)
                    continue;

                float candidate = Pathfind.Pathfinding.GetDistance(neigh, target.Node);

                if (candidate < min)
                {
                    nearestNeighbour = neigh;
                    min = candidate;
                }
            }

            return nearestNeighbour;
        }


        public override void Enter(params object[] args)
        {
            base.Enter(args);

            _target = Level.Instance.TowersEnum.OrderBy(x => (x.Transform.position - Enemy.Transform.position).magnitude).FirstOrDefault();

            if (_target == null)
            {
                Decide();
                return;
            }

            _dest = GetNearestNeighbour(_target, Pathfinding.Pathfinding.DistanceType.Euclidean);

            if (_dest == null)
            {
                Decide();
                return;
            }

            _currentPathPositionIndex = 0;

            _path = Pathfinding.Pathfinding.FindPath(
                Level.Instance.PathindingGrid,
                Enemy.PathfindPosition.ToPathFindingPoint(),
                _dest.Point,
                Pathfinding.Pathfinding.DistanceType.Manhattan
                );

            if (_path.Count != 0)
            {
                _nextDestination =
                    _path[0]
                        .Position
                        .FromPathfindToCellPosition()
                        .FromCellToWorldPosition();
            }


        }

        public override void BeginUpdate()
        {
            base.BeginUpdate();
        }
    }


    public class EnemyIdle : EnemyState
    {
        public override int ID => (int)EnemyStateID.Idle;

        public EnemyIdle(
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


    public class EnemyMarching : EnemyState
    {
        public override int ID => (int)EnemyStateID.Marching;

        private Vector2Int dest;

        //private List<Point> _path = new List<Point>();

        public override bool OnMaybeArrivedToDestination()
        {

            var cell = _path[_currentPathPositionIndex].Position.FromPathfindToCellPosition();

            var tile = Level.Instance.Tilemap.GetTile(cell) == null ? null : (GGJTile)Level.Instance.Tilemap.GetTile(cell);

            if (tile != null)
            {
                if (tile.ID == TileID.End)
                {
                    Level.Instance.RemoveEnemy(Enemy, true);
                    StateMachine.TrySetState(EnemyStateID.Idle);
                    return true;
                }
            }

            return false;

        }

        public EnemyMarching(
            bool isStart,
            params object[] context) : base(
            isStart,
            context)
        {

        }

        public override void Enter(params object[] args)
        {
            base.Enter(args);

            _currentPathPositionIndex = 0;

            var tile = Level.Instance.Tilemap.GetTile(Level.Instance.Ends.Random());

            _path = Pathfinding.Pathfinding.FindPath(
                Level.Instance.PathindingGrid,
                Enemy.PathfindPosition.ToPathFindingPoint(),
                Level.Instance.Ends.Random().FromCellToPathfindingPosition().ToPathFindingPoint(),
                Pathfinding.Pathfinding.DistanceType.Manhattan
                );

            if (_path.Count != 0)
            {
                _nextDestination =
                    _path[0]
                        .Position
                        .FromPathfindToCellPosition()
                        .FromCellToWorldPosition();    
            }
        }
    }

    public class EnemyStateMachine : Cirrus.FSM.BaseMachine
    {
        [SerializeField]
        private Enemy _enemy;

        public bool prevCelSet = false;

        public Vector3Int prevCel = new Vector3Int(-1, -1, -1);

        public GGJTile prevTile = null;

        public void OnEnemyRemoved()
        {
            if (prevCelSet)
            {
                prevCelSet = false;

                Level.Instance.SetCharacterCel(prevCel, false);
            }
        }

        public override void Awake()
        {

            base.Awake();

            _enemy.OnRemovedHandler += OnEnemyRemoved;
            _enemy.OnCollisionEnter2DHandler += x => ((EnemyState)Top).OnCollisionEnter2D(x);

            Add(new EnemyStart(true, _enemy, this));
            Add(new EnemyMarching(false, _enemy, this));
            Add(new EnemyAttack(false, _enemy, this));
            Add(new EnemyInAttack(false, _enemy, this));
            Add(new EnemyIdle(false, _enemy, this));

        }

        public override void Start()
        {
            base.Start();

        }
    }

}