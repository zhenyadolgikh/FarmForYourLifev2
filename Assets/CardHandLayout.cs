using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardHandLayout : MonoBehaviour
{

    RectTransform rectTransform;




    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    public void OrderCards()
    {
        DisplayCard[] cardsToOrder = GetComponentsInChildren<DisplayCard>();

        


        for(int i = 0; i < cardsToOrder.Length; i++)
        {
            
        }
    }

}
