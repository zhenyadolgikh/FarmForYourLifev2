using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalFastingLifeTime : EffectLifeTime
{
    int turnCount = 3;



    public AnimalFastingLifeTime(string cardId, TypeOfCard type)
    {
        cardIdentifier = cardId;

        typeOfCard = type;
    }

    public override int ModifyAmountToBeEaten(int amountToBeEaten)
    {
        return 0;
    }


    public override void UpdateLifeTime()
    {
        turnCount -= 1;
        if(turnCount == 0) 
        {
            lifeTimeEnded = true;
        }
    }
}
