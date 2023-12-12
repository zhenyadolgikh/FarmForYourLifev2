using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Special cards/Wheat alchemy")]

public class WheatAlchemy : SpecialCard
{
    public WheatAlchemy(string CardName, string CardDescription) : base(CardName, CardDescription)
    {
    }


    override public EffectLifeTime PlayCard(GameStateLogic.EffectInterface effectInterface)
    {
        return new WheatAlchemyLifeTime(cardName, TypeOfCard.special);
    }

}
