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

        //private bool _moved = false;

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

        bool _lastInputAxisState;

        /// <summary>
        /// Gets the axis input like an on key down event, returning <c>true</c> only 
        /// on the first press, after this return <c>false</c> until the next press. 
        /// Only works for axis between 0 (zero) to 1 (one).
        /// </summary>
        /// <param name="axisName">Axis name configured on input manager.</param>
        protected bool GetAxisInputLikeOnKeyDown(string axisName, out float dist)
        {
            dist = Input.GetAxis(axisName);
            var currentInputValue = Mathf.Abs(dist) > 0.1f;
            //dist = currentInputValue;

            // prevent keep returning true when axis still pressed.
            if (currentInputValue && _lastInputAxisState)
            {
                
                return false;
            }

            _lastInputAxisState = currentInputValue;

            return currentInputValue;
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

            if (GetAxisInputLikeOnKeyDown("Vertical", out float val))
            {
                float verticalInput = val;// Input.GetAxis("Vertical")
                Scroll((int)Mathf.Sign(val));
            }                         

        }
    }
}