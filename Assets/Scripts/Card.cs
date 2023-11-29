using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting.ReorderableList;
[System.Serializable]

public class Card
{
    public int id;
    public string cardName;
    public string cardDescription;

    public Card()
    {

    }

    public Card(int Id, string CardName, string CardDescription)
    {
        id = Id;
        cardName = CardName;
        cardDescription = CardDescription;
    }

    
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
