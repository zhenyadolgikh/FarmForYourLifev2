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

    private float waitTime = 0.2f;


    // Start is called before the first frame update
    void Start()
    {
        deckSize = CardDatabase.cardList.Count;

        GenerateSpecialDeck();
        ShuffleDeck(deck);

        StartCoroutine(DealCards());
    }

    // Update is called once per frame
    void Update()
    {
        staticDeck = deck;
    }

    void GenerateSpecialDeck()
    {
        for (int i = 0; i < CardDatabase.cardList.Count; i++)
        {
            deck.Add(CardDatabase.cardList[i]);
        }
    }

    public void ShuffleDeck<T>(List<T> list)
    {
        System.Random random = new System.Random();
        int n = list.Count;
        while (n > 1)
        {
            int k = random.Next(n);
            n--;
            T temp = list[k];
            list[k] = list[n];
            list[n] = temp;
        }
    }

    IEnumerator DealCards()
    {
        for(int i = 0; i <= 2; i++)
        {
            yield return new WaitForSeconds(waitTime);
            Debug.Log("Waiting");
            Instantiate(DisplayedCard, transform.position, Quaternion.identity);
            //xOffset = xOffset + 160;
        }
    }
}
