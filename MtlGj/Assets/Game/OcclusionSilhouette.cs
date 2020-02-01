using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MTLGJ
{

    public class OcclusionSilhouette : MonoBehaviour
    {
        //[SerializeField]
        //private SpriteRenderer spr;

        [SerializeField]
        private SpriteRenderer _sprite;

        [SerializeField]
        private Material _occlusionMaterial;
    
        [SerializeField]
        private Material _normalMaterial;

        //[SerializeField]
        //private Character _character;

        // Update is called once per frame

        public void Awake()
        {
            _sprite.material = _normalMaterial;
        }

        public void OnTriggerStay2D(Collider2D collider)
        {
            //Debug.Log("asd");

            var tow = collider.GetComponentInParent<Tower>();

            if (tow == null)
                return;

            if (tow.Transform.position.y > transform.position.y)
                return;

            _sprite.material = _occlusionMaterial;
        }

        public void OnTriggerExit2D(Collider2D other)
        {

            _sprite.material = _normalMaterial;
            
        }

    }
}