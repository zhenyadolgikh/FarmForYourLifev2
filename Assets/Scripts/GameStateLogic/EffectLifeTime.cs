using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EffectLifeTime
{

    public bool lifeTimeEnded = false;

    public string cardIdentifier;

    public TypeOfCard typeOfCard; 


    public  EffectLifeTime(string cardId, TypeOfCard type)
    {
        cardIdentifier = cardId;
        typeOfCard = type;
    }

    public virtual int ModifyResourcesGenerated(Resource resource, int resourcesToGenerate)
    {
        return resourcesToGenerate;
    }
    public virtual int ModifyWorkerPay(int amountToBePayed)
    { 
        return amountToBePayed; 
    }
    public  virtual int ModifyAmountToBeEaten(int amountToBeEaten)
    {
        return amountToBeEaten;
    }
    public virtual ResourcesAmount ModifyContract(ResourcesAmount contractCost, ResourcesAmount storedResources)
    {
        return contractCost;
    }
    public virtual int ModifyActionCost( ActionCostingType typeOfAction,int actionCost)
    {
        return actionCost;
    }

    public abstract void UpdateLifeTime();


}