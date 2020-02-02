using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class helpMenu : MonoBehaviour
{

    public GameObject panel;
    // Start is called before the first frame update
    public void apparaitre() {
        this.panel.SetActive(true);

    }

    public void disapear()
    {
        this.panel.SetActive(false);
    }
}
