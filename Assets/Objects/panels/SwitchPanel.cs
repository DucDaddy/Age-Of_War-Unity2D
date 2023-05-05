using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchPanel : MonoBehaviour
{
    [HideInInspector]public GameObject panelParent;
    public GameObject nextPanel;

    private void OnMouseDown()
    {
        panelParent = this.transform.parent.gameObject;
        if (panelParent.name == "panel1" && gameObject.name == "ui_228" && GameObject.Find("Resources").GetComponent<Resource>().exp < 3000)
        {
            return;
        }
        else if (panelParent.name == "panel1" && gameObject.name == "ui_228" && GameObject.Find("Resources").GetComponent<Resource>().exp >= 3000)
        {
            GameObject.Find("Game Controller").GetComponent<GameController>().NextAge();
        }
        panelParent.SetActive(false);
        nextPanel.SetActive(true);
    }
}
