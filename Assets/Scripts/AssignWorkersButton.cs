using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AssignWorkersButton : MonoBehaviour
{
    public TextMeshProUGUI textShowingWorkerCount;

    private UIManager uiManager;

    [SerializeField] private GameObject buttonPanel;

    //

    public List<Tuple<int, WorkType>> currentWorkAssigned = new List<Tuple<int, WorkType>>();

    private void Start()
    {
        uiManager = UIManager.instance;
    }

    public void DisableText()
    {
        textShowingWorkerCount.gameObject.SetActive(false);
    }

    public void AssignWorkersOnClick()
    {
        uiManager.MouseClickHandled();


        if(uiManager.hudState == HudState.assignWorkers)
        {
            uiManager.ResetUIElement();
            return;
        }

        if(uiManager == null)
        {
            uiManager = UIManager.instance;
        }
        currentWorkAssigned.Clear();
        uiManager.PlaceAllWorkersIdle();


        List<GameObject> uiElementsAdded = new List<GameObject>();

        uiElementsAdded.Add(textShowingWorkerCount.gameObject);

        AddedAssignWorkers addedUIElement = new AddedAssignWorkers(uiElementsAdded, HudState.assignWorkers);

        addedUIElement.panel = buttonPanel;
        addedUIElement.text = textShowingWorkerCount.gameObject;

        uiManager.AddUIElement(addedUIElement);


        textShowingWorkerCount.gameObject.SetActive(true);

        textShowingWorkerCount.SetText("Workers placed " + "0/" + uiManager.gameStateLogic.GetWorkerRegistry().Count);

        
    }

    public void AssignNewWork(Tuple<int, WorkType> workAssigned)
    {
        currentWorkAssigned.Add(workAssigned);
        textShowingWorkerCount.SetText("Workers placed " + currentWorkAssigned.Count + "/" + uiManager.gameStateLogic.GetWorkerRegistry().Count);


        if (currentWorkAssigned.Count == uiManager.gameStateLogic.GetWorkerRegistry().Count)
        {
            AssignWorkersAction assignWorkersAction = new AssignWorkersAction();
            assignWorkersAction.workAssigned = currentWorkAssigned;

            uiManager.DoAction(assignWorkersAction);
            uiManager.PopUIElement();
           // textShowingWorkerCount.gameObject.SetActive(false);
           // uiManager.hudState = HudState.standard;
                                       
            currentWorkAssigned.Clear();

           // print("den skickar actionen");
        }

        uiManager.PlaceWorkerDuringAssign(workAssigned, currentWorkAssigned.Count);
    }
    
}
