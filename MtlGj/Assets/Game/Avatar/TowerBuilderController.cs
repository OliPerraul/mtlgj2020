using Cirrus.Extensions;
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

        bool isMenuActive = false;
        public Canvas turretMenu;

        IsometricCharacterRenderer isoRenderer;

        Rigidbody2D rbody;

        //public Tilemap tilemap;

        //public Tile start;
        float positionX;
        float positionY;


        public TowerID[] _towerInventory = new TowerID[] {
            TowerID.Shooting1
            //TowerID.Shield1,
            //TowerID.Shooting1
        };


        [SerializeField]
        private int selectedTowerIndex = 0;

        private void Start()
        {
            //rbody = avatar.GetComponent<Rigidbody2D>();
            // scriptPosition = avatar.GetComponent<IsometricPlayerMovementController>();
            isoRenderer = avatar.GetComponentInChildren<IsometricCharacterRenderer>();

        }

        private bool settile = false;

        private Vector3Int oldCellPos = new Vector3Int(-1, -1, -1);

        private GGJTile oldTile;

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
                //turretMenu.enabled = !isMenuActive;
                isMenuActive = !isMenuActive;

                CreateTower(0);
                           
            }
        }

        public void CreateTower(int selectedTowerIndex)
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

            Level.Instance.SetBuildingCell(front.FromWorldToCellPosition(), true);

            var tower = TowerResources.Instance.GetTower(_towerInventory[selectedTowerIndex]);
           
                tower.gameObject.Create(
                    front.FromWorldToCellPosition().FromCellToWorldPosition(), 
                    Level.Instance.transform);
        }
        

        //void CreateShooting() { CreateTower(0); }
        //void CreateShield() { CreateTower(1); }
    }
//}