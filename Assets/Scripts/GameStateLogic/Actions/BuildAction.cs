using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildAction : Action
{

    public int farmTileIndex = -1;
    public Resource resource = Resource.money;

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
        if (!(other is BuildAction))
        {
            return false;
        }


        if(farmTileIndex != ((BuildAction)other).farmTileIndex || resource != ((BuildAction)other).resource)
        {
            return false;
        }

        return true;
    }

}
