using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

public class FarmTileUI : MonoBehaviour, IPointerClickHandler
{
    public GameObject optionPanel;
    public Button[] options;

    public GameObject assignWorkersPanel;
    public Button[] workOptions;

    public UIManager manager;

    public int farmTileIndex = -1;

    private Camera cam;




    // Start is called before the first frame update
    void Start()
    {

    }

    private void PopUp()
    {
        if(manager.hudState != HudState.assignWorkers)
        {
            if (optionPanel != null)
            {

                //   print(Input.mousePosition);

                Vector2 mousePosition = Input.mousePosition;
                optionPanel.transform.position = new Vector3(mousePosition.x, mousePosition.y + 130);
                bool isActive = optionPanel.activeSelf;
                optionPanel.SetActive(!isActive);

                List<GameObject> uiElementsToAdd = new List<GameObject>();

                uiElementsToAdd.Add(optionPanel);

                if (optionPanel.activeSelf)
                {
                    manager.AddUIElement(new AddedUIElement(uiElementsToAdd, HudState.standard));

                }
            }

            foreach (Button button in options)
            {
                BuildAction buildAction = new BuildAction();
                buildAction.farmTileIndex = farmTileIndex;
                buildAction.resource = GetAssociatedResource(button.name);
                button.interactable = manager.IsActionValid(buildAction);

                if (button.interactable)
                {
                    BuildOnClick buildOnClick = button.GetComponent<BuildOnClick>();
                    buildOnClick.farmTileIndex = farmTileIndex;
                }
            }
        }
        if (manager.hudState == HudState.assignWorkers)
        {
            if (assignWorkersPanel != null)
            {

                //   print(Input.mousePosition);

                Vector2 mousePosition = Input.mousePosition;
                assignWorkersPanel.transform.position = new Vector3(mousePosition.x, mousePosition.y + 130);
                bool isActive = assignWorkersPanel.activeSelf;
                assignWorkersPanel.SetActive(!isActive);

                List<GameObject> uiElementsToAdd = new List<GameObject>();

                uiElementsToAdd.Add(assignWorkersPanel);

                if (assignWorkersPanel.activeSelf)
                {
                    manager.AddUIElement(new AddedUIElement(uiElementsToAdd, HudState.assignWorkers));

                }
            }

            foreach (Button button in workOptions)
            {
            //   BuildAction buildAction = new BuildAction();
            //   buildAction.farmTileIndex = farmTileIndex;
            //   buildAction.resource = GetAssociatedResource(button.name);
            //   button.interactable = manager.IsActionValid(buildAction);
            //
                if (button.interactable)
                {
                    AssignWorkersPanel assignOnClick = button.GetComponent<AssignWorkersPanel>();
                    assignOnClick.farmTileIndex = farmTileIndex;
                }
            }
        }

    }

    public void OnPointerClick(PointerEventData pointerEventData)
    {
        //Output to console the clicked GameObject's name and the following message. You can replace this with your own actions for when clicking the GameObject.
     //   Debug.Log(name + " Game Object Clicked!");
        PopUp();
    }

    void OnMouseDown()
    {
   //     PopUp();

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
