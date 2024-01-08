using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    public GameObject buttonPanel;
    public UIManager manager;

    // Start is called before the first frame update
    void Start()
    {
        print("hello world");
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

    public void ClosePanel()
    {
        if (buttonPanel != null)
        {
            //bool isActive = buttonPanel.activeSelf;
            buttonPanel.SetActive(false);
        }
    }
}
