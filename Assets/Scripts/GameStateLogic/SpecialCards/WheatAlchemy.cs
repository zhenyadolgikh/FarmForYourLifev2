using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheatAlchemy : SpecialCard
{
    public WheatAlchemy(string CardName, string CardDescription) : base(CardName, CardDescription)
    {
    }


    override public EffectLifeTime PlayCard(GameStateLogic.EffectInterface effectInterface)
    {
        return null;
    }

}
