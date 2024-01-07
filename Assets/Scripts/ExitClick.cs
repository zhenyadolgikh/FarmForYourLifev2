using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitClick : MonoBehaviour
{
    // Start is called before the first frame update
    

    public void ExitOnClick()
    {
        SceneManager.LoadScene("MainMenu");
    }
        
}
