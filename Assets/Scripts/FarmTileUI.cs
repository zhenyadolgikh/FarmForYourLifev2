using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class FarmTileUI : MonoBehaviour
{
    public GameObject optionPanel;
    public Button[] options;

    public UIManager manager;

    public int farmTileIndex = -1;

    private CanvasGroup canvasGroup;




    // Start is called before the first frame update
    void Start()
    {
        canvasGroup = optionPanel.GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            // If CanvasGroup component is not present, add it.
            canvasGroup = optionPanel.AddComponent<CanvasGroup>();
        }
    }

    private void PopUp()
    {
        
        if (optionPanel != null)
        {
            Vector2 mousePosition = Mouse.current.position.ReadValue();
            optionPanel.transform.position = new Vector2(mousePosition.x, mousePosition.y + 130);
            bool isActive = optionPanel.activeSelf;
            optionPanel.SetActive(!isActive);

            canvasGroup.blocksRaycasts = !isActive;
        }

        foreach (Button button in options)
        {
            BuildAction buildAction = new BuildAction();
            button.interactable = manager.IsActionValid(buildAction);

            if (button.interactable)
            {
                //canClick = true;
                //bygg

            }
        }
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

    public void OnClickBuildWheat()
    {
        BuildAction buildAction = new BuildAction();
        buildAction.farmTileIndex = farmTileIndex;
        buildAction.resource = Resource.wheat;
        manager.DoAction(buildAction);
    }

    public void OnClickBuildApple()
    {
        BuildAction buildAction = new BuildAction();
        buildAction.farmTileIndex = farmTileIndex;
        buildAction.resource = Resource.apple;
        manager.DoAction(buildAction);
    }

    public void OnClickBuildCinnamon()
    {
        BuildAction buildAction = new BuildAction();
        buildAction.farmTileIndex = farmTileIndex;
        buildAction.resource = Resource.cinnamon;
        manager.DoAction(buildAction);
    }

    public void OnClickBuildPig()
    {
        BuildAction buildAction = new BuildAction();
        buildAction.farmTileIndex = farmTileIndex;
        buildAction.resource = Resource.pigMeat;
        manager.DoAction(buildAction);
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
                return Resource.cinnamon;
            case "PigButton":
                return Resource.pigMeat;
            default:
                // Handle unknown buttonName, you might want to throw an exception or return a default value
                throw new ArgumentException($"Unknown buttonName: {buttonName}");
        }
    }
    
}
