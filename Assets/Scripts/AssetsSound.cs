using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AssetsSound
{
    public string name;

    public bool loop;

    public AudioClip clip;

    [Range(0f, 1f)]
    public float volume;

    [Range(0f, 3f)]
    public float pitch;

    public AudioSource source;
}
