using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddedAssignWorkers : AddedUIElement
{
    public AddedAssignWorkers(List<GameObject> gameObject, HudState hudstate) : base(gameObject, hudstate)
    {
    }


    public GameObject panel;
    public GameObject text;


    public override bool RemoveElement()
    {
        if(panel.activeSelf)
        {
            panel.SetActive(false);

            return false;
        }
        else
        {
            text.SetActive(false);
            UIManager.instance.Refresh();
            return true;
        }
    }

}
