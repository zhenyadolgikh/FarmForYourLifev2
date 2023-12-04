using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FarmTileUI : MonoBehaviour
{
    public GameObject optionPanel;
    public Button[] options;

    public UIManager manager;
    public int currentMoney;

    public int farmTileIndex = -1;

    bool canClick = false;




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

    //    BuildAction buildAction = new BuildAction();
    //    buildAction.farmTileIndex = 5;
    //    buildAction.resource = Resource.wheat;
    //
    //    if(manager.IsActionValid(buildAction))
    //    {
    //        manager.DoAction(buildAction);
    //    }
    }

    void OnClickBuildWheat()
    {
        BuildAction buildAction = new BuildAction();
        buildAction.farmTileIndex = 5;
        buildAction.resource = Resource.wheat;
    }

    private Resource GetAssociatedResource(string buttonName)
    {
        // Convert button name to associated resource
        switch (buttonName)
        {
            case "WheatButton":
                return Resource.wheat;
            case "AppleButton":
                return Resource.apple;
            case "CinnamonButton":
                return Resource.cotton;
            case "PigButton":
                return Resource.pigMeat;
            default:
                // Handle unknown buttonName, you might want to throw an exception or return a default value
                throw new ArgumentException($"Unknown buttonName: {buttonName}");
        }
    }

    private void PopUp()
    {
        currentMoney = manager.gameStateLogic.GetStoredResourceAmount(Resource.money);
        Debug.Log(currentMoney);

        if (optionPanel != null)
        {
            bool isActive = optionPanel.activeSelf;
            optionPanel.SetActive(!isActive);
            
           foreach (Button button in options)
           {
               Resource associateResource = GetAssociatedResource(button.name);
               int cost = manager.gameStateLogic.GetBuildingCost(associateResource);
               Debug.Log("Success!");
               button.interactable = currentMoney >= cost;

                if(button.interactable)
                {
                    canClick = true;
                    //manager.IsActionValid(buildAction) ??
                }
           }

            BuildAction buildAction = new BuildAction();

        //   if (!manager.IsActionValid(buildAction))
        //   {
        //               //
        //   }
            //sätt knapparnas active utifrån IsActionValid

                //Sätt knapparnas OnClick 
        }
    }
}
