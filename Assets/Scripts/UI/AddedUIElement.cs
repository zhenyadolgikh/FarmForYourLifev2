using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AddedUIElement 
{
    public HudState hudStateNeeded;

    public List<GameObject> uiElementToInactivate; 


    public AddedUIElement(List<GameObject> gameObject, HudState hudstate)
    {
        uiElementToInactivate = gameObject;
        hudStateNeeded = hudstate;
    }

    public abstract bool RemoveElement();


}
