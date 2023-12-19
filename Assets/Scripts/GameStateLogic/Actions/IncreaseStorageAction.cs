using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncreaseStorageAction : Action
{
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
        if (!(other is IncreaseStorageAction))
        {
            return false;
        }


        return true;
    }
}
