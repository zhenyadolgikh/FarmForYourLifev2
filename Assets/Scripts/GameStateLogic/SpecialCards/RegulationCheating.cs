using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Special cards/Regulation cheating")]
public class RegulationCheating : SpecialCard
{

    public RegulationCheating(string CardName, string CardDescription) : base(CardName, CardDescription)
    {     
    }

    override public EffectLifeTime PlayCard(GameStateLogic.EffectInterface effectInterface)
    {
        effectInterface.IncreaseActions(1);

        return new ActionCostLifeTime(cardName, TypeOfCard.special, ActionCostingType.build);

    }
}
