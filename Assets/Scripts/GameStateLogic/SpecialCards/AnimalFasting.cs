using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Special cards/Animal fasting")]

public class AnimalFasting : SpecialCard
{
    public AnimalFasting(string CardName, string CardDescription) : base(CardName, CardDescription)
    {
    }

    override public EffectLifeTime PlayCard(GameStateLogic.EffectInterface effectInterface)
    {
        return new AnimalFastingLifeTime(cardName, TypeOfCard.special);
    }
}
public class AnimalFastingLifeTime : EffectLifeTime
{



    public AnimalFastingLifeTime(string cardId, TypeOfCard type) : base(cardId, type)
    {
        turnAmount = 3; 
    }

    public override int ModifyAmountToBeEaten(int amountToBeEaten)
    {
        return 0;
    }


    public override void UpdateLifeTime()
    {
        turnAmount -= 1;
        if (turnAmount < 0)
        {
            lifeTimeEnded = true;
        }
    }
}
