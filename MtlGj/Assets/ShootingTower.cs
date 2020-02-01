using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingTower : MonoBehaviour
{
    GameObject[] objectArray;
    float distance;

    // Update is called once per frame
    void Update()
    {
        
    }

    void searchGameObjects(string tag) 
    {
        if (objectArray == null)
            objectArray = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (GameObject enemy in objectArray)  
        {
            distance = Vector3.Distance(enemy.transform.position, this.transform.position);
            Debug.Log("Distance: " + distance);
        }

    }
}
