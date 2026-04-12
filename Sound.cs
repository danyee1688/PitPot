using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Sound
{
    public string name;
    public int id;
    public string type;
    public AudioClip audioClip;

    [HideInInspector]
    public AudioSource audioSource;

    [Range(0f, 1f)]
    public float volume;

    [Range(.1f, 3f)]
    public float pitch;

    public void Initialize(AudioSource audioSource)
    {
        this.audioSource = audioSource;
        this.audioSource.clip = audioClip;
        this.audioSource.volume = volume;
        this.audioSource.pitch = pitch;
    }
}
