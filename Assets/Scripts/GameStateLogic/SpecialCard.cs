using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpecialCard : Card
{

    public SpecialCard(int Id, string CardName, string CardDescription)
    {
        id = Id;
        cardName = CardName;
        cardDescription = CardDescription;
    }
}
