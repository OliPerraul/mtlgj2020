﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MTLGJ
{

    public class IsometricPlayerMovementController : MonoBehaviour
    {

        public float movementSpeed = 1f;
        IsometricCharacterRenderer isoRenderer;
        float horizontalInput;
        float verticalInput;

        Rigidbody2D rbody;

        private void Awake()
        {
            rbody = GetComponent<Rigidbody2D>();
            isoRenderer = GetComponentInChildren<IsometricCharacterRenderer>();
        }


        // Update is called once per frame
        void FixedUpdate()
        {
            Vector2 currentPos = rbody.position;
            horizontalInput = Utils.InMenu ? 0 : Input.GetAxis("Horizontal");
            verticalInput = Utils.InMenu ? 0 : Input.GetAxis("Vertical");
            Vector2 inputVector = new Vector2(horizontalInput, verticalInput);
            inputVector = Vector2.ClampMagnitude(inputVector, 1);
            Vector2 movement = inputVector * movementSpeed;
            Vector2 newPos = currentPos + movement * Time.fixedDeltaTime;
            isoRenderer.SetDirection(movement);
            rbody.MovePosition(newPos);
        }
    }
}