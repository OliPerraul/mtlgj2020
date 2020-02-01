//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//namespace MTLGJ
//{

//    public class EnemyController : MonoBehaviour
//    {
//        public float movementSpeed = 5000f;
//        IsometricCharacterRenderer isoRenderer;

//        Rigidbody2D rbody;

//        public Vector2 Axis = new Vector2();


//        private void Awake()
//        {
//            rbody = GetComponent<Rigidbody2D>();
//            isoRenderer = GetComponentInChildren<IsometricCharacterRenderer>();
//        }


//        // Update is called once per frame
//        void FixedUpdate()
//        {
//            Vector2 currentPos = rbody.position;
//            float horizontalInput = Input.GetAxis("Horizontal");
//            float verticalInput = Input.GetAxis("Vertical");
//            Vector2 inputVector = new Vector2(horizontalInput, verticalInput);
//            inputVector = Vector2.ClampMagnitude(inputVector, 1);
//            Vector2 movement = inputVector * movementSpeed;
//            Vector2 newPos = currentPos + movement * Time.fixedDeltaTime;
//            isoRenderer.SetDirection(movement);
//            rbody.MovePosition(newPos);
//        }
//    }
//}