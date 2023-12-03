using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public List<Card> displayedCard = new List<Card>();
    public GameObject HandPanel;
    public GameObject[] ClickedCard;
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
        if (displayedCard.Count >= 1)
        {
            selectedCard = 

            for (int i = 0; i < availableCardHandSlots.Length; i++)
            {
                if (availableCardHandSlots[i] == true)
                {
                    Debug.Log("Clicked");
                    selectedCard.gameObject.SetActive(true);
                    selectedCard.transform.position = cardHandSlots[i].position;
                }
            }
        }

    }*/
}
