using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace MTLGJ
{

    public enum TowerID
    {
        Shooting1,
        Shield1
    }

    public abstract class Tower : BaseObject
    {

        public abstract TowerID ID { get; }

        [SerializeField]
        private PolygonCollider2D _occlusionCollider;


        //// Start is called before the first frame update
        //void Start()
        //{

        //}

        //// Update is called once per frame
        //void Update()
        //{

        //}
    }
}