using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UICostManager : MonoBehaviour
{
    public Button[] buttons;
    public Color defaultTextColor;

    public UIManager manager;

    public void CanAfford()
    {
        foreach (Button button in buttons)
        {
            button.interactable = true;
            Transform moneyTextTransform = button.transform.Find("ButtonText/MoneyText (TMP)");
            Transform actionTextTransform = button.transform.Find("ButtonText/ActionText (TMP)");

            if (moneyTextTransform != null)
            {
                TextMeshProUGUI moneyText = moneyTextTransform.GetComponent<TextMeshProUGUI>();
                string moneyTextString = moneyText.text;

                if (manager.gameStateLogic.GetStoredResourceAmount(Resource.money) < int.Parse(moneyTextString))
                {
                    moneyText.color = Color.red;
                    button.interactable = false;
                }
                else
                {
                    moneyText.color = defaultTextColor;
                }
            }
            if(actionTextTransform != null)
            {
                TextMeshProUGUI actionText = actionTextTransform.GetComponent<TextMeshProUGUI>();

                if (manager.gameStateLogic.GetCurrentActions() < 1)
                {
                    actionText.color = Color.red;
                    button.interactable = false;
                }
                else
                {
                    actionText.color = defaultTextColor;
                }
            }
            
        }
    }

}
