using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName ="Special cards/Exponentional animal growth")]
public class ExponentialAnimalGrowth : SpecialCard
{
   public ExponentialAnimalGrowth(string CardName, string CardDescription, GameStateLogic.EffectInterface effectInterface) : base(CardName, CardDescription)
    {

    }

    public override EffectLifeTime PlayCard(GameStateLogic.EffectInterface effectInterface)
    {
        effectInterface.DoubleAnimals();
        return base.PlayCard(effectInterface);
    } 
}
