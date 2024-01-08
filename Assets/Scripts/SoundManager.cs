using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    void Awake()
    {
        if (instance != null)
            GameObject.Destroy(instance);
        else
            instance = this;

    }

        public void Mute(bool muted) 
    { 
        if (muted) 
        {
            AudioListener.volume = 0;
        }
        else
        {
            AudioListener.volume = 1;
        }
    }





    private void PlaySound()
    {

    }
}