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
        workIcon = gameObject.GetComponentInChildren<Image>();
    }

    public void WorkerAnimation()
    {
        workIcon.sprite = buildSprite;
    //   switch (workType)
    //   {
            //case WorkType.building:
               
                //Animator pigAnim = pigResource.GetComponent<Animator>();
                //pigAnim.SetTrigger("reSize");
                //break;

       //   case WorkType.harvesting:
       //       workIcon.sprite = buildSprite;
       //       break;
       //
       //   case WorkType.unassigned:
       //       workIcon.sprite = buildSprite;
       //       break;
       //
        //    default:
          //      break;
        //}

    }

}
