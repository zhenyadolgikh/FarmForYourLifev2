
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using System.ComponentModel;

public class DisplayCard : MonoBehaviour
{
    public List<Card> faceUpCard = new List<Card>();

    public int displayId;

    public int id;
    public string cardName;
    public string cardDescription;

    public TextMeshProUGUI nameText;
    public TextMeshProUGUI descriptionText;

    public GameObject SpecialCardsPanel;
    public static int cardsInDeck;

    // Start is called before the first frame update
    void Start()
    {
     //  cardsInDeck = CardDeck.deckSize;
     //
     //  faceUpCard[0] = CardDatabase.cardList[displayId];
        

    }

    public void ResetCard()
    {
        id = -1;
        cardName = "";
        cardDescription = "";
    }

    public void SetCardValue(Card card)
    {
        nameText.SetText(card.cardName);
        descriptionText.SetText(card.cardDescription);
    }

    // Update is called once per frame
    void Update()
    {
    //    id = faceUpCard[0].id;
    //    cardName = faceUpCard[0].cardName;
    //    cardDescription = faceUpCard[0].cardDescription;
    //
    //    nameText.text = " " + cardName;
    //    descriptionText.text = " " + cardDescription;
    //
    //    if(this.tag == "Clone")
    //    {
    //        faceUpCard[0] = CardDeck.staticDeck[cardsInDeck - 1];
    //        cardsInDeck -= 1;
    //        CardDeck.deckSize -= 1;
    //        this.tag = "Untagged";            
    //    }

    }

}
