using Cirrus.Extensions;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace MTLGJ
{
    public class ShootingTower : Tower
    {
        public override void OpenUpgradMenu()
        {
            UpgradeMenu.Instance.OpenShootingTowerUpgrades();
        }

        // TODO make better upgrades (more specific ..??)

        public override void Upgrade(ShootingTowerUpgrade upgrade)
        {
            if (upgrade == ShootingTowerUpgrade.Unknown)
                return;

            if (TakeSufficientFunds(upgrade))
                return;

            switch (upgrade)
            {
                case ShootingTowerUpgrade.Range:
                    Range++;
                    break;

                case ShootingTowerUpgrade.BulletForce:
                    BulletForce++;
                    break;

                case ShootingTowerUpgrade.Frequency:
                    Frequency++;
                    break;

                case ShootingTowerUpgrade.BulletDamage:
                    BulletDamage++;
                    break;

            }

            Level.Value++;
        }


        public override TowerID ID => TowerID.Shooting1;

        GameObject[] objectArray;
        GameObject closestObject;

        public GameObject bulletPrefab;
        public Transform firePoint;


        public float movingDelay;
   
        public GameObject wreckagePrefab;

        [SerializeField]
        private CircleCollider2D _detectionRadius;

        public float Range
        {
            get
            {
                return _detectionRadius.radius;
            }

            set => _detectionRadius.radius = value;
        }


        public float BulletForce = 4f;

        public float BulletDamage = 1f;

        [SerializeField]
        public float Frequency = 0.5f;
    

        private Cirrus.Timer _timer = new Cirrus.Timer(repeat: true, start: false);

        public void Awake()
        {

            //player = GameObject.FindGameObjectWithTag("Player");
        }


        // Update is called once per frame
        public override void Update()
        {
            base.Update();

            hbar.SetSize(Health);

            if (Health <= 0)
            {
               
                Instantiate(wreckagePrefab, this.transform.position, this.transform.rotation);
                Destroy(this.gameObject);
            }
        }

    }
}