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
    public TextMeshProUGUI endText;

    public UIManager manager;

    public void GameEnd()
    {
        if (manager.gameStateLogic)
        {
            endText.SetText("Wow, you won!");
        }
        else
            endText.SetText("Haha, you lost!");
    }

    public void OnRestart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        endPanel.SetActive(false);

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
