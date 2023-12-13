using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Special cards/Brainstorm")]
public class BrainStorm : SpecialCard
{
    public BrainStorm(string CardName, string CardDescription) : base(CardName, CardDescription)
    {
    }


    public override EffectLifeTime PlayCard(GameStateLogic.EffectInterface effectInterface)
    {
        effectInterface.BrainStormShuffleCards();

        return base.PlayCard(effectInterface);
    }
}
