using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarmTile : MonoBehaviour
{
    public GameObject optionPanel;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnMouseDown()
    {
        PopUp();
    }

    private void PopUp()
    {
        if(optionPanel != null)
        {
            bool isActive = optionPanel.activeSelf;
            optionPanel.SetActive(!isActive);
        }
    }
}
