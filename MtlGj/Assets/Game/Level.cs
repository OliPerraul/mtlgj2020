using Cirrus.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Tilemaps;
using Pathfinding = NesScripts.Controls.PathFind;

namespace MTLGJ
{

    public static class LevelUtils
    {
        public static Vector2Int FromCellToPathfindingPosition(this Vector3Int pos)
        {
            return ((Level.Instance.Tilemap.origin * -1) + pos).ToVector2Int();
        }

        public static Vector3Int FromPathfindToCellPosition(this Vector2Int pathfindPosition)
        {
            return Level.Instance.Tilemap.origin + pathfindPosition.ToVector3Int();
        }

        public static Vector3 FromCellToWorldPosition(this Vector3Int cellPosition)
        {
            // TODO harc nono
            return Level.Instance.Tilemap.CellToWorld(cellPosition) + new Vector3(1.5f - 0.75f - 0.75f, 1.5f - 0.75f);
        }

        public static Vector3Int FromWorldToCellPosition(this Vector3 wposition)
        {
            return Level.Instance.Tilemap.WorldToCell(wposition);
        }
    }

    public class Level : MonoBehaviour
    {
        public Cirrus.Events.Event<Enemy> OnEnemyRemovedHandler;

        //[SerializeField]
        //public Tilemap RuletileMap;

        public Cirrus.Events.Event<Vector3Int, bool> OnTilemapCellChangedHandler;


        public Dictionary<Vector3Int, int> CharacterCells = new Dictionary<Vector3Int, int>();

        public Dictionary<Vector3Int, Tower> Towers = new Dictionary<Vector3Int, Tower>();

        public void AddTower(Tower  tower, Vector3Int pos) { Towers.Add(pos, tower); }
        public void RemoveTower(Tower tower, Vector3Int pos) { Towers.Remove(pos); }

        public Tower GetTower(Vector3Int pos) 
        {
            Tower tower;
            if (Towers.TryGetValue(pos, out tower)){ return tower; } return null;
        }

        public void SetBuildingCell(Vector3Int pos, bool building)
        {
            Tilemap.SetTile(
                   pos,
                   TilemapResources.Instance.GetTile(building ? TileID.Building : TileID.Empty));

            _grid.UpdateGrid(pos.FromCellToPathfindingPosition(), !building);

            OnTilemapCellChangedHandler?.Invoke(pos, !building);
        }        
        


        public void SetCharacterCel(Vector3Int pos, bool character)
        {
            if (character)
            {
                //_grid.UpdateGrid(pos.ToVector2Int(), walkable);
                if (!CharacterCells.TryGetValue(pos, out int value))
                {
                    CharacterCells.Add(pos, 1);                
                    Tilemap.SetTile(
                         pos,
                         TilemapResources.Instance.GetTile(TileID.Character));
                }
                else CharacterCells[pos]++;

            }
            else
            {
                if (CharacterCells.TryGetValue(pos, out int value))
                {
                    CharacterCells[pos]--;
                    if (CharacterCells[pos] == 0)
                    {
                        Tilemap.SetTile(
                             pos,
                             TilemapResources.Instance.GetTile(TileID.Empty));
                    }
                }

            }
        }



        [SerializeField]
        public Tilemap Tilemap;

        [SerializeField]
        public Tilemap TilemapGuide;

        public static Level Instance;

        private Pathfinding.Grid _grid;

        public Pathfinding.Grid PathindingGrid => _grid;

        //public Action<Enemy> OnEnemyDiedHandler { get; internal set; }

        public List<Vector3Int> Ends = new List<Vector3Int>();


        public List<Vector3Int> Starts = new List<Vector3Int>();

        private void Awake()
        {
            Tilemap.CompressBounds();

            Instance = this;
            var init = new bool[
                Tilemap.size.x,
                Tilemap.size.y];

            _grid = new Pathfinding.Grid(init);


            //Pathfinding.

            for (int i = 0; i < Tilemap.size.x; i++)
            {
                for (int j = 0; j < Tilemap.size.y; j++)
                {
                    // No offset == Pathfind pos
                    var celpos = new Vector2Int(i, j).FromPathfindToCellPosition();
                    var tile = (GGJTile)Tilemap.GetTile(celpos);
                    if (tile == null)
                        continue;

                    var pfp = celpos.FromCellToPathfindingPosition();

                    if (tile.ID == TileID.End)
                    {
                        Ends.Add(celpos);
                    }

                    if (tile.ID == TileID.Start)
                    {
                        Starts.Add(celpos);
                    }

                    _grid.UpdateGrid(pfp,
                        tile.ID == TileID.Empty ||
                        tile.ID == TileID.Start ||
                        tile.ID == TileID.End ||
                        tile.ID == TileID.Character ||
                        tile.ID == TileID.Guide
                        );
                }
            }
        }

        public void RemoveEnemy(Enemy enemy, bool invaded)
        {
            OnEnemyRemovedHandler?.Invoke(enemy);
            enemy.OnRemovedHandler?.Invoke();
            if (invaded)
            {
                Game.Instance.Session.Value.Lives.Value--;
            }

            enemy.gameObject.Destroy();

        }


        // Start is called before the first frame update
        void Start()
        {
            //RuletileMap.SetTile(TilemapResources.Instance.GetTile(TileID.))

            //    //Level.Instance.InvokeRepeating.

            //TilemapResources.Inst


            //Level.Instance.RuletileMap.Wo
        }

        // Update is called once per frame
        void Update()
        {

        }


    }
}