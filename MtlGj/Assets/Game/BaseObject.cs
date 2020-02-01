using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MTLGJ
{

    public class BaseObject : MonoBehaviour
    {
        [SerializeField]
        public Transform Transform;

        [SerializeField]
        public SpriteRenderer SpriteRenderer;

        public Vector3Int CellPos => Level.Instance == null ? new Vector3Int() : Level.Instance.Tilemap.WorldToCell(Transform.position);


        // Start is called before the first frame update
        public virtual void Start()
        {
            
        }

        // Update is called once per frame
        public virtual void Update()
        {
           // if(SpriteRenderer != null)
               // SpriteRenderer.sortingOrder = (int)-Transform.position.y;


           
        }
    }
}