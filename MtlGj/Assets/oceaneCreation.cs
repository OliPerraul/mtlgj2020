using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class oceaneCreation : MonoBehaviour
{ 
   public GameObject avatar;
   public GameObject myPrefab;

     IsometricCharacterRenderer isoRenderer;

    Rigidbody2D rbody;

    float horizontalInput;
    float verticalInput; 
    bool horizontal = true;
    bool vertical;
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

               // RuletileMap.SetTile(TilemapResources.Instance.Getile(tileID.))
        }
   }
}
