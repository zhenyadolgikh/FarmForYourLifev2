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

    override public EffectLifeTime PlayCard()
    {
        return null;
    }
}
