using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting.ReorderableList;


[CreateAssetMenu]
public class Card : ScriptableObject
{
    public string cardName;
    public string cardDescription;

    public Card()
    {

    }

    public Card(string CardName, string CardDescription)
    {
        cardName = CardName;
        cardDescription = CardDescription;
    }


    public string getCardName() { return cardName; }
    public string getCardDescription() {  return cardDescription; }

}
