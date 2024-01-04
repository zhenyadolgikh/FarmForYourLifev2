using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndPanel : MonoBehaviour
{
    public GameObject endPanel;
    public GameObject startPanel;
    public GameObject endImage;
    public TextMeshProUGUI endText;
    public Sprite WinImage, LoseImage;

    public UIManager manager;


    public void GameEnd()
    {
        if (manager.gameStateLogic.GetIfVictorious())
        {
            endImage.GetComponent<Image>().sprite = WinImage;
            endText.SetText("Wow, you won!");
            endPanel.SetActive(true);
        }
        else if(manager.gameStateLogic.GetHasLost()){
            endImage.GetComponent<Image>().sprite = LoseImage;
            endText.SetText("Oh no, you lost. How sad. :(");
            endPanel.SetActive(true);
        }
    }

    public void OnRestart()
    {
        endPanel.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        startPanel.SetActive(false);
    }

    public void OnExit()
    {
        #if UNITY_STANDALONE
            Application.Quit();
        #endif

        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }

}
