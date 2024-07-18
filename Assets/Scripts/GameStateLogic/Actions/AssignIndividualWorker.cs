using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssignIndividualWorker : Action
{
    public int workerId = -1;
    public int farmTileIndex = -1;

    public WorkType workType;

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
        if (!(other is AssignIndividualWorker))
        {
            return false;
        }


        if (farmTileIndex != ((AssignIndividualWorker)other).farmTileIndex || workType != ((AssignIndividualWorker)other).workType || workerId != ((AssignIndividualWorker)other).workerId)
        {
            return false;
        }

        return true;
    }

}
