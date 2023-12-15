using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class VictoryCondition : ScriptableObject
{
    public int amountOfTurns = 0;


    public virtual bool CheckIfVictorious(GameStateLogic.EffectInterface effectInterface)
    {
        return false;
    }
}
