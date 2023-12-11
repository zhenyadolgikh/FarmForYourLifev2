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
