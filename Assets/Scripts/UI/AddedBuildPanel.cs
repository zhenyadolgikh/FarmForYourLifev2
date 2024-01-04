using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddedBuildPanel : AddedUIElement
{
    public AddedBuildPanel(List<GameObject> gameObject, HudState hudstate) : base(gameObject, hudstate)
    {
    }

    public override bool RemoveElement()
    {

        foreach(GameObject gameObject in uiElementToInactivate)
        {
            gameObject.SetActive(false);
        }
        return true;
    }

}
