using Cirrus.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace MTLGJ
{
    public abstract class Tower : BaseObject
    {
        //public PlayfulSystems.ProgressBar.ProgressBarPro progressBar;

        [SerializeField]
        private UnityEngine.UI.Text _levelUpText;

        [SerializeField]
        private UnityEngine.UI.Text _levelText;

        private float _levelUptime = 1f;

        private Cirrus.Timer _levelUpTimer = new Cirrus.Timer(start:false, repeat:false);

        public Cirrus.Events.ObservableInt Level = new Cirrus.Events.ObservableInt(1);

        public abstract TowerID ID { get; }

        public abstract void OpenUpgradMenu();

        [SerializeField]
        private PolygonCollider2D _occlusionCollider;

        [SerializeField]
        public ColliderListener Colliderlistener;

        [SerializeField]
        private Sprite _spriteUpgrade;    

        //[SerializeField]
        //private Sprite _spriteNormal;

        public override void Awake()
        {
            base.Awake();

            _levelUpText.gameObject.SetActive(false);
            Level.OnValueChangedHandler += OnLevelUp;
            _levelUpTimer.OnTimeLimitHandler += OnLevelUpTimeout;
        }

        public override void Start()
        {
            base.Start();

            OnLevelUp(1);
        }

        private void OnLevelUpTimeout()
        {
            _levelUpText.gameObject.SetActive(false);
        }

        public void OnLevelUp(int lv)
        {
            if (lv >= 5)
            {
                if(SpriteRenderer != null)
                SpriteRenderer.sprite = _spriteUpgrade;
            }

            _levelUpText.gameObject.SetActive(false);
            //_levelUpText.text = "Level up!";
            _levelText.text = "lv " + lv;
            _levelUpTimer.Start(_levelUptime);            
        }

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

            if (!TakeSufficientFunds(upgrade))
                return;

            if (Cirrus.Numeric.Chance.CheckIsTrue(
                TowerResources.Instance.Chance(upgrade)
                ))
            {
                Explode();
                return;
            }

            switch (upgrade)
            {
                case TowerUpgrade.Health:
                    Health.Value++;
                    break;
                case TowerUpgrade.MaxHealth:
                    MaxHealth.Value++;
                    break;
            }

            Level.Value++;
        }

        public void Explode()
        {
            CameraShake.Instance.Shake();
            var expl = TowerResources.Instance.Explosion.gameObject.Create(Transform.position, MTLGJ.Level.Instance.transform);
            expl.transform.position.SetZ(0);
            MTLGJ.Level.Instance.RemoveTower(this, transform.position.FromWorldToCellPosition());
            MTLGJ.Level.Instance.SetBuildingCell(transform.position.FromWorldToCellPosition(), false);
            gameObject.Destroy();
        }

        public void SmallExplode()
        {
            CameraShake.Instance.Shake();
            var expl = TowerResources.Instance.SmallExplosion.gameObject.Create(Transform.position, MTLGJ.Level.Instance.transform);
            expl.transform.position.SetZ(0);
            MTLGJ.Level.Instance.RemoveTower(this, transform.position.FromWorldToCellPosition());
            MTLGJ.Level.Instance.SetBuildingCell(transform.position.FromWorldToCellPosition(), false);
            gameObject.Destroy();
        }

        public override void ApplyDamage(float dmg)
        {
            base.ApplyDamage(dmg);

            if (Health.Value == 0)
            {
                SmallExplode();
            }
        }

        public virtual void Upgrade(ShootingTowerUpgrade upgrade)
        {

        }

    }
}