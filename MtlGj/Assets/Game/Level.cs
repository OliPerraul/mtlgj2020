using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Tilemaps;

public class Level : MonoBehaviour
{
    [SerializeField]
    public Tilemap RuletileMap;

    [SerializeField]
    public Tilemap CollisionTileMap;

    public static Level Instance;

    private void Awake()
    {
        Instance = this;        
    }


    // Start is called before the first frame update
    void Start()
    {
        //RuletileMap.SetTile(TilemapResources.Instance.GetTile(TileID.))
            
        //    //Level.Instance.InvokeRepeating.

        //TilemapResources.Inst
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
