using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace MTLGJ
{

    public enum TowerID
    {
        Shooting1,
        Shield1
    }

    public enum TowerUpgrade
    {
        Health
    }

    public abstract class Tower : BaseObject
    {

        public abstract TowerID ID { get; }

        [SerializeField]
        private PolygonCollider2D _occlusionCollider;

        [SerializeField]
        public ColliderListener Colliderlistener;

        public float Health = 1f;

        [SerializeField]
        public healthbar hbar;

        public void Upgrade(TowerUpgrade upgrade)
        {
            //todo
            switch (upgrade)
            {
                case TowerUpgrade.Health:
                    Health++;
                    break;
            }
        }
    }
}