using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Victory conditions/third level")]

public class ThirdLevel : VictoryCondition
{

    public override bool CheckIfVictorious(GameStateLogic.EffectInterface effectInterface)
    {
        if (effectInterface.GetAmountOfResource(Resource.pigMeat) >= 24 && effectInterface.GetAmountOfResource(Resource.apple) >= 320)
        {
            return true;
        }

        return base.CheckIfVictorious(effectInterface);
    }
}
