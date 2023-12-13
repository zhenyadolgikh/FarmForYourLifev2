using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheatAlchemyLifeTime : EffectLifeTime
{
    public WheatAlchemyLifeTime(string cardId, TypeOfCard type) : base(cardId, type)
    {
    }

    public override void UpdateLifeTime()
    {
    }

    public override ResourcesAmount ModifyContract(ResourcesAmount contractCost, ResourcesAmount storedResources)
    {

        int wheatNeeded = 0; 
        ResourcesAmount potenialContract = contractCost;

        if(contractCost.wheatCost != -1)
        {
            wheatNeeded = storedResources.wheatCost;
        }
        if(contractCost.appleCost != -1) 
        {
            if(contractCost.appleCost > storedResources.appleCost ) 
            {
                wheatNeeded += contractCost.appleCost - storedResources.appleCost;
                potenialContract.appleCost = storedResources.appleCost;
            }
        }
        if(contractCost.cinnamonCost != -1) 
        {
            if(contractCost.cinnamonCost > storedResources.cinnamonCost) 
            {
                wheatNeeded += contractCost.cinnamonCost - storedResources.cinnamonCost;
                potenialContract.cinnamonCost = storedResources.cinnamonCost;

            }
        }

        //
        if(contractCost.pigMeatCost != -1) 
        {
            if(contractCost.pigMeatCost > storedResources.pigMeatCost) 
            {
                wheatNeeded += (contractCost.pigMeatCost - storedResources.pigMeatCost)/10;
                potenialContract.pigMeatCost = storedResources.pigMeatCost;

            }
        }
        potenialContract.wheatCost = wheatNeeded;
        //
        if(wheatNeeded == 0)
        {
            wheatNeeded = -1;
        }

        if(wheatNeeded <= storedResources.wheatCost && wheatNeeded != contractCost.wheatCost)
        {
            lifeTimeEnded = true;
        }

        return potenialContract;
    }
}
