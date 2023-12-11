using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ContractCard : Card
{
    public int wheatNeeded = -1;
    public int applesNeeded = -1;
    public int cinnamonsNeeded = -1;
    public int pigMeatNeeded = -1;

    public int moneyEarned = -1;

    override public EffectLifeTime PlayCard(GameStateLogic.EffectInterface effectInterface)
    {
        return null;
    }
}
