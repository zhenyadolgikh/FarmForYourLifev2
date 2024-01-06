using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClonedWorker : MonoBehaviour
{
    public GameObject workerMesh;
    public Image workIcon;

    private Sprite buildSprite, harvestSprite, idleSprite;


    // Start is called before the first frame update
    void Awake()
    {
        buildSprite = Resources.Load<Sprite>("2D/WBuild/2d1workerbuild");
        harvestSprite = Resources.Load<Sprite>("2D/WBuild/2d1WorkerHarvest");
        idleSprite = Resources.Load<Sprite>("2D/WBuild/2d1WorkerSleep");
        workIcon = gameObject.GetComponentInChildren<Image>();
    }

    public void WorkerAnimation(WorkType workType)
    {

       switch (workType)
       {
            case WorkType.building:
                workIcon.sprite = buildSprite;
                //Animator pigAnim = pigResource.GetComponent<Animator>();
                //pigAnim.SetTrigger("reSize");
                break;

          case WorkType.harvesting:
              workIcon.sprite = harvestSprite;
              break;
       
          case WorkType.unassigned:
              workIcon.sprite = idleSprite;
              break;
       
           default:
                break;
       }

    }

}
