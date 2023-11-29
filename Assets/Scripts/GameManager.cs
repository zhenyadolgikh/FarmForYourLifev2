using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public List<Card> card = new List<Card>();
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

    /*public void DrawCard(int cardIndex)
    {
        if (card.Count >= cardIndex)
        {
            Card selectedCard = card[cardIndex];

            for (int i = 0; i < availableCardHandSlots.Length; i++)
            {
                if (availableCardHandSlots[i] == true)
                {
                    Debug.Log("Clicked");
                    selectedCard.transform.position = cardHandSlots[i].position;
                    availableCardHandSlots[i] = false;
                    card.Remove(selectedCard);
                    return;
                }
            }
        }

    }*/
}
