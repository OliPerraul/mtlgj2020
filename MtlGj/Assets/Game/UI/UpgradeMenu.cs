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
       
        }

        public void DeleteOldOption()
        {
            foreach (var entr in Entries)
            {
                if (entr == null)
                    continue;

                entr.gameObject.Destroy();
            }

            Entries.Clear();

            MenuEntryTemplate.gameObject.SetActive(true);
        }

        public void OpenShootingTowerUpgrades()
        {
            DeleteOldOption();

            UpgradeMenu.Instance.gameObject.SetActive(true);

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

            DoOpen();
        }

        public void OpenShieldTowerUpgrades()
        {
            DeleteOldOption();

            UpgradeMenu.Instance.gameObject.SetActive(true);

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

            MenuEntryTemplate.gameObject.SetActive(false);

            DoOpen();
        }


        public void DoOpen()
        {
            foreach (var entr in Entries)
            {
                entr.Selected = false;
            }

            if (Entries.Count == 0) return;

            Entries[0].Selected = true;
        }



        public void Start()
        {
            Instance.gameObject.SetActive(false);
        }
    }
}