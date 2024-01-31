using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class EffectHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public string cardName {  get; private set; }

   // private int remainingTurns = 0;

    [SerializeField] private TextMeshProUGUI cardNameText;
    [SerializeField] private TextMeshProUGUI turnCountText;

    public void InitializeObject(string cardName, int amountOfTurns)
    {
        this.cardName = cardName;
     //   remainingTurns = amountOfTurns;

        cardNameText.text = cardName;

        if (amountOfTurns == -1)
        {
            turnCountText.text = "-";
        }
        else
        {
            turnCountText.text = amountOfTurns.ToString();
        }
    }

    public void UpdateTurnCount(int amountOfTurns)
    {

        if (amountOfTurns == -1)
        {
            turnCountText.text = "-";
        }
        else
        {
            turnCountText.text = amountOfTurns.ToString();
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        UIManager.instance.SetCardView(UIManager.instance.gameStateLogic.GetSpecialCardFromName(cardName));
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        UIManager.instance.InactivateCardView();
    }
}
