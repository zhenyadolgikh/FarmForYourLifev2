using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarmTileUI : MonoBehaviour
{
    public GameObject optionPanel;

    public UIManager manager;

    public int farmTileIndex = -1;


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
            //sätt knapparnas active utifrån IsActionValid
            
            //Sätt knapparnas OnClick 
        }
    }
}
