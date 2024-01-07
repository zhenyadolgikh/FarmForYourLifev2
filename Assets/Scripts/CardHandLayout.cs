using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardHandLayout : MonoBehaviour
{

    RectTransform rectTransform;

    public int paddingX = 0;


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


        print("vad är rect transformen width " + rectTransform.rect.width + " vad är höjden " + rectTransform.rect.height);

        if (cardsToOrder.Length > 0)
        {
            cardWidth = cardsToOrder[0].GetComponent<RectTransform>().rect.width;
            cardHeight = cardsToOrder[0].GetComponent<RectTransform>().rect.height;

            //print("vad är widthen" + cardWidth);
        }
        float positionLeft;
        Vector3 originPosition;// = new Vector3();
        
        if(cardsToOrder.Length % 2 == 0)
        {
            positionLeft = cardsToOrder.Length / 2;
            originPosition = new Vector3(middleX - (cardWidth + paddingX) * positionLeft, rectTransform.position.y,0);
        }
        else
        {
            positionLeft = cardsToOrder.Length / 2;
            originPosition = new Vector3(middleX - (cardWidth + paddingX) * positionLeft, rectTransform.position.y, 0);
        }

        for(int i = 0; i < cardsToOrder.Length; i++)
        {

            print("orign grejen " + originPosition);

            Vector3 nyVector = new Vector3(originPosition.x + ((cardWidth + paddingX) * i), originPosition.y, originPosition.z);
            print("nya grejen " + nyVector);

            //Vector3 vector = new Vector3(originPosition.x,originPosition.y,originPosition.z);
            cardsToOrder[i].GetComponent<RectTransform>().transform.position = nyVector;
            
        }
    }

}
