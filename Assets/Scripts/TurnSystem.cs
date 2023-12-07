using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TurnSystem : MonoBehaviour
{
    public bool isYourTurn;
    public int yourTurn;
    public TextMeshProUGUI turnText;

    public UIManager uiManager;

    public static bool startTurn;
    public static bool reShuffle;

    // Start is called before the first frame update
    void Start()
    {
        isYourTurn = true;
        startTurn = false;

        uiManager = UIManager.instance;
    }

    // Update is called once per frame
    void Update()
    {
        if(isYourTurn == true)
        {
            turnText.text = "1 / 12";
        }
    }

    public void EndYourTurn()
    {
        isYourTurn = false;
        startTurn = true;
    }

    public void ReShuffle()
    {

    }
}
