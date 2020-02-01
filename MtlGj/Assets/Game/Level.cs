using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Tilemaps;
using Pathfinding = NesScripts.Controls.PathFind;

public class Level : MonoBehaviour
{
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
                //_grid.UpdateGrid(new Vector2Int())
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
