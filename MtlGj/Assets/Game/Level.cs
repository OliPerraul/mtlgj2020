using Cirrus.Extensions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Tilemaps;
using Pathfinding = NesScripts.Controls.PathFind;



public class Level : MonoBehaviour
{


    public Vector2Int ToPathfindingPosition(Vector3Int pos)
    {
        return ((Level.Instance.Tilemap.origin * -1) + pos).ToVector2Int();
    }


    //[SerializeField]
    //public Tilemap RuletileMap;

    [SerializeField]
    public Tilemap Tilemap;

    public static Level Instance;

    private Pathfinding.Grid _grid;

    private void Awake()
    {
        Instance = this;
        var init = new bool[
            Tilemap.size.x,
            Tilemap.size.y];

        _grid = new Pathfinding.Grid(init);

        for (int i = 0; i < Tilemap.size.x; i++)
        {
            for (int j = 0; j < Tilemap.size.y; j++)
            {
                var tile = (GGJTile)Tilemap.GetTile(new Vector3Int(i, j, 0));

                if (tile == null)
                    continue;

                var pfp = ToPathfindingPosition(new Vector3Int(i, j, 0));

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
