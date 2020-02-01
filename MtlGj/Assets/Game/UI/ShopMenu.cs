using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace MTLGJ
{
    public class ShopMenu<T> : MonoBehaviour where T : MonoBehaviour
    {
        public List<MenuItemEntry> Entries = new List<MenuItemEntry>();

        public MenuItemEntry MenuEntryTemplate;

        [SerializeField]
        protected Transform _entriesTransform;

        private static T inst;

        public static T Instance
        {
            get
            {
                if (inst == null)
                {
                    var v = Resources.FindObjectsOfTypeAll<T>();
                    inst = (T) v.FirstOrDefault();
                }

                return inst;
            }
        }       
    }
}