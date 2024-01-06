using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.UI;

public class ClonedWorker : MonoBehaviour
{
    public GameObject workerMesh;
    public Animator workAnim;

    public Sprite buildSprite, harvestSprite, idleSprite;


    // Start is called before the first frame update
    void Awake()
    {
        workAnim = gameObject.GetComponentInChildren<Animator>();
    }

    public void WorkerAnimation(WorkType workType)
    {
        workAnim.SetBool("building", false);
        workAnim.SetBool("harvesting", false);
        workAnim.SetBool("idle", false);

        switch (workType)
        {
            case WorkType.building:
                workAnim.SetBool("building", true);
                workAnim.SetBool("idle", false);

                break;

          case WorkType.harvesting:
                //    workIcon.sprite = harvestSprite;
                workAnim.SetBool("harvesting", true);
              break;
       
          case WorkType.unassigned:
                workAnim.SetBool("idle", true);
                break;
       
           default:
                break;
       }

    }

}
