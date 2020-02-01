using Cirrus.Extensions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Tilemaps;
using Pathfinding = NesScripts.Controls.PathFind;

public class oceaneCreation : MonoBehaviour
{ 
   public GameObject avatar;
   public GameObject myPrefab;


     IsometricCharacterRenderer isoRenderer;

    Rigidbody2D rbody;

     public Tilemap tilemap;

     public Tile start;
    float positionX;
    float positionY;

     private void Start()
    {
        rbody = avatar.GetComponent<Rigidbody2D>();
       // scriptPosition = avatar.GetComponent<IsometricPlayerMovementController>();
        isoRenderer = avatar.GetComponentInChildren<IsometricCharacterRenderer>();

    }
  
   
    void Update()
   {
     //Input.GetButtonDown("Fire1")
      if ( Input.GetKeyDown(KeyCode.E))
        {
            
                Vector2 playerPos = avatar.transform.position;
                Vector2 playerDirection = avatar.transform.forward *5;
                Quaternion playerRotation = avatar.transform.rotation;
                float spawnDistance = 500;
                
                Vector2 spawnPos = playerPos + playerDirection;
                Debug.Log(playerDirection.ToString("N4"));
               Instantiate(myPrefab, spawnPos, Quaternion.identity);

                Vector3Int plpl = new Vector3Int((int)playerPos.x, (int)playerPos.y,3);
               /* Tile tile = (Tile)tilemap.GetTile(plpl);
               //  Tile tile = ScriptableObject.CreateInstance<Tile>();
                 tilemap.SetTile(plpl, tile);*/

                 Level.Instance.Tilemap.SetTile(
                     plpl,
                     TilemapResources.Instance.GetTile(TileID.Full));
        }
   }
}
