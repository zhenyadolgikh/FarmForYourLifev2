using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "Special cards/Wheat season")]
public class WheatSeason : SpecialCard
{

    public WheatSeason( string CardName, string CardDescription) : base(CardName, CardDescription)
    {

    }

    override public EffectLifeTime PlayCard(GameStateLogic.EffectInterface effectInterface)
    {   
        

        

        return new WheatSeasonLifeTme(cardName,TypeOfCard.special);
    }

}
