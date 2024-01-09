using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    private AudioSource audioSource;

    public AudioClip cardMovedSound;

    public AudioClip pigDiedSound;

    public AudioClip specialCardPlayed;

    public AudioClip contractCardPlayed;
    public AudioClip takeCardToHandSound;

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
        audioSource.PlayOneShot(cardMovedSound);
    }

    public void PlayPigDied()
    {
        audioSource.PlayOneShot(pigDiedSound);
    }

    public void PlayContractCard()
    {
        audioSource.PlayOneShot(contractCardPlayed);
    }

    public void PlaySpecialCard()
    {
        audioSource.PlayOneShot(specialCardPlayed);
    }
    public void PlayTakeCardToHand()
    {
        audioSource.PlayOneShot(takeCardToHandSound);
    }
    public void PlayMouseClick()
    {
        audioSource.PlayOneShot(cardMovedSound);
    }
    public  void PlayButtonSound()
    {

    }
    public void PlayPurchaseFarm()
    {

    }

}