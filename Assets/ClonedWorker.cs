using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ClonedWorker : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public GameObject workerMesh;
    public Image workIcon;

    private Sprite buildSprite, harvestSprite, idleSprite;


    private Vector3 mousePosition;

    // Start is called before the first frame update
    void Awake()
    {
        print(Camera.main);
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

    private Vector3 ConvertScreenSpaceToWorldSpace()
    {
        Vector3 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition - mousePosition);
        
        Vector3 direction = worldPoint - Camera.main.transform.position;

        Vector3 cameraForward = direction;

        Vector3 mouseVector = worldPoint + (cameraForward * worldPoint.y / (-cameraForward.y));

        print("world pointen " + worldPoint);
        print("mouse vector " + mouseVector);

        return mouseVector;
    }

    private Vector3 GetMousePos()
    {
        print("hej");
        return Camera.main.WorldToScreenPoint(transform.position);
    }



    private void OnMouseUp()
    {
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        print("mouse down");

        mousePosition = Input.mousePosition - GetMousePos();
    }

    public void OnDrag(PointerEventData eventData)
    {
        print("mouse drag ");

     //Vector3 newPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition - mousePosition);
     //   
     //   Vector3 newPosition = Camera.main.ViewportToWorldPoint(Input.mousePosition - mousePosition);

     //   newPosition.y = transform.position.y;

        transform.position = ConvertScreenSpaceToWorldSpace();
    }

    public void OnEndDrag(PointerEventData eventData)
    {
       //throw new System.NotImplementedException();
    }
}
