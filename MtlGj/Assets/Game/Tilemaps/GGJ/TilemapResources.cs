using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TilemapResources : Cirrus.Resources.Resources
{
    //public class Resources : BaseResources<Resources>
    //{
        [SerializeField]
        private GGJTile[] tiles;

        public GGJTile GetTile(TileID type)
        {
            return tiles.Where(x => x.ID == type).FirstOrDefault();
        }


}
