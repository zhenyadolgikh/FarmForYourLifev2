using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssignWorkersAction : Action
{

    public List< Tuple<int,WorkType>> workAssigned = new List<Tuple<int, WorkType>>();


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
        if (!(other is AssignWorkersAction))
        {
            return false;
        }


        AssignWorkersAction otherCasted = (AssignWorkersAction)other;
        
        for(int i = 0; i < workAssigned.Count; i++)
        {
            if (workAssigned[0].Item1 != otherCasted.workAssigned[0].Item1 || workAssigned[0].Item2 != otherCasted.workAssigned[0].Item2)
            {
                return false;
            }
        }


        return true;
    }
}
