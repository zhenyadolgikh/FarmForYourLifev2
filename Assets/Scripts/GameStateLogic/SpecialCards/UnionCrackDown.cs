using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

[CreateAssetMenu(menuName = "Special cards/Union crackdown")]
public class UnionCrackDown : SpecialCard
{
    
    public UnionCrackDown(string CardName, string CardDescription) : base(CardName, CardDescription)
    {

    }

    public override EffectLifeTime PlayCard(GameStateLogic.EffectInterface effectInterface)
    {
        return new UnionCrackDownLifeTime(cardName,TypeOfCard.special);
    }
}

class UnionCrackDownLifeTime : EffectLifeTime
{

    int turnCount = 3;

    public UnionCrackDownLifeTime(string cardId, TypeOfCard type) : base(cardId, type)
    {
    }

    public override void UpdateLifeTime()
    {
        turnCount -= 1;

        if(turnCount < 0)
        {
            lifeTimeEnded = true;
        }
    }

    public override int ModifyWorkerPay(int amountToBePayed)
    {
        return 0; 
    }
}