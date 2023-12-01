using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public List<Card> deck = new List<Card>();
    public Transform[] cardHandSlots;
    public bool[] availableCardHandSlots;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /*public void DrawCard()
    {
        if (deck.Count >= 1)
        {
            Card randomCard = deck[Random.Range(0, deck.Count)];

            for (int i = 0; i < availableCardHandSlots.Length; i++)
            {
                if (availableCardHandSlots[i] == true)
                {
                    Debug.Log("Clicked");
                    randomCard.gameObject.SetActive(true);
                    randomCard.transform.position = cardHandSlots[i].position;
                }
            }
        }

    }*/
}
