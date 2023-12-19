using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public abstract class Action : IEquatable<Action>
{
    public abstract bool Equals(Action other);
}
