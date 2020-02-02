using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace MTLGJ
{
    public class healthbar : MonoBehaviour
    {
        private Transform bar;

        private void Awake()
        {
            bar = transform.Find("bar");
        }

        public void SetSize(float sizeNormalized)
        {
            bar.localScale = new Vector3(sizeNormalized, 1f);
        }

        // Update is called once per frame
        public void SetColor(Color color)
        {
            bar.Find("BarSprite").GetComponent<SpriteRenderer>().color = color;
        }
    }
}