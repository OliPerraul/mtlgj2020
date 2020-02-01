using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace MTLGJ
{
    public class Avatar : Character
    {
        [SerializeField] int ressourcesQty;
        [SerializeField] int maxRessources;
        [SerializeField] int qtyCollected;
        [SerializeField] int qtySpent;
        [SerializeField] Text resourceTxt;

        public override void Update() {
            base.Update();
            //resourceTxt.text = ""+ressourcesQty;
            //Debug.Log("ressources quantity: " + ressourcesQty);
        }

        private void OnTriggerEnter2D(Collider2D other) { 
            
                if (other.CompareTag("collectible")) {
                Debug.Log("collected");
                if (ressourcesQty <=maxRessources) {
                    ressourcesQty += qtyCollected;
                    if (ressourcesQty > maxRessources) { ressourcesQty = maxRessources; }

                    Destroy(other.gameObject);
                }
            }
        }

        public void ressourcesSpent() { ressourcesQty -= qtySpent; }
    }
}
