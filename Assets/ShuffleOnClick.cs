using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShuffleOnClick : MonoBehaviour
{
    public TypeOfCard typeOfCard;
    public int cost;

    public void OnClick()
    {
        MoveCardsAction action = new MoveCardsAction();

        action.moneyCost = cost;
        action.typeOfdeck = typeOfCard;

        if(UIManager.instance.IsActionValid(action).wasActionValid == false)
        {
            UIManager.instance.SendErrorMessage(UIManager.instance.IsActionValid(action).errorMessage);
        }
        else
        {
            UIManager.instance.DoAction(action);
        }
    }
}
