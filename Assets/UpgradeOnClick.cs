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
    public void UpgradeActionClick()
    {
        UIManager uiManager = UIManager.instance;

        IncreaseActionsAction action = new IncreaseActionsAction();

        if(uiManager.IsActionValid(action).wasActionValid)
        {
            uiManager.DoAction(action);
        }
        else
        {
            uiManager.SendErrorMessage(uiManager.IsActionValid(action).errorMessage);
        }      
    }

    public void BuyWorkerClick()
    {
        UIManager uiManager = UIManager.instance;

        BuyWorkerAction action = new BuyWorkerAction();

        if (uiManager.IsActionValid(action).wasActionValid)
        {
            uiManager.DoAction(action);
        }
        else
        {
            uiManager.SendErrorMessage(uiManager.IsActionValid(action).errorMessage);
        }
    }
}
