using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AssignWorkersButton : MonoBehaviour
{
    public TextMeshProUGUI textShowingWorkerCount;

    private UIManager uiManager;

    public List<Tuple<int, WorkType>> currentWorkAssigned = new List<Tuple<int, WorkType>>();

    private void Start()
    {
        uiManager = UIManager.instance;
    }

    public void AssignWorkersOnClick()
    {
        currentWorkAssigned.Clear();

        textShowingWorkerCount.gameObject.SetActive(true);

        List<GameObject> uiElementsAdded = new List<GameObject>();

        uiElementsAdded.Add(textShowingWorkerCount.gameObject);

        AddedUIElement addedUIElement = new AddedUIElement(uiElementsAdded, HudState.assignWorkers);

        uiManager.AddUIElement(addedUIElement);

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
    }
    
}
