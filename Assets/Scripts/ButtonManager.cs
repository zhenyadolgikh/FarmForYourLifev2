using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonManager : MonoBehaviour
{
    public GameObject buttonPanel;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Upgrade()
    {
        PopUp();
    }

    private void PopUp()
    {
        if (buttonPanel != null)
        {
            bool isActive = buttonPanel.activeSelf;
            buttonPanel.SetActive(!isActive);
        }
    }
}