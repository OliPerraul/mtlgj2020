using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace MTLGJ
{
    public enum ShootingTowerUpgrade
    {
        Unknown,
        Range,
        BulletForce,
        BulletDamage,
        Frequency,
    }

    public enum TowerID
    {
        Shooting1,
        Shield1
    }

    public static class BuildUtils
    {
        public static TowerID[] Towers
            = new TowerID[]
            {
                TowerID.Shield1,
                TowerID.Shooting1
            };
    }

    public static class UpgradeUtils
    {
        public static TowerUpgrade[] TowerUpgrades
            = new TowerUpgrade[]
        {
            TowerUpgrade.Health,
            TowerUpgrade.MaxHealth
        };

        public static ShootingTowerUpgrade[] ShootTowerUpgrades
                = new ShootingTowerUpgrade[]
            {
                ShootingTowerUpgrade.BulletDamage,
                ShootingTowerUpgrade.BulletForce,
                ShootingTowerUpgrade.Frequency,
                ShootingTowerUpgrade.Range,
            };
    }


    public enum TowerUpgrade
    {
        Unknown,
        Health,
        MaxHealth,
    }

    public class TowerResources : Cirrus.Resources.BaseResourceManager<TowerResources>
    {
        //public 
        public float HealthUpgradeChance = 0.4f;
        public float HealthUpgradeCost = 0.4f;

        public float HealthRepairChance = 0.4f;
        public float HealthRepairCost = 5f;

        public float RangeUpgradeChance = 0.5f;
        public float RangeUpgradeCost = 0.5f;

        public float BulletForceUpgradeChance = 0.5f;
        public float BulletForceUpgradeCost = 0.5f;

        public float BulletDamageUpgradeChance = 0.4f;
        public float BulletDamageUpgradeCost = 0.4f;


        public float ShootingFrequencyUpgradeChance = 0.4f;
        public float ShootingFrequencyUpgradeCost = 0.4f;

        public float Shooting1BuildChance = 0.4f;
        public float Shooting1BuildCost = 0.4f;

        public float Shield1BuildChance = 0.4f;
        public float Shield1BuildCost = 0.4f;

        public float Chance(TowerID id)
        {
            switch (id)
            {
                case TowerID.Shield1:
                    return Shooting1BuildChance;
                default:
                case TowerID.Shooting1:
                    return Shield1BuildChance;

            }
        }

        public float Cost(TowerID id)
        {
            switch (id)
            {
                case TowerID.Shield1:
                    return Shooting1BuildCost;
                default:
                case TowerID.Shooting1:
                    return Shield1BuildCost;

            }
        }



        public float Chance(ShootingTowerUpgrade id)
        {
            switch (id)
            {
                case ShootingTowerUpgrade.BulletDamage:
                    return Shooting1BuildChance;

                case ShootingTowerUpgrade.BulletForce:
                    return BulletForceUpgradeChance;

                case ShootingTowerUpgrade.Frequency:
                    return ShootingFrequencyUpgradeChance;

                default:
                case ShootingTowerUpgrade.Range:
                    return RangeUpgradeChance;
            }
        }

        public float Cost(ShootingTowerUpgrade id)
        {
            switch (id)
            {
                case ShootingTowerUpgrade.BulletDamage:
                    return Shooting1BuildCost;

                case ShootingTowerUpgrade.BulletForce:
                    return BulletForceUpgradeCost;

                case ShootingTowerUpgrade.Frequency:
                    return ShootingFrequencyUpgradeCost;

                default:
                case ShootingTowerUpgrade.Range:
                    return RangeUpgradeCost;
            }
        }



        public float Chance(TowerUpgrade id)
        {
            switch (id)
            {
                default:
                case TowerUpgrade.Health:
                    return HealthRepairChance;

                case TowerUpgrade.MaxHealth:
                    return HealthUpgradeChance;

                    //case ShootingTowerUpgrade.:
                    //    return Shield1BuildChance;

                    //case ShootingTowerUpgrade.BulletForce:
                    //    return Shield1BuildChance;

            }
        }


        public float Cost(TowerUpgrade id)
        {
            switch (id)
            {
                default:
                case TowerUpgrade.Health:
                    return HealthRepairCost;// Chance;

                case TowerUpgrade.MaxHealth:
                    return HealthUpgradeCost;

                    //case ShootingTowerUpgrade.:
                    //    return Shield1BuildChance;

                    //case ShootingTowerUpgrade.BulletForce:
                    //    return Shield1BuildChance;

            }
        }



        //public float Cost(TowerUpgrade id)
        //{
        //    switch (id)
        //    {
        //        default:
        //        case TowerUpgrade.Health:
        //            return HealthUpgradeCost;

        //            //case ShootingTowerUpgrade.:
        //            //    return Shield1BuildCost;

        //            //case ShootingTowerUpgrade.BulletForce:
        //            //    return Shield1BuildCost;

        //    }
        //}



        //public class Resources : BaseResources<Resources>
        ////{
        [SerializeField]
        private Tower[] Towers;

        public Tower GetTower(TowerID type)
        {
            return Towers.Where(x => x.ID == type).FirstOrDefault();
        }


    }
}