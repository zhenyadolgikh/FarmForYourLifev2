using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Victory conditions/second level")]

public class SecondLevelWinCondition : VictoryCondition
{

    public override bool CheckIfVictorious(GameStateLogic.EffectInterface effectInterface)
    {
        if (effectInterface.GetAmountOfResource(Resource.apple) >= 160 && effectInterface.GetAmountOfResource(Resource.cinnamon) >= 80)
        {
            return true;
        }

        return base.CheckIfVictorious(effectInterface);
    }
}
