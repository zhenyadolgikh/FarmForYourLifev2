using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardDeck : MonoBehaviour
{
    public List<Card> deck = new List<Card>();
    public static List<Card> staticDeck = new List<Card>();
 
    public static int deckSize;

    public GameObject DisplayedCard;
    public GameObject[] Clones;
    public GameObject SpecialCardsPanel;
    private HorizontalLayoutGroup horizontalLayoutGroup; 

    public List<Card> shuffleContainer = new List<Card>();
    private float waitTime = 0.2f;

    //Reference for the instatiated prefabs
    private GameObject cardClone;

    //List to keep track of the instantiated prefabs
    private List<GameObject> destroyCard = new List<GameObject>();

    public UIManager uiManager; 


    public void CreateDisplayCards()
    {
        List<SpecialCard> specialCardsOnTable = uiManager.gameStateLogic.getSpecialCardsOnTable();

        DisplayCard[] cardsToShow = SpecialCardsPanel.GetComponentsInChildren<DisplayCard>(true);


        for(int i = 0; i < specialCardsOnTable.Count; i++)
        {
            cardsToShow[i].gameObject.SetActive(true);
            cardsToShow[i].SetCardValue(specialCardsOnTable[i]);
        }

    }

    public void ResetCards()
    {
        DisplayCard[] cardsToReset = SpecialCardsPanel.GetComponentsInChildren<DisplayCard>();

        foreach(DisplayCard card in cardsToReset)
        {
            card.ResetCard();
            card.gameObject.SetActive(false);
        }
    }



    // Start is called before the first frame update
    void Start()
    {
        horizontalLayoutGroup = SpecialCardsPanel.gameObject.GetComponent<HorizontalLayoutGroup>();
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
