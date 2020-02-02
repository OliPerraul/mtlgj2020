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
        public override int ID => -1;

        public Enemy Enemy => (Enemy)_context[0];

        public EnemyStateMachine StateMachine => (EnemyStateMachine)_context[1];

        private List<Point> _path;

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
            // Only reroute if cel in path was changed

            if (_path == null)
            {
                CalculatePath();
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
                    CalculatePath();
                    return;
                };
            }
        }

        public void CalculatePath()
        {
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

                Enemy.pos = _nextDestination;
                //Level.Instance.Tilemap.SetTile(
                //     _path[_currentPathPositionIndex].Position.FromPathfindToCellPosition(),
                //     TilemapResources.Instance.GetTile(TileID.Building));
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

            _nextDestination = Enemy.Transform.position;
            CalculatePath();
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

        public void FollowPath()
        {
            if (_currentPathPositionIndex >= _path.Count - 1)
                return;

            if (Enemy.Transform.position.IsCloseEnough(
                _nextDestination, 0.5f))
            {
                // DELETE ENEMY HERE
                var cell = _path[_currentPathPositionIndex].Position.FromPathfindToCellPosition();

                var tile = Level.Instance.Tilemap.GetTile(cell) == null ? null : (GGJTile)Level.Instance.Tilemap.GetTile(cell);

                if (tile != null)
                {
                    if (tile.ID == TileID.End)
                    {
                        Level.Instance.RemoveEnemy(Enemy, true);
                        StateMachine.TrySetState(EnemyStateID.Idle);
                        return;
                    }
                }

                Mark(_path[_currentPathPositionIndex].Position.FromPathfindToCellPosition());

                _path[_currentPathPositionIndex] = new Point(-1, -1);

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


        //public override void Exit() { }

        public override void BeginUpdate()
        {
            FollowPath();
        }

        public override void EndUpdate() { }

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
            //Level.Instance.RuletileMap.WorldToCell(Enemy.transform.position)
        }

        public override void Enter(params object[] args)
        {
            base.Enter(args);
        }
    }

    public class EnemyAttack : EnemyState
    {
        public override int ID => (int)EnemyStateID.Attack;

        public EnemyAttack(
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
        }
    }

    public class EnemyStateMachine : Cirrus.FSM.BaseMachine
    {
        //[SerializeField]
        //private Avatar _character;

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

            Add(new EnemyStart(true, _enemy, this));
            Add(new EnemyMarching(false, _enemy, this));
            Add(new EnemyAttack(false, _enemy, this));
            Add(new EnemyIdle(false, _enemy, this));

        }

        public override void Start()
        {
            base.Start();

        }
    }

}