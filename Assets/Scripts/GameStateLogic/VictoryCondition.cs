using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class VictoryCondition : ScriptableObject
{



    public virtual bool CheckIfVictorious(GameStateLogic.EffectInterface effectInterface)
    {
        return false;
    }
}
