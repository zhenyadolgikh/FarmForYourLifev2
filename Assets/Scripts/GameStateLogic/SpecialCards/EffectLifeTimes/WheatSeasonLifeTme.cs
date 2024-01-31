using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheatSeasonLifeTme : EffectLifeTime
{




    public WheatSeasonLifeTme(string cardId, TypeOfCard type): base(cardId, type)
    {
        turnAmount = 3; 

        
    }


    public override int ModifyResourcesGenerated(Resource resource, int resourcesToGenerate)
    {
        if(resource == Resource.wheat)
        {
            if(resourcesToGenerate == 40)
            {
                return 80;
            }
        }


        return resourcesToGenerate;
    }


    public override void UpdateLifeTime()
    {
        turnAmount -= 1;

        if(turnAmount < 0)
        {
            lifeTimeEnded = true;
        }
    }

}
