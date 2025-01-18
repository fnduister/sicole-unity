using System;
using UnityEngine;

public enum AudioType
{
    Music,
    Ambient
}

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    private AudioSource _clipSource;
    private AudioSource _ambientSource;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        _clipSource = gameObject.AddComponent<AudioSource>();
        _ambientSource = gameObject.AddComponent<AudioSource>();
        _ambientSource.volume = 0.05f;
        _clipSource.volume = 0.85f;

        DontDestroyOnLoad(gameObject);
    }

    public static void Play(string clipName, AudioType type)
    {
        AudioClip clip = Resources.Load<AudioClip>($"{type.ToString()}/{clipName}");
        
        if (clip != null)
        {   
            var audioSource = type == AudioType.Music ? instance._clipSource : instance._ambientSource;
            audioSource.clip = clip;
            audioSource.loop = (type == AudioType.Ambient);
            audioSource.Play();
        }
        else
        {
            Debug.LogError($"AudioClip '{clipName}' not found in Resources/{type.ToString()} folder.");
        }
    }
}