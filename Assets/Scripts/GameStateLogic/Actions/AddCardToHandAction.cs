using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddCardToHandAction : Action
{

    public int index;
    public TypeOfCard cardType;
    //public Card card;



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
        if (!(other is AddCardToHandAction))
        {
            return false;
        }

        if (index != ((AddCardToHandAction)other).index || cardType != ((AddCardToHandAction)other).cardType )
        {
            return false;
        }


        return true;
    }

}
