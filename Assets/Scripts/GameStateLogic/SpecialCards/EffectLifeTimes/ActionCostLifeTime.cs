using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionCostLifeTime : EffectLifeTime
{

    private ActionCostingType actionCostType;
    public ActionCostLifeTime(string cardId, TypeOfCard type,ActionCostingType typeAction ) : base(cardId, type)
    {
        actionCostType = typeAction;
    }

    public override void UpdateLifeTime()
    {
        throw new System.NotImplementedException();
    }

    public override int ModifyActionCost(ActionCostingType typeOfAction, int actionCost)
    {
        if(typeOfAction == actionCostType)
        {
            return 0;
        }


        return actionCost;

    }
}
