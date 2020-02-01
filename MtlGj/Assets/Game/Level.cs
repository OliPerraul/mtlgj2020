using Cirrus.Extensions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Tilemaps;
using Pathfinding = NesScripts.Controls.PathFind;

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

    //[SerializeField]
    //public Tilemap RuletileMap;

    [SerializeField]
    public Tilemap Tilemap;

    public static Level Instance;

    private Pathfinding.Grid _grid;

    public Pathfinding.Grid PathindingGrid => _grid;


    public List<Vector3Int> Ends = new List<Vector3Int>();

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

                _grid.UpdateGrid(pfp,
                    tile.ID == TileID.Empty || tile.ID == TileID.Start || tile.ID == TileID.End
                    );
            }
        }
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
