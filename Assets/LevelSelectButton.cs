using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelectButton : MonoBehaviour
{
    public string levelToLoad;

    public void OnClick()
    {
        SceneManager.LoadScene(levelToLoad);
    }
}
