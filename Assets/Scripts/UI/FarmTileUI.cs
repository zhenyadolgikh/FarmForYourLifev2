using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
using UnityEditor.Experimental.GraphView;
//using UnityEngine.UIElements;

public class FarmTileUI : MonoBehaviour, IPointerClickHandler
{
    //public GameStateLogic gameStateLogic;

    //public TextMeshProUGUI farmTileText;
    public GameObject farmTile;
    public GameObject optionPanel;
    public Button[] options;

    public GameObject assignWorkersPanel;
    public Button[] workOptions;

    public UIManager manager;

    public int farmTileIndex = -1;

    public Color normalColor;
    public Color hoverOverColor;

    bool mouseOver = false;






    // Start is called before the first frame update
    void Start()
    {

    }

//   public void OnPointerEnter(PointerEventData eventData)
//   {
//       farmTile = GetComponent<Renderer>().material.SetColor("_Color", hoverOverColor);
//   }
//
//   public void OnPointerExit(PointerEventData eventData)
//   {
//       
//   }

    void OnMouseEnter()
    {
        mouseOver = true;
        GetComponent<Renderer>().material.SetColor("_Color", hoverOverColor);
    }
    void OnMouseExit()
    {
        mouseOver = false;
        GetComponent<Renderer>().material.SetColor("_Color", normalColor);
    }

    private void PopUp()
    {
        if(manager.hudState != HudState.assignWorkers)
        {

                //   print(Input.mousePosition);

            Vector2 mousePosition = Input.mousePosition;
            optionPanel.transform.position = new Vector3(mousePosition.x, mousePosition.y + 130);
            bool isActive = optionPanel.activeSelf;
            //optionPanel.SetActive(!isActive);

            List<GameObject> uiElementsToAdd = new List<GameObject>();

            uiElementsToAdd.Add(optionPanel);

            if (!isActive)
            {
                manager.AddUIElement(new AddedUIElement(uiElementsToAdd, HudState.standard));
                optionPanel.SetActive(true);

            }
            else
            {
                manager.PopUIElement();
            }
            
            foreach (Button button in options)
            {
                BuildOnClick buildOnClick = button.GetComponent<BuildOnClick>();
                BuildAction buildAction = new BuildAction();
                buildAction.farmTileIndex = farmTileIndex;
                buildAction.resource = GetAssociatedResource(button.name);
                if(!manager.IsActionValid(buildAction).wasActionValid)
                {
                    button.interactable = false;

                    if (manager.gameStateLogic.GetStoredResourceAmount(Resource.money) < manager.gameStateLogic.GetBuildingCost(GetAssociatedResource(button.name)))
                    {
                        buildOnClick.costText.GetComponent<TextMeshProUGUI>().color = Color.red;
                    }
                //   if (manager.gameStateLogic.GetStoredResourceAmount(Resource.money) < manager.gameStateLogic.GetBuildingCost(GetAssociatedResource(button.name)) == false)
                //   {
                //       
                //   }

                }


                buildOnClick.farmTileIndex = farmTileIndex;
                buildOnClick.SetCostText(manager.gameStateLogic.GetBuildingCost(GetAssociatedResource(button.name)));

                
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

//   public void ResourceTextOnTile()
//   {
//       foreach (KeyValuePair<int, FarmTile> farmTileRef in gameStateLogic.GetFarmTiles())
//       {
//           int farmTileIndex = farmTileRef.Key;
//
//           if (farmTileIndex == this.farmTileIndex) // Check if it's the same farm tile as FarmTileUI
//           {
//               FarmTile farmTile = farmTileRef.Value;
//
//               if (farmTile.buildingOnTile || farmTile.isBuilt)
//               {
//                   TextMeshProUGUI farmTileText = GetComponentInChildren<TextMeshProUGUI>();
//
//                   if (farmTileText != null)
//                   {
//                       farmTileText.SetText(farmTileRef.Value.storedResources + " / " + farmTileRef.Value.maxStoredResources);
//                   }
//               }
//           }
//       }
//   }

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
