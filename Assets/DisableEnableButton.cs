using System.Collections;
using System.Collections.Generic;
using UnityEditor.UI;
using UnityEngine;
using UnityEngine.UIElements;

public class DisableEnableButton : MonoBehaviour
{

    public List<GameObject> gameObjectsToEnable;
    public List<GameObject> gameObjectsToDisable;


    public void OnClick()
    {
        foreach(GameObject gameObject in gameObjectsToEnable)
        {
            gameObject.SetActive(true);
            Animator[] animatorsToReset = gameObject.GetComponentsInChildren<Animator>();

            foreach(Animator animator in animatorsToReset)
            {
                print("kommer hit");
                animator.ResetTrigger("Normal");

            }
            
        }
        foreach(GameObject gameObject in gameObjectsToDisable)
        {
            gameObject.SetActive(false);
            Animator[] animatorsToReset = gameObject.GetComponentsInChildren<Animator>();

            foreach (Animator animator in animatorsToReset)
            {
                print("kommer hit");
                animator.ResetTrigger("Normal");

            }
        }
    }
}
