using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using System.ComponentModel;

public class DisplayedCard : MonoBehaviour
{
    public List<Card> faceUpCard = new List<Card>();

    public int displayId;

    public int id;
    public string cardName;
    public string cardDescription;

    public TextMeshProUGUI nameText;
    public TextMeshProUGUI descriptionText;

    // Start is called before the first frame update
    void Start()
    {

        faceUpCard[0] = CardDatabase.cardList[displayId];

      
    }

    // Update is called once per frame
    void Update()
    {

            id = faceUpCard[0].id;
            cardName = faceUpCard[0].cardName;
            cardDescription = faceUpCard[0].cardDescription;

            nameText.text = " " + cardName;
            descriptionText.text = " " + cardDescription;
        

    }


    /*void Shuffle()
    {
        for(int i = 0; i < deckSize; i++)
        {
            container[0] = faceUpCard[i];
            int randomIndex = Random.Range(i, deckSize);
            faceUpCard[i] = faceUpCard[randomIndex];
            faceUpCard[randomIndex] = container[0];
        }
    }*/

}
