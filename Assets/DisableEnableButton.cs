using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableEnableButton : MonoBehaviour
{

    public List<GameObject> gameObjectsToEnable;
    public List<GameObject> gameObjectsToDisable;

    public Vector2 originPosition;

    private void Start()
    {
        //originPosition = gameObjectsToEnable[0]
    }

    public void OnClick()
    {
        foreach(GameObject gameObject in gameObjectsToEnable)
        {
            //gameObject.SetActive(true);
            gameObject.GetComponent<RectTransform>().anchoredPosition = originPosition;
            if (!gameObject.activeSelf)
            {
                gameObject.SetActive(true);
            }

            
        }
        foreach(GameObject gameObject in gameObjectsToDisable)
        {
            //  gameObject.SetActive(false);
            gameObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(-1000, -1000);
        }
    }
}
