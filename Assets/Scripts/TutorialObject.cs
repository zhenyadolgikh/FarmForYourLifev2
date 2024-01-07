using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialObject : MonoBehaviour
{

    public List<GameObject> objectsToDisable = new List<GameObject>();
    public List<GameObject> objectsToEnable = new List<GameObject>();

    public TutorialObject tutorialObjectToEnable;

    public bool acceptActions; 



    public void RemoveObject()
    {
        foreach(GameObject obj in objectsToDisable) 
        {
            obj.SetActive(false);        
        }
        if(tutorialObjectToEnable != null)
        {
            tutorialObjectToEnable.ShowObject();
        }

    }

    public void ShowObject()
    {
        foreach (GameObject obj in objectsToDisable) 
        {
            obj.SetActive(true);
        }

        UIManager.instance.SetAcceptActions(acceptActions);
        UIManager.instance.SetTutorialObjectToPop(this);
    }
}
