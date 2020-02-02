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
        [SerializeField] public int ressourcesQty;
        [SerializeField] int maxRessources;
        [SerializeField] int qtyCollected;
        [SerializeField] public int priceShootingTower;
        [SerializeField] public int priceShieldTower;
        [SerializeField] public int priceRepairTower;

        [SerializeField] Text resourceTxt;

        public override void Update() {
            base.Update();
            //resourceTxt.text = ""+ressourcesQty;
            //Debug.Log("ressources quantity: " + ressourcesQty);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {            
                if (other.CompareTag("collectible")) {
                Debug.Log("collected");
                SoundManagerScript.PlaySound("inventor");
                if (ressourcesQty <=maxRessources) {
                    ressourcesQty += qtyCollected;
                    if (ressourcesQty > maxRessources) { ressourcesQty = maxRessources; }

                    Destroy(other.gameObject);
                }
            }
        }

        
    }
}
