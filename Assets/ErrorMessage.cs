using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ErrorMessage : MonoBehaviour
{
    public TextMeshProUGUI errorText;
    public int lenghtToStay;


    public void SetErrorMessage(string message)
    {
        gameObject.SetActive(true);

        errorText.text = message;

        StartCoroutine(InactivateWindow(lenghtToStay));
    }

    public void OnClickRemove()
    {
        gameObject.SetActive(false);
    }

    IEnumerator InactivateWindow(int seconds)
    {

        yield return new WaitForSeconds(seconds);
        gameObject.SetActive (false);
    }


}
