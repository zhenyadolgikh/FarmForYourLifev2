using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardViewer : MonoBehaviour
{

    [SerializeField]private DisplayCard cardViewer;




    private void Start()
    {
        cardViewer.disableOnClick = true;
    }

    public void SetCardView(Card card)
    {
        if(card is ContractCard)
        {
            cardViewer.SetCardType(TypeOfCard.contract, card);

            cardViewer.SetContractCardValue(card);
        }
        else
        {
            cardViewer.SetCardType(TypeOfCard.special,card);

            cardViewer.SetSpecialCardValue(card);
        }

        cardViewer.gameObject.SetActive(true);
    }

    public void InactivateCardView()
    {
        cardViewer.gameObject.SetActive(false);
    }


}
