using UnityEngine;
using System.Collections;
using Cirrus.Editor;
using System.Collections.Generic;

namespace MTLGJ
{
    // TODO move in the conditions

    //public delegate void OnCollisionEvent(Collision other);
    //public delegate void OnColliderEvent(Collider other);

    public class ColliderListener : MonoBehaviour
    {
        [SerializeField]
        public Cirrus.Events.Event<Collider2D> OnTriggerEnter2DHandler;

        [SerializeField]
        public Cirrus.Events.Event<Collider2D> OnTriggerExit2DHandler;

        [SerializeField]
        public Cirrus.Events.Event<Collider2D> OnTriggerStayHandler;


        public void OnTriggerEnter2D(Collider2D other)
        {
            OnTriggerEnter2DHandler?.Invoke(other);
        }

        public void OnTriggerStay2D(Collider2D other)
        {
            OnTriggerStayHandler?.Invoke(other);
        }    

        public void OnTriggerExit2D(Collider2D other)
        {
            OnTriggerExit2DHandler?.Invoke(other);

        }


    }

}
