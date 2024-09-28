using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{
    [SerializeField] private AudioSource SFX;
    public AudioClip VirusDestroy
                    ,Explore
                    ,PickUpItem
                    ,GameOver
                    ,FireBullet;
    public void PlaySFX(AudioClip audioClip)
    {
        SFX.PlayOneShot(audioClip);
    }
                                      
}
