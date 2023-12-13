using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeOnClick : MonoBehaviour
{
    public void UpgradeStorageClick()
    {
        UIManager uiManager = UIManager.instance;

        IncreaseStorageAction action = new IncreaseStorageAction();

        if(uiManager.IsActionValid(action).wasActionValid)
        {
            uiManager.DoAction(action);
        }
        else
        {
            uiManager.SendErrorMessage(uiManager.IsActionValid(action).errorMessage);
        }
        
    }
}
