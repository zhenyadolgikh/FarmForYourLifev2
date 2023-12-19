using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayCardAction : Action
{

    public int index = -1;
    public TypeOfCard typeOfCard;
    public string cardIdentifier;



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
        if (!(other is PlayCardAction))
        {
            return false;
        }

        if (index != ((PlayCardAction)other).index || typeOfCard != ((PlayCardAction)other).typeOfCard || cardIdentifier != ((PlayCardAction)other).cardIdentifier)
        {
            return false;
        }


        return true;
    }
}
