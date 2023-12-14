using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TogglePanel : MonoBehaviour
{
    public GameObject panel;
    Toggle toggle;

    public void Toggle()
    {
        toggle = GetComponent<Toggle>();
        if(toggle.isOn)
            panel.SetActive(false);
    }
}
