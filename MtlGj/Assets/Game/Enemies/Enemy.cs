using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Pathfinding = NesScripts.Controls.PathFind;

namespace MTLGJ
{
    public class Enemy : Character
    {
        public Cirrus.Events.Event OnRemovedHandler;

        public float MoveSpeed = 0.002f;

        [SerializeField]
        public float AttackRange = 2f;

        [SerializeField]
        public IsometricCharacterRenderer isoRenderer;

        public Rigidbody2D rbody;

        public Vector2 Axis = new Vector2(0, 0);

        public GameObject wreckage;

        public override void Awake()
        {
            base.Awake();

            rbody = GetComponent<Rigidbody2D>();
            isoRenderer = GetComponentInChildren<IsometricCharacterRenderer>();
        }

        public override void ApplyDamage(float dmg)
        {
            base.ApplyDamage(dmg);
            if (Health.Value == 0)
            {
                Instantiate(wreckage, this.transform.position, this.transform.rotation);
                Level.Instance.RemoveEnemy(this, false);
            }
        }

        void FixedUpdate()
        {

        }

        public Vector3 pos;

        public void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(pos, 0.05f);
        }
    }
}