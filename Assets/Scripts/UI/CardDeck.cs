using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardDeck : MonoBehaviour
{
    public List<Card> deck = new List<Card>();
 

    public GameObject DisplayedCard;
    public GameObject[] Clones;
    public GameObject SpecialCardsPanel;
    public GameObject contractCardsPanel;

    public GameObject cardInHandPanel;


    public List<Card> shuffleContainer = new List<Card>();

    //Reference for the instatiated prefabs


    //List to keep track of the instantiated prefabs

    public UIManager uiManager; 


    private void CreateCardDisplays(TypeOfCard cardType)
    {
        List<Card> cardsOnTable = uiManager.gameStateLogic.getCardsOnTable(cardType);

        GameObject panelToUse = GetPanelToUse(cardType);

        DisplayCard[] cardsToShow = panelToUse.GetComponentsInChildren<DisplayCard>(true);


        //int amountOfNull = 0;

        if(cardType == TypeOfCard.contract)
        {
           
        }

        for(int i = 0; i < cardsOnTable.Count; i++)
        {   
            if (cardsOnTable[i] == null) 
            {
                cardsToShow[i].gameObject.SetActive(false);
            }
            else
            {
                cardsToShow[i].gameObject.SetActive(true);
                cardsToShow[i].cardIndex = i;
                cardsToShow[i].SetCardType(cardType, cardsOnTable[i]);
                cardsToShow[i].inHand = false;
                //DisplayCard.CardCost();
            }
        }
    }

    private GameObject GetPanelToUse(TypeOfCard cardType)
    {
        if(cardType == TypeOfCard.special)
        {
            return SpecialCardsPanel;
        }
        else
        {
            return contractCardsPanel;
        }
    }

    private void ShowCardsInHand()
    {
        List<Card> cardsInHand = uiManager.gameStateLogic.GetCardsInHand();

        DisplayCard[] cardsToShowHand = cardInHandPanel.GetComponentsInChildren<DisplayCard>(true);

        //print(cardsToShowHand.Length + "mängden grejer i handen");

        for (int i = 0; i < cardsInHand.Count; i++)
        {
            
            cardsToShowHand[i].gameObject.SetActive(true);
            cardsToShowHand[i].cardIndex = i;
            cardsToShowHand[i].inHand = true;
            //DisplayCard.CardCost();
            if (cardsInHand[i] is SpecialCard)
            {
                cardsToShowHand[i].SetCardType(TypeOfCard.special, cardsInHand[i]);
            }
            else
            {
                cardsToShowHand[i].SetCardType(TypeOfCard.contract, cardsInHand[i]);
            }
        }
    }

    public void Refresh()
    {
        ResetCards();
        CreateCardDisplays(TypeOfCard.special);
        CreateCardDisplays(TypeOfCard.contract);
        ShowCardsInHand();
    }

    private void ResetCards()
    {
        DisplayCard[] cardsToReset = SpecialCardsPanel.GetComponentsInChildren<DisplayCard>();

        DisplayCard[] contractCardsToReset = contractCardsPanel.GetComponentsInChildren<DisplayCard>();

        DisplayCard[] cardsInHandToReset = cardInHandPanel.GetComponentsInChildren<DisplayCard>();

        foreach(DisplayCard card in cardsInHandToReset)
        {
            card.ResetCard();
            card.gameObject.SetActive(false);
        }

        foreach (DisplayCard card in cardsToReset)
        {
            card.ResetCard();
            card.gameObject.SetActive(false);
        }
        foreach (DisplayCard card in contractCardsToReset)
        {
            card.ResetCard();
            card.gameObject.SetActive(false);
        }
    }



    // Start is called before the first frame update
    void Start()
    {
    //    deckSize = CardDatabase.cardList.Count;
    //
    //    GenerateSpecialDeck();
    //    ShuffleDeck();
    //
    //    StartCoroutine(DealCards());
    }

    // Update is called once per frame
    void Update()
    {
     //   staticDeck = deck;
     //
     //   //Re-shuffling the cards
     //   if(TurnSystem.reShuffle == true)
     //   {
     //       StartCoroutine(ReShuffle(3));
     //       TurnSystem.reShuffle = false;
     //
     //   }
    }


}
