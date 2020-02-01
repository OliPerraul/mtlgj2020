using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace MTLGJ
{

    public class TowerResources : Cirrus.Resources.BaseResourceManager<TowerResources>
    {
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