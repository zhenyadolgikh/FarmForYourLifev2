using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndPanel : MonoBehaviour
{
    public GameObject endPanel;
    public GameObject startPanel;
    public TextMeshProUGUI endText;

    public UIManager manager;


    public void GameEnd()
    {
        if (manager.gameStateLogic.GetIfVictorious())
        {
            Debug.Log("Vad händer här?");
            endText.SetText("Wow, you won!");
            endPanel.SetActive(true);
        }
        else if(manager.gameStateLogic.GetHasLost()){
 
            endText.SetText("Haha, you lost!");
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
