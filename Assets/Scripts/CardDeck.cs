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

    public GameObject cardInHandPanel;


    public List<Card> shuffleContainer = new List<Card>();

    //Reference for the instatiated prefabs


    //List to keep track of the instantiated prefabs

    public UIManager uiManager; 


    private void CreateDisplayCards()
    {
        List<SpecialCard> specialCardsOnTable = uiManager.gameStateLogic.getSpecialCardsOnTable();

        DisplayCard[] cardsToShow = SpecialCardsPanel.GetComponentsInChildren<DisplayCard>(true);


        int amountOfNull = 0;

        for(int i = 0; i < specialCardsOnTable.Count; i++)
        {   
            if (specialCardsOnTable[i] == null) 
            {
                cardsToShow[i].gameObject.SetActive(false);
                amountOfNull += 1;
            }
            else
            {
                cardsToShow[i].gameObject.SetActive(true);
                cardsToShow[i].SetCardValue(specialCardsOnTable[i]);
                cardsToShow[i].cardIndex = i;
                cardsToShow[i].typeOfCard = TypeOfCard.special;
                cardsToShow[i].inHand = false;

            }

        }

       // print("hur många kort i handen är null " + amountOfNull);
    }

    private void ShowCardsInHand()
    {
        List<Card> cardsInHand = uiManager.gameStateLogic.GetCardsInHand();

        DisplayCard[] cardsToShowHand = cardInHandPanel.GetComponentsInChildren<DisplayCard>(true);

        //print(cardsToShowHand.Length + "mängden grejer i handen");

        for (int i = 0; i < cardsInHand.Count; i++)
        {
            
            cardsToShowHand[i].gameObject.SetActive(true);
            cardsToShowHand[i].SetCardValue(cardsInHand[i]);
            cardsToShowHand[i].cardIndex = i;
            cardsToShowHand[i].inHand = true;
            if (cardsInHand[i] is SpecialCard)
            {
                cardsToShowHand[i].typeOfCard = TypeOfCard.special;
            }
            
        }
    }

    public void Refresh()
    {
        ResetCards();
        CreateDisplayCards();
        ShowCardsInHand();
    }

    private void ResetCards()
    {
        DisplayCard[] cardsToReset = SpecialCardsPanel.GetComponentsInChildren<DisplayCard>();

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
