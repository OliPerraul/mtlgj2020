using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Pathfinding = NesScripts.Controls.PathFind;

namespace MTLGJ
{
    public class Enemy : Character
    {
        public float MoveSpeed = 5f;

        //Path

        [SerializeField]
        private IsometricCharacterRenderer isoRenderer;

        Rigidbody2D rbody;

        public Vector2 Axis = new Vector2();

        private void Awake()
        {
            rbody = GetComponent<Rigidbody2D>();
            isoRenderer = GetComponentInChildren<IsometricCharacterRenderer>();

            var tile = Level.Instance.Tilemap.GetTile(Transform.position.FromWorldToCellPosition());
            //Debug.Log("");

        }

        // Update is called once per frame
        void FixedUpdate()
        {
            Vector2 currentPos = rbody.position;

            Vector2 inputVector = new Vector2(Axis.x, Axis.y);
            inputVector = Vector2.ClampMagnitude(inputVector, 1);
            Vector2 movement = inputVector * MoveSpeed;
            Vector2 newPos = currentPos + movement * Time.fixedDeltaTime;
        
            isoRenderer.SetDirection(movement);
            rbody.MovePosition(newPos);
        }
    }
}