using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.UI;

public class ClonedWorker : MonoBehaviour
{
    public GameObject workerMesh;
    public Image workIcon;
    public Animator workAnim;

    public Sprite buildSprite, harvestSprite, idleSprite;
    public Animator buildAnim, harvestAnim, idleAnim;


    // Start is called before the first frame update
    void Awake()
    {
        workIcon = gameObject.GetComponentInChildren<Image>();
        workAnim = gameObject.GetComponentInChildren<Animator>();
    }

    public void WorkerAnimation(WorkType workType)
    {

       switch (workType)
       {
            case WorkType.building:
                workIcon.sprite = buildSprite;
                workAnim = buildAnim;
                buildAnim.SetTrigger("reSize");
                break;

          case WorkType.harvesting:
              workIcon.sprite = harvestSprite;
                Debug.Log(workIcon.sprite);
              break;
       
          case WorkType.unassigned:
              workIcon.sprite = idleSprite;
              break;
       
           default:
                break;
       }

    }

}
