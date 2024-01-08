using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    private AudioSource audioSource;

    public AudioClip cardMovedSound;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();

        print("vad är audio sourcen" + audioSource);
    }

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





    public void PlayMoveSound()
    {
        print(" vad är audiosourcen sen " + audioSource);
        audioSource.PlayOneShot(cardMovedSound);
    }
}