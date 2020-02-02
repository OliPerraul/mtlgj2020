using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cirrus;
using Cirrus.Extensions;

namespace MTLGJ
{
    public class Collectible : MonoBehaviour
    {
        [SerializeField]
        public float Value = 5f;

        public void Awake()
        {
            
        }

        public void OnTriggerEnter2D(Collider2D collision)
        {
            var avatar = collision.transform.GetComponentInParent<Avatar>();

            if (avatar != null)
            {
                Game.Instance.Session.Value.ResourcesAmount.Value += (int)Value;
                gameObject.Destroy();
            }
        }
    }
}
