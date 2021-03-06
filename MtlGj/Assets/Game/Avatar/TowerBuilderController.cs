﻿using Cirrus.Extensions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Tilemaps;
using Pathfinding = NesScripts.Controls.PathFind;
using UnityEngine.UI;

namespace MTLGJ
{
    public class TowerBuilderController : MonoBehaviour
    {
        [SerializeField]
        public Avatar avatar;
        //public GameObject myPrefab;

        [SerializeField]
        private float _buildRange = 2f;

        //bool isMenuActive = false;
        //public Canvas turretMenu;

        IsometricCharacterRenderer isoRenderer;

        Rigidbody2D rbody;

        //public Tilemap tilemap;

        //public Tile start;
        float positionX;
        float positionY;

        [SerializeField]
        private int selectedTowerIndex = 0;

        public void Awake()
        {
            BuildMenu.Instance.OnItemSelectedHandler += OnBuildSelected;
            UpgradeMenu.Instance.OnItemSelectedHandler += OnUpgradeSelected;
        }

        private GameObject _activeMenu;

        public void OnBuildSelected(MenuItemEntry menuItem)
        {
            CreateTower(menuItem.TowerID);

            if (_activeMenu != null)
            {
                CloseMenu();
                return;
            }
        }

        public void OnUpgradeSelected(MenuItemEntry menuItem)
        {
            _upgradedTower.Upgrade(menuItem.ShootingUpgrade);
            _upgradedTower.Upgrade(menuItem.Upgrade);

            if (_activeMenu != null)
            {
                CloseMenu();
                return;
            }
        }

        public void CloseMenu()
        {
            _upgradedTower = null;
            Utils.InMenu = false;
            _activeMenu.SetActive(false);
            _activeMenu = null;
            return;
        }

        private void Start()
        {
            isoRenderer = avatar.GetComponentInChildren<IsometricCharacterRenderer>();
        }

        private bool settile = false;

        private Vector3Int oldCellPos = new Vector3Int(-1, -1, -1);

        private GGJTile oldTile;

        private Tower _upgradedTower;

        void Update()
        {
            //if()
            //if(isoRenderer.Di)
            // TODO NOT HARDCOEE
            var front =
                avatar.Transform.position +
                isoRenderer.Direction * _buildRange;

            if (settile)
            {
                Level.Instance.TilemapGuide.SetTile(
                    oldCellPos,
                    oldTile);
            }

            var cellPos = front.FromWorldToCellPosition();
            oldCellPos = cellPos;
            oldTile = Level.Instance.TilemapGuide.GetTile(cellPos) == null ?
                null :
                (GGJTile)Level.Instance.TilemapGuide.GetTile(cellPos);

            Level.Instance.TilemapGuide.SetTile(
                cellPos,
                TilemapResources.Instance.GetTile(TileID.Guide));

            settile = true;

            if (Input.GetButtonDown("Fire1"))
            {
                if (_activeMenu != null)
                {
                    CloseMenu();
                    return;
                }

                //var front =
                //  avatar.Transform.position +
                //  isoRenderer.Direction * _buildRange;

                var curr = Level.Instance.Tilemap.GetTile(
                            front.FromWorldToCellPosition());

                //turretMenu.enabled = !isMenuActive;
                //isMenuActive = !isMenuActive;
                //if (curr != null && ((GGJTile)curr).ID == TileID.Building)
                //    return;

                if (curr == null)
                    return;

                if (((GGJTile)curr).ID == TileID.Character)
                    return;

                if (((GGJTile)curr).ID == TileID.End)
                    return;

                if (((GGJTile)curr).ID == TileID.Start)
                    return;

                if (((GGJTile)curr).ID == TileID.Building)
                {
                    if (Level.Instance.TryGetTower(front.FromWorldToCellPosition(), out Tower tow))
                    { 
                        tow.OpenUpgradMenu();
                        _upgradedTower = tow;
                       
                        _activeMenu = UpgradeMenu.Instance.gameObject;
                        Utils.InMenu = true;
                        return;
                    }
                }

                if (((GGJTile)curr).ID == TileID.Empty)
                {
                    BuildMenu.Instance.gameObject.SetActive(true);
                    _activeMenu = BuildMenu.Instance.gameObject;
                    Utils.InMenu = true;
                    return;
                }

            }
        }

        public void CreateTower(TowerID towerid)
        {
            var front =
              avatar.Transform.position +
              isoRenderer.Direction * _buildRange;

            var curr = Level.Instance.Tilemap.GetTile(
                     front.FromWorldToCellPosition());

            if (curr != null && ((GGJTile)curr).ID == TileID.Building)
                return;

            if (curr != null && ((GGJTile)curr).ID == TileID.Character)
                return;

            if (curr != null && ((GGJTile)curr).ID == TileID.End)
                return;

            if (curr != null && ((GGJTile)curr).ID == TileID.Start)
                return;

            if (Game.Instance.Session.Value.ResourcesAmount.Value < (int)TowerResources.Instance.Cost(towerid))
                return;

            var tower = TowerResources.Instance.GetTower(towerid);

            if (tower == null)
                return;

            Game.Instance.Session.Value.ResourcesAmount.Value -= (int)TowerResources.Instance.Cost(towerid);
            Game.Instance.Session.Value.ResourcesAmount.Value = Game.Instance.Session.Value.ResourcesAmount.Value < 0 ? 0 : Game.Instance.Session.Value.ResourcesAmount.Value;

            Level.Instance.SetBuildingCell(front.FromWorldToCellPosition(), true);

            var newtow = tower.gameObject.Create(
                front.FromWorldToCellPosition().FromCellToWorldPosition(),
                Level.Instance.transform).GetComponent<Tower>();

            SoundManagerScript.PlaySound("Hammer");
            Level.Instance.AddTower(newtow, front.FromWorldToCellPosition());
        }
    }
}

