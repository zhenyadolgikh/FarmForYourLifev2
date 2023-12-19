using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpCardAction : Action
{

    public int cardIndex;
    public TypeOfCard cardType;


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
        if (!(other is PickUpCardAction))
        {
            return false;
        }

        if (cardIndex != ((PickUpCardAction)other).cardIndex || cardType != ((PickUpCardAction)other).cardType)
        {
            return false;
        }


        return true;
    }

}
