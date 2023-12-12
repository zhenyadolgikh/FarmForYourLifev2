using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheatSeasonLifeTme : EffectLifeTime
{
    private int turnCount = 3;




    public WheatSeasonLifeTme(string cardId, TypeOfCard type): base(cardId, type)
    {

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
        turnCount -= 1;

        if(turnCount < 0)
        {
            lifeTimeEnded = true;
        }
    }

}
