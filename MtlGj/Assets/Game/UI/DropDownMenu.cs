using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DropDownMenu : MonoBehaviour
{
    List<string> towers = new List<string>() { "Shooting Tower", "Shielding Tower" };

    public Dropdown dropdown;
    public Text selectedName;

    public void Dropdown_IndexChanged(int index) 
    {
        selectedName.text = towers[index];
    }

    // Start is called before the first frame update
    void Start()
    {
        PopulateList();
    }

    void PopulateList() 
    {
        dropdown.AddOptions(towers);
    }
}
