using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingTower : MonoBehaviour
{
    GameObject[] objectArray;
    GameObject closestObject;

    public GameObject bulletPrefab;
    public Transform firePoint;

    Transform target;
    public float bulletForce;

    float distance;
    List<float> distanceList;

    float timer;
    public int waitingTime;
    public float movingDelay;

    public int health;

    // Update is called once per frame
    void Update()
    {
        closestObject = FindNearestEnemy();
        target = closestObject.transform.Find("Witch");

        timer += Time.deltaTime;

        if (timer > waitingTime && timer < movingDelay) {
           
        }
        else{ Shoot(); timer = 0; }

       
    }

    GameObject FindNearestEnemy() 
    {
        objectArray = GameObject.FindGameObjectsWithTag("Enemy");

        float minDistance = 0;
        int count = 0;

        foreach (GameObject enemyItem in objectArray) {
            float dist = Vector3.Distance(enemyItem.transform.position, firePoint.position);
            if (count == 0) {
                minDistance = dist;
                closestObject = enemyItem;
                count++;
            }
            else {
                if (dist < minDistance) {
                    minDistance = dist;
                    closestObject= enemyItem;
                }
            }
        }
        return closestObject;
    }

    void Shoot() 
    {
        Vector2 vector = target.position - firePoint.position;

        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(vector * bulletForce, ForceMode2D.Impulse);
    }
}
