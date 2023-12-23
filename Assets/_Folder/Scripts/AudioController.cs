using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    public static AudioController instance;
    public bool isMuted = false;
    public AudioSource _SfxAudioSource;
    public AudioSource _MusicAudioSource;
    public float musicVolume = .35f;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void LateUpdate()
    {
        if (isMuted)
        {
            _MusicAudioSource.volume = 0;
        }
        else
        {
            _MusicAudioSource.volume = musicVolume;
        }
    }

    public void PlaySFX(AudioClip clip)
    {
        if(isMuted) return;
        _SfxAudioSource.PlayOneShot(clip);
    }
}
