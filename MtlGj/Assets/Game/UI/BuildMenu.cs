using Cirrus.Extensions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MTLGJ
{
    public class BuildMenu : ShopMenu<BuildMenu>
    {
        public void Awake()
        {
            foreach (var upg in BuildUtils.Towers)
            {
                var entry = MenuEntryTemplate.Create(_entriesTransform);

                entry.Name = upg.ToString();

                entry.TowerID = upg;

                entry.Chance = TowerResources.Instance.Chance(upg);

                entry.Cost = TowerResources.Instance.Cost(upg);

                Entries.Add(
                    entry
                    );
            }

            foreach (var ent in Entries)
            {
                ent.Selected = false;
            }

            if (Entries.Count != 0)
            {
                Entries[0].Selected = true;
            }

            MenuEntryTemplate.gameObject.SetActive(false);
        }

        public void Start()
        {
            gameObject.SetActive(false);
        }
    }
}