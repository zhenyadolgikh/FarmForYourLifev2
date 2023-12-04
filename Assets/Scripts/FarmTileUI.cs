using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FarmTileUI : MonoBehaviour
{
    public GameObject optionPanel;

    public UIManager manager;

    public int farmTileIndex = -1;

    public Button[] options;


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

    private void PopUp()
    {
        if(optionPanel != null)
        {
            bool isActive = optionPanel.activeSelf;
            optionPanel.SetActive(!isActive);

            options[2].enabled = false;

            BuildAction buildAction = new BuildAction();

        //   if (!manager.IsActionValid(buildAction))
        //   {
        //       optionPanel.GetComponentInChildren<FarmTile>(true);
        //
        //   }
            //sätt knapparnas active utifrån IsActionValid

                //Sätt knapparnas OnClick 
        }
    }
}
