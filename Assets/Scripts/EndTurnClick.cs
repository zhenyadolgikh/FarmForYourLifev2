using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndTurnClick : MonoBehaviour
{
    

    public void EndTurnOnClick()
    {
        UIManager.instance.EndTurn();
    }
}
