using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardDeck : MonoBehaviour
{
    public List<Card> deck = new List<Card>();
    public int x;

    // Start is called before the first frame update
    void Start()
    {
        GenerateSpecialDeck();
        ShuffleDeck(deck);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void GenerateSpecialDeck()
    {
        x = 0;

        for (int i = 0; i < CardDatabase.cardList.Count; i++)
        {
            deck.Add(CardDatabase.cardList[i]);
        }
    }

    void ShuffleDeck<T>(List<T> list)
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
}
