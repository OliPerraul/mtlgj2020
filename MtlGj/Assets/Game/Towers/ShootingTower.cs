﻿using Cirrus.Extensions;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace MTLGJ
{
    public class ShootingTower : Tower
    {
        public override TowerID ID => TowerID.Shooting1;

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

        public float health = 1f;
        [SerializeField] private healthbar hbar;


        [SerializeField]
        private float _range;

        // Update is called once per frame
        public void Update()
        {
            // TODO: DONT DO THIS EVERY FRAME


            var closestEnemy = FindNearestEnemy();

            if (closestEnemy != null)
            {
                Debug.Log("there is a bad guys");
                target = closestObject.transform.Find("EnemyTransform");
                timer += Time.deltaTime;

                if (timer > waitingTime && timer < movingDelay) { } else { Shoot(); timer = 0; }
            }
            else { Debug.Log("there is no bad guys"); }



            hbar.SetSize(health);

            if (health <= 0) { gameObject.Destroy(); }


        }

        private List<Enemy> CaptureNearbyTargets()
        {
            // TODO: for now we use closest, other options maybe?
            Collider2D[] colliders = Physics2D.OverlapCircleAll(Transform.position, _range);
            var lis = new List<Enemy>();

            foreach (Collider2D collider in colliders)
            {
                var tg = collider.GetComponentInParent<Enemy>();

                if (tg == null)
                    continue;

                 lis.Add(tg);
            }

            return lis;
        }



        public Enemy FindNearestEnemy()
        {
            return CaptureNearbyTargets().
                OrderBy(x => (x.Transform.position - Transform.position).magnitude).FirstOrDefault();

            //objectArray = GameObject.FindGameObjectsWithTag("Enemy");

            //float minDistance = 0;
            //int count = 0;

            //foreach (GameObject enemyItem in objectArray)
            //{
            //    float dist = Vector3.Distance(enemyItem.transform.position, firePoint.position);
            //    if (count == 0)
            //    {
            //        minDistance = dist;
            //        closestObject = enemyItem;
            //        count++;
            //    }
            //    else
            //    {
            //        if (dist < minDistance)
            //        {
            //            minDistance = dist;
            //            closestObject = enemyItem;
            //        }
            //    }
            //}
            //return closestObject;
        }

        void Shoot()
        {
            Vector2 vector = target.position - firePoint.position;

            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            rb.AddForce(vector * bulletForce, ForceMode2D.Impulse);
        }
    }
}