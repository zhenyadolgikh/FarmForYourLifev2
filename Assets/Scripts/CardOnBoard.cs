using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CardOnBoard : MonoBehaviour
{
    public GameObject SpecialCardsPanel;
    public GameObject DisplayedCard;
    public GameObject CardHand;

    // Start is called before the first frame update
    void Start()
    {
        SpecialCardsPanel = GameObject.Find("SpecialCardsPanel");
        DisplayedCard.transform.SetParent(SpecialCardsPanel.transform);
        //DisplayedCard.transform.localScale = Vector3.one;
        DisplayedCard.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        //DisplayedCard.transform.eulerAngles = new Vector3(0, 25, 0);
    }

    // Update is called once per frame
    void Update()
    {        

    }

    public void SelectCard()
    {
        CardHand = GameObject.Find("CardHand");
        Vector3 worldPosition = DisplayedCard.transform.position;


        DisplayedCard.transform.SetParent(CardHand.transform);

        DisplayedCard.transform.localPosition = CardHand.transform.InverseTransformPoint(worldPosition);


    }
}
