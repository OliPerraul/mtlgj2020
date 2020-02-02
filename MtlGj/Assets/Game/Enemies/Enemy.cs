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

        public float AttackSpeed = 0.004f;

        public float ChanceAttack = 0.4f;

        public float ChanceDecideOnTowerPlaced = 0.2f;

        public float ChanceMarch = 0.2f;

        public float Damage = 5f;

        public float AttackFrequence = 5f;

        [SerializeField]
        public float AttackRange = 2f;

        [SerializeField]
        public IsometricCharacterRenderer isoRenderer;

        public Rigidbody2D rbody;

        public Vector2 Axis = new Vector2(0, 0);

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
                Level.Instance.RemoveEnemy(this, false);
            }
        }

        public void FixedUpdate() { }

        public Vector3 pos;

        public Cirrus.Events.Event<Collision2D> OnCollisionEnter2DHandler;

        public void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(pos, 0.05f);
        }

        public void OnCollisionEnter2D(Collision2D collision)
        {
            OnCollisionEnter2DHandler?.Invoke(collision);
        }
    }
}