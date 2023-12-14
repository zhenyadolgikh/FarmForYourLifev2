using System.Collections;
using System.Collections.Generic;
using UnityEditor.Search;
using UnityEngine;

public class ContractLayout : MonoBehaviour
{
    public int padding = 0;

    public float scaleModifier;

    private RectTransform rectTransform;

    private float cardWidth = 0;
    private float cardHeight = 0;


    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    private void Update()
    {
        DisplayCard[] displayCards = GetComponentsInChildren<DisplayCard>();

    }

    public void OrderCards()
    {
        DisplayCard[] displayCards = GetComponentsInChildren<DisplayCard>();

        if (displayCards.Length > 0 ) 
        {
            cardWidth = displayCards[0].GetComponent<RectTransform>().rect.width;
            cardHeight = displayCards[0].GetComponent<RectTransform>().rect.height;
        }

        for(int i = 0; i < displayCards.Length; i++)
        {
            if(i < 3)
            {
                displayCards[i].GetComponent<RectTransform>().transform.position = new Vector3(rectTransform.position.x + ((cardWidth + padding) * i), rectTransform.position.y, rectTransform.position.z);
            }

            //hard codat af vilket är wack
            if(i == 3)
            {
                displayCards[i].GetComponent<RectTransform>().transform.position = new Vector3(rectTransform.position.x + ((cardWidth/2) + (padding/2)) , rectTransform.position.y - cardHeight, rectTransform.position.z);
            }
            if( i == 4)
            {
                displayCards[i].GetComponent<RectTransform>().transform.position = new Vector3(rectTransform.position.x + ((cardWidth / 2) + (padding * 1.5f) ) + cardWidth, rectTransform.position.y - cardHeight, rectTransform.position.z);

            }
        }
    }


}
