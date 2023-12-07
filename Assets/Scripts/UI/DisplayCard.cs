
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using System.ComponentModel;

public class DisplayCard : MonoBehaviour
{
    public string cardName;
    public string cardDescription;

    public TextMeshProUGUI nameText;
    public TextMeshProUGUI descriptionText;


    public int cardIndex = -1;
    public TypeOfCard typeOfCard; 

    public bool inHand = false;


    private UIManager uiManager;

    // Start is called before the first frame update
    void Start()
    {
        uiManager = UIManager.instance;
     //  cardsInDeck = CardDeck.deckSize;
     //
     //  faceUpCard[0] = CardDatabase.cardList[displayId];
        

    }

    public void OnCardClick()
    {
        if(inHand == false)
        {
            AddCardToHandAction addCardToHandAction = new AddCardToHandAction();
            addCardToHandAction.index = cardIndex;
            addCardToHandAction.cardType = typeOfCard;
            
            if(uiManager.IsActionValid(addCardToHandAction).wasActionValid)
            {
                gameObject.SetActive(false);
                uiManager.DoAction(addCardToHandAction);
            }
            else
            {
                uiManager.SendErrorMessage(uiManager.IsActionValid(addCardToHandAction).errorMessage);
            }
        }
        if(inHand)
        {
            PlayCardAction playCardAction = new PlayCardAction();
            playCardAction.index = cardIndex;

            if(uiManager.IsActionValid(playCardAction).wasActionValid)
            {
                gameObject.SetActive(false);
                uiManager.DoAction(playCardAction);
            }
            else
            {
                uiManager.SendErrorMessage(uiManager.IsActionValid(playCardAction).errorMessage);

            }
        }
    }

    public void ResetCard()
    {
        cardName = "";
        cardDescription = "";
    }

    public void SetCardValue(Card card)
    {

      // print("namn texten  " + nameText);
      // print("kortet " + card);
        nameText.SetText(card.getCardName());
        descriptionText.SetText(card.getCardDescription());
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
