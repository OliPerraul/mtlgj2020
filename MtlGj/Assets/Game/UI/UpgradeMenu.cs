using Cirrus.Extensions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MTLGJ
{
    public class UpgradeMenu : ShopMenu<UpgradeMenu>
    {
        public void Awake()
        {
            foreach (var upg in UpgradeUtils.TowerUpgrades)
            {
                var entry = MenuEntryTemplate.Create(_entriesTransform);

                entry.Name = upg.ToString();

                entry.Upgrade = upg;

                entry.Chance = TowerResources.Instance.Chance(upg);

                entry.Cost = TowerResources.Instance.Cost(upg);

                Entries.Add(
                    entry
                    );
            }

            foreach (var upg in UpgradeUtils.ShootTowerUpgrades)
            {
                var entry = MenuEntryTemplate.Create(_entriesTransform);

                entry.Name = upg.ToString();

                entry.ShootingUpgrade = upg;

                entry.Chance = TowerResources.Instance.Chance(upg);

                entry.Cost = TowerResources.Instance.Cost(upg);

                Entries.Add(
                    entry
                    );
            }

            MenuEntryTemplate.gameObject.SetActive(false);
            //gameObject.SetActive(false);
        }

        public void Start()
        {
            Instance.gameObject.SetActive(false);
        }
    }
}