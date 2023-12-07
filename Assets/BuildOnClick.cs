using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildOnClick : MonoBehaviour
{
    public int farmTileIndex = -1;
    public Resource resourceToBuild = Resource.unassignedResource;


    //asdasd
    public void BuildOnClickMethod()
    {
        BuildAction buildAction = new BuildAction();
        buildAction.farmTileIndex = farmTileIndex;
        buildAction.resource = resourceToBuild;

        UIManager.instance.DoAction(buildAction);
    }


}
