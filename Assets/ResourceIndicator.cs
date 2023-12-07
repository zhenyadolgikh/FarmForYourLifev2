using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ResourceIndicator : MonoBehaviour
{
    

    [SerializeField]private Image resourceImage;
    [SerializeField] private TextMeshProUGUI resourceAmount;

    public void SetResourceIndicator(int ResourceAmount)
    {
        //resourceImage.sprite = sprite;
        resourceAmount.SetText(resourceAmount.ToString());
    }


}
