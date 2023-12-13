using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BuildOnClick : MonoBehaviour
{
    public int farmTileIndex = -1;
    public Resource resourceToBuild = Resource.unassignedResource;

    public TextMeshProUGUI costText;

    //asdasd
    public void BuildOnClickMethod()
    {   

        BuildAction buildAction = new BuildAction();
        buildAction.farmTileIndex = farmTileIndex;
        buildAction.resource = resourceToBuild;

        if(UIManager.instance.IsActionValid(buildAction).wasActionValid == false)
        {
            UIManager.instance.SendErrorMessage(UIManager.instance.IsActionValid(buildAction).errorMessage);
        }
        else
        {
            UIManager.instance.DoAction(buildAction);
        }

        UIManager.instance.PopUIElement();
    }

    public void SetCostText(int cost)
    {
        costText.SetText(cost.ToString());
    }


}
