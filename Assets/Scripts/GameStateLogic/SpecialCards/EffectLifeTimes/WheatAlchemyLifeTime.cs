using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheatAlchemyLifeTime : EffectLifeTime
{
    public override void UpdateLifeTime()
    {
    }

    public override ResourcesAmount ModifyContract(ResourcesAmount contractCost, ResourcesAmount storedResources)
    {   
        return base.ModifyContract(contractCost, storedResources);
    }
}
