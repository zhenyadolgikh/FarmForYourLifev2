using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCardsAction : Action
{
    public TypeOfCard typeOfdeck;

    public int moneyCost;
    public override bool Equals(Action other)
    {
        if (ReferenceEquals(null, other))
        {
            return false;
        }
        if (ReferenceEquals(this, other))
        {
            return true;
        }
        if (!(other is MoveCardsAction))
        {
            return false;
        }

        if(typeOfdeck != ((MoveCardsAction)other).typeOfdeck || moneyCost != ((MoveCardsAction)other).moneyCost )
        {
            return false;
        }


        return true;
    }

}
