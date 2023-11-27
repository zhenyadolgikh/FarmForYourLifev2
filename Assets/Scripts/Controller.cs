using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Controller : MonoBehaviour, IPointerClickHandler
{
    public int cardIndex; // Set this in the Unity Editor
    private Card card;

    public void OnPointerClick(PointerEventData eventData)
        {
            if (eventData.button == PointerEventData.InputButton.Left)
            {
                // Call the DrawCard method with the clicked card's index
                card.DrawCard(cardIndex);
          }
      }
  }
