using UnityEngine.Audio;
using UnityEngine;

[System.Serializable]
public class Sound
{
    public string name;
    public AudioClip clip;
    [HideInInspector]
    public AudioSource source;

    public bool loop;
    public bool playOnAwake;

    [Range(0,100)]
    public float volume;
    [Range(0, 100)]
    public float pitch;

}
