using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

using UnityEngine.UI;

namespace MTLGJ
{
    public class MenuItemEntry : MonoBehaviour
    {
        public TowerID TowerID;

        public TowerUpgrade Upgrade = TowerUpgrade.Unknown;

        public ShootingTowerUpgrade ShootingUpgrade = ShootingTowerUpgrade.Unknown;

        public string Name
        {
            set
            {
                _nameText.text = value;
            }
        }

        private float _chance = 0;

        public float Chance
        {
            set
            {
                _chance = value;
                _chanceText.text = value.ToString("P0", CultureInfo.InvariantCulture);
            }
        }

        private float _cost = 0;

        public float Cost
        {
            set
            {
                _cost = value;
                _costText.text = Convert.ToInt32(_cost).ToString();
            }
        }




        public Text _nameText;

        public Text _chanceText;

        public Text _costText;

        [SerializeField]
        private Color _selectedColor;

        [SerializeField]
        private Color _defaultColor;

        [SerializeField]
        private Image _background;

        [SerializeField]
        private Image _arrow;

        public bool Selected
        {
            set
            {
                _background.color = value ? _selectedColor : _defaultColor;
                _arrow.gameObject.SetActive(value);
            }
        }


        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}