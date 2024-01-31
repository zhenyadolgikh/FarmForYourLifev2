using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Special cards/Labour rush")]
public class LabourRush : SpecialCard
{
    public LabourRush(string CardName, string CardDescription) : base(CardName, CardDescription)
    {
    }

    public override EffectLifeTime PlayCard(GameStateLogic.EffectInterface effectInterface)
    {
        return new LabourRushLifeTime(cardName, TypeOfCard.special, effectInterface);
    }
}


public class LabourRushLifeTime : EffectLifeTime
{
    GameStateLogic.EffectInterface effectInterfacet;

    public LabourRushLifeTime(string cardId, TypeOfCard type, GameStateLogic.EffectInterface effectInterfacet) : base(cardId, type)
    {
        this.effectInterfacet = effectInterfacet;
        turnAmount = 3;
    }

    public override void UpdateLifeTime()
    {

        turnAmount -= 1;

        effectInterfacet.AddWorker();

        if(turnAmount == 0)
        {
            lifeTimeEnded = true;
        }
        

    }
}
