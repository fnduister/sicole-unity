using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    private AudioSource _musicSource;
    private AudioSource _ambientSource;
    private AudioSource _sfxSource;

    void Awake()
    {
        if (!Instance)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        _musicSource = gameObject.AddComponent<AudioSource>();
        _ambientSource = gameObject.AddComponent<AudioSource>();
        _sfxSource = gameObject.AddComponent<AudioSource>();

        _musicSource.volume = 0.85f;
        _ambientSource.volume = 0.05f;
        _sfxSource.volume = 0.7f;

        _musicSource.loop = true;
        _ambientSource.loop = true;
        _sfxSource.loop = false;

        DontDestroyOnLoad(gameObject);
    }

    public static void Play(string clipName, AudioType type)
    {
        AudioClip clip = Resources.Load<AudioClip>($"{type.ToString()}/{clipName}");

        if (clip != null)
        {
            PlayClip(clip, type);
        }
        else
        {
            Debug.LogError($"AudioClip '{clipName}' not found in Resources/{type.ToString()} folder.");
        }
    }

    public static void Play(AudioClip clip, AudioType type)
    {
        if (clip != null)
        {
            PlayClip(clip, type);
        }
        else
        {
            Debug.LogError("AudioClip is null.");
        }
    }

    private static void PlayClip(AudioClip clip, AudioType type)
    {
        AudioSource audioSource = type switch
        {
            AudioType.Music => Instance._musicSource,
            AudioType.Ambient => Instance._ambientSource,
            AudioType.SFX => Instance._sfxSource,
            _ => Instance._musicSource
        };

        if (type == AudioType.SFX)
        {
            audioSource.PlayOneShot(clip);
        }
        else
        {
            audioSource.clip = clip;
            audioSource.Play();
        }
    }

    public static void StopMusic()
    {
        if (Instance._musicSource.isPlaying)
        {
            Instance._musicSource.Stop();
        }
    }

    public static void StopAmbient()
    {
        if (Instance._ambientSource.isPlaying)
        {
            Instance._ambientSource.Stop();
        }
    }

    public static void SetMusicVolume(float volume)
    {
        Instance._musicSource.volume = Mathf.Clamp01(volume);
    }

    public static void SetAmbientVolume(float volume)
    {
        Instance._ambientSource.volume = Mathf.Clamp01(volume);
    }

    public static void SetSFXVolume(float volume)
    {
        Instance._sfxSource.volume = Mathf.Clamp01(volume);
    }
}