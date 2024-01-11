using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContractLayout : MonoBehaviour
{
    public int paddingX = 0;
    public int paddingY = 0;

    public float scaleModifier;

    private RectTransform rectTransform;

    public float cardWidth = 0;
    public float cardHeight = 0;


    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    private void Update()
    {
        //DisplayCard[] displayCards = GetComponentsInChildren<DisplayCard>();

    }

    public void OrderCards()
    {
        DisplayCard[] displayCards = GetComponentsInChildren<DisplayCard>(true);

        if (displayCards.Length > 0 ) 
        {
            cardWidth = displayCards[0].GetComponent<RectTransform>().rect.width;
            cardHeight = displayCards[0].GetComponent<RectTransform>().rect.height;
        }

        for(int i = 0; i < displayCards.Length; i++)
        {
            if(i < 3)
            {
               displayCards[i].GetComponent<RectTransform>().anchoredPosition = new Vector2(rectTransform.anchoredPosition.x + ((cardWidth + paddingX) * i), rectTransform.anchoredPosition.y);

            }

            //hard codat af vilket är wack
            if(i == 3)
            {
                displayCards[i].GetComponent<RectTransform>().anchoredPosition = new Vector2(rectTransform.anchoredPosition.x + ((cardWidth/2) + (paddingX/2)) , rectTransform.anchoredPosition.y - cardHeight - paddingY);
            }
            if( i == 4)
            {
                displayCards[i].GetComponent<RectTransform>().anchoredPosition = new Vector2(rectTransform.anchoredPosition.x + ((cardWidth / 2) + (paddingX * 1.5f) ) + cardWidth, rectTransform.anchoredPosition.y - cardHeight -paddingY);

            }// 
        }
    }


}
