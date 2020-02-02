using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Cirrus.Extensions;

namespace MTLGJ
{
    public class ShopMenu<T> : MonoBehaviour where T : MonoBehaviour
    {
        public Cirrus.Events.Event<MenuItemEntry> OnItemSelectedHandler;

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
                    var v = FindObjectsOfType<T>();
                    inst = (T) v.FirstOrDefault();
                }

                return inst;
            }
        }

        private bool _moved = false;

        private int _idx = 0;

        public void Scroll(int dir)
        {
            if (Entries[_idx])
            {
                Entries[_idx].Selected = false;
            }

            _idx -= dir;
            _idx = _idx.Mod(Entries.Count);        

            if (Entries[_idx])
            {
                Entries[_idx].Selected = true;
            }
        }


        public void Update()
        {
            if (!Utils.InMenu)
                return;

            if (Entries.Count == 0)
                return;

            if (Input.GetButtonDown("Fire2"))
            {               
                OnItemSelectedHandler?.Invoke(Entries[_idx]);
                return;
            } 

            float verticalInput = Input.GetAxis("Vertical");

            if (verticalInput.IsCloseEnough(0, 0.1f))
            {
                _moved = false;
                return;
            }
            else if(!_moved)
            {
                _moved = true;
                Scroll((int)Mathf.Sign(verticalInput));
            }

        }
    }
}