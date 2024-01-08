using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardHandLayout : MonoBehaviour
{

    RectTransform rectTransform;

    public int paddingX = 0;
    public int height = 0;

    private float cardWidth = 0;
    private float cardHeight = 0;

    float middleX = 0;
    float middleY = 0;

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        middleX = rectTransform.rect.width/2;
        middleY = rectTransform.rect.height/2;
    }

    public void OrderCards()
    {
        DisplayCard[] cardsToOrder = GetComponentsInChildren<DisplayCard>(false);

        List<DisplayCard> cardsToOrderSorted = new List<DisplayCard>();


        if (cardsToOrder.Length > 0)
        {
            cardWidth = cardsToOrder[0].GetComponent<RectTransform>().rect.width;
            cardHeight = cardsToOrder[0].GetComponent<RectTransform>().rect.height;

            cardsToOrderSorted = new List<DisplayCard>();

            for(int i = 0; i < cardsToOrder.Length; i++)
            {
                cardsToOrderSorted.Add(null);
            }
            foreach(DisplayCard displayCard in cardsToOrder) 
            {
                cardsToOrderSorted[displayCard.cardIndex] = displayCard;
            }

            //print("vad är widthen" + cardWidth);
        }
        float positionsLeft;
        Vector2 originPosition;// = new Vector3();
        
        if(cardsToOrderSorted.Count % 2 == 0)
        {
            positionsLeft = cardsToOrderSorted.Count / 2;
            originPosition = new Vector2(middleX - (cardWidth + paddingX) * positionsLeft, rectTransform.position.y);
        }
        else
        {
            positionsLeft = cardsToOrderSorted.Count / 2;
            originPosition = new Vector2(middleX - (cardWidth + paddingX) * positionsLeft, rectTransform.position.y);
        }

        for(int i = 0; i < cardsToOrderSorted.Count; i++)
        {

            print("orign grejen " + originPosition);

            Vector2 nyVector = new Vector2(originPosition.x + ((cardWidth + paddingX) * i), height);


     
            print("nya grejen " + nyVector);

            //Vector3 vector = new Vector3(originPosition.x,originPosition.y,originPosition.z);
            cardsToOrder[i].GetComponent<RectTransform>().anchoredPosition = nyVector;
            
        }
    }

}
