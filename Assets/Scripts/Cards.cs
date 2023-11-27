using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Unity.VisualScripting.ReorderableList;

public class Card : MonoBehaviour
{

    public List<Card> faceUpCard = new List<Card> ();
    public Transform[] cardHandSlots;
    public bool[] availableCardHandSlots;


    public void DrawCard(int cardIndex)
    {
        if(faceUpCard.Count >= cardIndex)
        {
            Card selectedCard = faceUpCard[cardIndex];

            for (int i = 0; i < availableCardHandSlots .Length; i++)
            {
                if(availableCardHandSlots[i] == true)
                {
                    Debug.Log("Clicked");
                    selectedCard.transform.position = cardHandSlots[i].position;
                    availableCardHandSlots[i] = false;
                    //faceUpCard.Remove(cardIndex);
                    return;
                }
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
