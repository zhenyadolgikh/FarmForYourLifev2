using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyPanel : MonoBehaviour
{
    public static DontDestroyPanel instance;
    public bool showPanel = true;
    

    // Start is called before the first frame update
    void Awake()
    {
        if (instance == null)
            instance = this;            
        else
            GameObject.Destroy(gameObject);

        DontDestroyOnLoad(this);
    }

}
