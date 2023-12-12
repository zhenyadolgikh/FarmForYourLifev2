using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
public class SpecialCard : Card
{

    public SpecialCard(string CardName, string CardDescription)
    {
        cardName = CardName;
        cardDescription = CardDescription;
    }

    virtual public EffectLifeTime PlayCard(GameStateLogic.EffectInterface effectInterface)
    {
        return null;
    }
}
