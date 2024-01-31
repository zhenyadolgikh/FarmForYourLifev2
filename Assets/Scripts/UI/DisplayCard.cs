
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
    [SerializeField] private TextMeshProUGUI actionCostText;

    public Color defaultTextColor;

    public int cardIndex = -1;
    public TypeOfCard typeOfCard;
    public string cardIdentifier;

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


    public bool disableOnClick = false;

    // Start is called before the first frame update
    void Start()
    {
        uiManager = UIManager.instance;
     //  cardsInDeck = CardDeck.deckSize;
     //
     //  faceUpCard[0] = CardDatabase.cardList[displayId];
        

    }


    public void SetCardType(TypeOfCard cardType, Card card)
    {
        if(cardType == TypeOfCard.special)
        {
            typeOfCard=cardType;

            contractCardVariant.SetActive(false);
            specialCardVariant.SetActive(true);
            borderImage.sprite = specialCardSprite;

            SetSpecialCardValue(card);
        }
        else
        {
            typeOfCard = cardType;
            //
            contractCardVariant.SetActive(true);
            specialCardVariant.SetActive(false);
            borderImage.sprite = contractCardSprite;

            SetContractCardValue(card);
        }

        cardIdentifier = card.getCardName();
    }

    public void OnCardClick()
    {   

        if(disableOnClick)
        {
            return;
        }
        if(inHand == false)
        {
            AddCardToHandAction addCardToHandAction = new AddCardToHandAction();
            addCardToHandAction.index = cardIndex;
            addCardToHandAction.cardType = typeOfCard;

            IsActionValidMessage message = uiManager.IsActionValid(addCardToHandAction);

            if (message != null && message.wasActionValid)
            {
                gameObject.SetActive(false);
                uiManager.DoAction(addCardToHandAction);
            }
            else
            {   if(message == null)
                {
                    return;
                }
                uiManager.SendErrorMessage(message.errorMessage);
            }
        }
        if(inHand)
        {
            PlayCardAction playCardAction = new PlayCardAction();
            playCardAction.index = cardIndex;
            playCardAction.typeOfCard = typeOfCard;
            playCardAction.cardIdentifier = cardIdentifier;
            IsActionValidMessage message = uiManager.IsActionValid(playCardAction);

            if (message != null && message.wasActionValid)
            {
                gameObject.SetActive(false);
                uiManager.DoAction(playCardAction);
            }
            else
            {
                if (message == null)
                    return;
                uiManager.SendErrorMessage(uiManager.IsActionValid(playCardAction).errorMessage);

            }
        }
    }

    public void ResetCard()
    {
        cardName = "";
        cardDescription = "";
        wheatIndicator.gameObject.SetActive(false);
        appleIndicator.gameObject.SetActive(false);
        cinnamonIndicator.gameObject.SetActive(false);
        pigMeatIndicator.gameObject.SetActive(false);
        moneyIndicator.gameObject.SetActive(false);
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
        ContractCard contractCard = (ContractCard)card;
        // print("namn texten  " + nameText);
        // print("kortet " + card);

        borderImage.sprite = contractCardSprite;

        wheatIndicator.gameObject.SetActive(false);
        appleIndicator.gameObject.SetActive(false);
        cinnamonIndicator.gameObject.SetActive(false);
        pigMeatIndicator.gameObject.SetActive(false);
        


        if (contractCard.wheatNeeded != -1)
        {
            wheatIndicator.gameObject.SetActive(true);
            wheatIndicator.SetResourceIndicator(contractCard.wheatNeeded);
        }
        if(contractCard.applesNeeded != -1)
        {
            appleIndicator.gameObject.SetActive(true);
            appleIndicator.SetResourceIndicator(contractCard.applesNeeded);
        }
        if(contractCard.cinnamonsNeeded != -1)
        {
            cinnamonIndicator.gameObject.SetActive(true);
            cinnamonIndicator.SetResourceIndicator(contractCard.cinnamonsNeeded);
        }
        if(contractCard.pigMeatNeeded != -1)
        {
            pigMeatIndicator.gameObject.SetActive(true);
            pigMeatIndicator.SetResourceIndicator(contractCard.pigMeatNeeded);
        }

        moneyIndicator.gameObject.SetActive(true);
        moneyIndicator.SetResourceIndicator(contractCard.moneyEarned);
        //nameText.SetText(card.getCardName());
        //descriptionText.SetText(card.getCardDescription());
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

    public void CardCost()
    {
        uiManager = UIManager.instance;

        if (uiManager.gameStateLogic.GetCurrentActions() < 1)
        {

            actionCostText.color = Color.red;
        }
        else
        {
            actionCostText.color = defaultTextColor;
        }
            
        
    }

}
