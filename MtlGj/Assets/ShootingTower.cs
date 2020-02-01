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
        searchGameObjects("tag");
    }

    void searchGameObjects(string tag) 
    {
        if (objectArray == null)
            objectArray = GameObject.FindGameObjectsWithTag(tag);

        foreach (GameObject tagged in objectArray)  
        {
            distance = Vector3.Distance(tagged.transform.position, this.transform.position);
            Debug.Log("Distance: " + distance);
        }

    }
}
