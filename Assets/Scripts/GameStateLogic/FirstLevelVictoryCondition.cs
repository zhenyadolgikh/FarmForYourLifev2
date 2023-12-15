using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Victory conditions/first level")]
public class FirstLevelVictoryCondition : VictoryCondition
{


    public override bool CheckIfVictorious(GameStateLogic.EffectInterface effectInterface)
    {
        if(effectInterface.GetAmountOfResource(Resource.wheat) >= 360)
        {
            return true; 
        }

        return base.CheckIfVictorious(effectInterface);
    }
}
