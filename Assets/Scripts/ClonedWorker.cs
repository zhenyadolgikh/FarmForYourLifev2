using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ClonedWorker : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public GameObject workerMesh;
    public Image workIcon;
    public Animator workAnim;

    public Sprite buildSprite, harvestSprite, idleSprite;

    private Vector3 mousePosition;


    public int workerId = -1;

    private UIManager uiManager;

    // Start is called before the first frame update
    void Awake()
    {
        workAnim = gameObject.GetComponentInChildren<Animator>();
    }
    void Start()
    {
        uiManager = UIManager.instance;
    }

    private void Update()
    {
        print("vilket worker id " + workerId);
    }


    private Vector3 ConvertScreenSpaceToWorldSpace()
    {
        Vector3 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition - mousePosition);

        Vector3 direction = worldPoint - Camera.main.transform.position;

        Vector3 cameraForward = direction;

        Vector3 mouseVector = worldPoint + (cameraForward * worldPoint.y / (-cameraForward.y));


        return mouseVector;
    }

    private Vector3 GetMousePos()
    {
        
        return Camera.main.WorldToScreenPoint(transform.position);
    }



    private void OnMouseUp()
    {
    }

    public void OnBeginDrag(PointerEventData eventData)
    {

        uiManager.SetWorkerBeingDragged(true);
        uiManager.SetWorkerBeingDraggedId(workerId);

        mousePosition = Input.mousePosition - GetMousePos();
    }

    public void OnDrag(PointerEventData eventData)
    {

        //Vector3 newPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition - mousePosition);
        //   
        //   Vector3 newPosition = Camera.main.ViewportToWorldPoint(Input.mousePosition - mousePosition);

        //   newPosition.y = transform.position.y;

        transform.position = ConvertScreenSpaceToWorldSpace();
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        UIManager.instance.Refresh();

        uiManager.SetWorkerBeingDragged(true);


        
        //throw new System.NotImplementedException();
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
                break;

          case WorkType.harvesting:
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
