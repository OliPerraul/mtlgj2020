using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Wall : MonoBehaviour
{
  public GameObject myPrefab;

   public int width = 10;
   public int height = 4;
   public GameObject avatar;

     IsometricCharacterRenderer isoRenderer;

    Rigidbody2D rbody;

     private void Start()
    {
        rbody = avatar.GetComponent<Rigidbody2D>();
        isoRenderer = avatar.GetComponentInChildren<IsometricCharacterRenderer>();
    }
  
   
    void Update()
   {
     
      if (Input.GetButtonDown("Fire1"))
        {
            Debug.Log(Input.mousePosition);
           // isoRenderer.GetDirection();
                float positionX = rbody.position.x + 1;
                float positionY = rbody.position.y + 1;
               Instantiate(myPrefab, new Vector2(positionX,positionY), Quaternion.identity);
        }
   }
}
