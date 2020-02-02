using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class helpMenu : MonoBehaviour
{

    public GameObject panel;
    // Start is called before the first frame update
    public void apparaitre() {
        this.panel.SetActive(true);
        SoundManagerScript.PlaySound("magical_1");

    }

    public void disapear()
    {
        SoundManagerScript.PlaySound("magical_1");
        this.panel.SetActive(false);
    }
}
