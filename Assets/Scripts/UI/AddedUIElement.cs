using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddedUIElement 
{
    public HudState hudStateNeeded;

    public List<GameObject> uiElementToInactivate; 


    public AddedUIElement(List<GameObject> gameObject, HudState hudstate)
    {
        uiElementToInactivate = gameObject;
        hudStateNeeded = hudstate;
    }

}
