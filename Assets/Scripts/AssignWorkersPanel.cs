using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssignWorkersPanel : MonoBehaviour
{
    public GameObject panelToInactivate;

    public AssignWorkersButton assignWorkersButton;

    //ad
    public WorkType workType = WorkType.wrongWorkType;
    public int farmTileIndex = -1;


    public void AssignWorkersPanelOnClick()
    {
        UIManager.instance.MouseClickHandled();
        //panelToInactivate.SetActive(false);
        UIManager.instance.PopUIElement();
        assignWorkersButton.AssignNewWork(new Tuple<int, WorkType>(farmTileIndex, workType));
    }

}
