using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardDeck : MonoBehaviour
{
    public List<Card> deck = new List<Card>();
    public static List<Card> staticDeck = new List<Card>();
 
    public static int deckSize;

    public GameObject DisplayedCard;
    public GameObject[] Clones;
    public GameObject SpecialCardsPanel;

    public List<Card> shuffleContainer = new List<Card>();
    private float waitTime = 0.2f;

    //Reference for the instatiated prefabs
    private GameObject cardClone;

    //List to keep track of the instantiated prefabs
    private List<GameObject> destroyCard = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        deckSize = CardDatabase.cardList.Count;

        GenerateSpecialDeck();
        ShuffleDeck();

        StartCoroutine(DealCards());
    }

    // Update is called once per frame
    void Update()
    {
        staticDeck = deck;

        //Re-shuffling the cards
        if(TurnSystem.reShuffle == true)
        {
            StartCoroutine(ReShuffle(3));
            TurnSystem.reShuffle = false;

        }
    }

    void GenerateSpecialDeck()
    {
        for (int i = 0; i < CardDatabase.cardList.Count; i++)
        {
            deck.Add(CardDatabase.cardList[i]);
        }
    }

    public void ShuffleDeck()
    {
        for(int i = 0; i < deckSize; i++)
        {
            shuffleContainer[0] = deck[i];
            int randomIndex = Random.Range(i, deckSize);
            deck[i] = deck[randomIndex];
            deck[randomIndex] = shuffleContainer[0];
        }
    }

    //Create instantiated prefabs for the DisplayedCard prefab
    void CreateCard()
    {
        cardClone = Instantiate(DisplayedCard, transform.position, Quaternion.identity);
    }

    IEnumerator DealCards()
    {
        for(int i = 0; i <= 2; i++)
        {
            yield return new WaitForSeconds(waitTime);
            CreateCard();
            //the list of DestroyedCard clones
            destroyCard.Add(cardClone);
        }
    }


    IEnumerator ReShuffle(int x)
    {
        List<GameObject> cardsToDestroy = new List<GameObject>(destroyCard);

        foreach (GameObject card in cardsToDestroy)
        {
            destroyCard.Remove(card);
            Destroy(card);
        }

        destroyCard.Clear();
        yield return new WaitForSeconds(waitTime);

        //DisplayCard.faceUpCard[0] = CardDeck.staticDeck[DisplayCard.cardsInDeck + 3];
        DisplayCard.cardsInDeck += 3;
        deckSize += 3;

        ShuffleDeck();

        for (int i = 0; i < x; i++)
        {
            yield return new WaitForSeconds(waitTime);
            CreateCard();
            destroyCard.Add(cardClone);
        }
         
    }
}
