
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

    public Sprite specialCardSprite;
    public Sprite contractCardSprite;

    public Image borderImage;


    [SerializeField] private ResourceIndicator wheatIndicator;
    [SerializeField] private ResourceIndicator appleIndicator;
    [SerializeField] private ResourceIndicator cinnamonIndicator;
    [SerializeField] private ResourceIndicator pigMeatIndicator;
    [SerializeField] private ResourceIndicator moneyIndicator;


    public GameObject contractCardVariant;
    public GameObject specialCardVariant;


    // Start is called before the first frame update
    void Start()
    {
        uiManager = UIManager.instance;
     //  cardsInDeck = CardDeck.deckSize;
     //
     //  faceUpCard[0] = CardDatabase.cardList[displayId];
        

    }


    public void SetCardType(TypeOfCard cardType)
    {
        if(cardType == TypeOfCard.special)
        {
            typeOfCard=cardType;

            contractCardVariant.SetActive(false);
            specialCardVariant.SetActive(true);
            borderImage.sprite = specialCardSprite;
        }
        else
        {
            typeOfCard = cardType;

            contractCardVariant.SetActive(true);
            specialCardVariant.SetActive(false);
            borderImage.sprite = contractCardSprite;
        }
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

    public void SetSpecialCardValue(Card card)
    {

        // print("namn texten  " + nameText);
        // print("kortet " + card);

        borderImage.sprite = specialCardSprite;
        nameText.SetText(card.getCardName());
        descriptionText.SetText(card.getCardDescription());
    }
    public void SetContractCardValue(Card card)
    {

        // print("namn texten  " + nameText);
        // print("kortet " + card);

        borderImage.sprite = contractCardSprite;
        nameText.SetText(card.getCardName());
        descriptionText.SetText(card.getCardDescription());
    }

    public void SetContractCardValue()
    {

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
