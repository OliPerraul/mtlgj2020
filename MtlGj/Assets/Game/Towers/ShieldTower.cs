using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTLGJ
{
    class ShieldTower : Tower
    {
        public override TowerID ID => TowerID.Shield1;

        public override void OpenUpgradMenu()
        {
            UpgradeMenu.Instance.OpenShieldTowerUpgrades();
        }

        public override void Upgrade(ShootingTowerUpgrade upgrade)
        {
            base.Upgrade(upgrade);
        }
    }
}
