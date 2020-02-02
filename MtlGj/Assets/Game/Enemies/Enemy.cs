using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Pathfinding = NesScripts.Controls.PathFind;

namespace MTLGJ
{
    public class Enemy : Character
    {
        public float MoveSpeed = 0.002f;

        //Path

        [SerializeField]
        public IsometricCharacterRenderer isoRenderer;

        public Rigidbody2D rbody;

        public Vector2 Axis = new Vector2(0, 0);

        private void Awake()
        {
            rbody = GetComponent<Rigidbody2D>();
            isoRenderer = GetComponentInChildren<IsometricCharacterRenderer>();
        }

        public void ApplyDamage(float dmg)
        {
            Health -= dmg;
            if (Health < 0)
            {
                Level.Instance.Remove(this, false);
                Health = 0;
                return;
            }
            Flash();
        }

        // Update is called once per frame
        void FixedUpdate()
        {

            //var front =
            // this.Transform.position +
            //isoRenderer.Direction  ;
            //int x = (int)front.x;
            //int y = (int)front.y;
            //int z = (int)front.z;
            //front = new Vector3(x, y, z);

            //var curr = Level.Instance.Tilemap.GetTile(
            //             front.FromWorldToCellPosition());

            // MOVED TO STATEMACHINE
            //if (curr != null && ((GGJTile)curr).ID == TileID.End) { Destroy(this.gameObject); }
        }

        public Vector3 pos;

        public void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(pos, 0.05f);
        }
    }
}