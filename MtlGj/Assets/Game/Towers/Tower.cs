using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace MTLGJ
{
    public abstract class Tower : BaseObject
    {
        public Cirrus.Events.ObservableInt Level = new Cirrus.Events.ObservableInt(1);

        public abstract TowerID ID { get; }

        public abstract void OpenUpgradMenu();

        [SerializeField]
        private PolygonCollider2D _occlusionCollider;

        [SerializeField]
        public ColliderListener Colliderlistener;

        public float Health = 1f;

        [SerializeField]
        public healthbar hbar;


        public bool TakeSufficientFunds(TowerUpgrade upgr)
        {
            if (Game.Instance.Session.Value.ResourcesAmount.Value < (int)TowerResources.Instance.Cost(upgr))
                return false;


            Game.Instance.Session.Value.ResourcesAmount.Value -= (int)TowerResources.Instance.Cost(upgr);
            Game.Instance.Session.Value.ResourcesAmount.Value = Game.Instance.Session.Value.ResourcesAmount.Value < 0 ? 0 : Game.Instance.Session.Value.ResourcesAmount.Value;

            return true;
        }


        public bool TakeSufficientFunds(ShootingTowerUpgrade upgr)
        {
            if (Game.Instance.Session.Value.ResourcesAmount.Value < (int)TowerResources.Instance.Cost(upgr))
                return false;

            Game.Instance.Session.Value.ResourcesAmount.Value -= (int)TowerResources.Instance.Cost(upgr);
            Game.Instance.Session.Value.ResourcesAmount.Value = Game.Instance.Session.Value.ResourcesAmount.Value < 0 ? 0 : Game.Instance.Session.Value.ResourcesAmount.Value;

            return true;
        }

        public void Upgrade(TowerUpgrade upgrade)
        {
            if (upgrade == TowerUpgrade.Unknown)
                return;

            if (TakeSufficientFunds(upgrade))
                return;

            switch (upgrade)
            {
                case TowerUpgrade.Health:
                    Health++;
                    break;
            }

            Level.Value++;
        }

        public virtual void Upgrade(ShootingTowerUpgrade upgrade)
        {

        }

    }
}