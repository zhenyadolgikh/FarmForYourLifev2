using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeOnClick : MonoBehaviour
{
    public void UpgradeStorageClick()
    {
        UIManager uiManager = UIManager.instance;

        IncreaseStorageAction action = new IncreaseStorageAction();
        IsActionValidMessage message = uiManager.IsActionValid(action);
        if (message != null && message.wasActionValid)
        {
            uiManager.DoAction(action);
        }
        else
        {   if (message == null)
                return;
            uiManager.SendErrorMessage(message.errorMessage);
        }      
    }   
    public void UpgradeActionClick()
    {
        UIManager uiManager = UIManager.instance;

        IncreaseActionsAction action = new IncreaseActionsAction();
        IsActionValidMessage message = uiManager.IsActionValid(action);
        if (message != null && message.wasActionValid)
        {
            uiManager.DoAction(action);
        }
        else
        {
            if (message == null)
                return;
            uiManager.SendErrorMessage(message.errorMessage);
        }      
    }

    public void BuyWorkerClick()
    {
        UIManager uiManager = UIManager.instance;

        BuyWorkerAction action = new BuyWorkerAction();
        IsActionValidMessage message = uiManager.IsActionValid(action);

        if (message != null && message.wasActionValid)
        {
            uiManager.DoAction(action);
        }
        else
        {
            if (message == null)
                return;
            uiManager.SendErrorMessage(message.errorMessage);
        }
    }
}
