using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    private AudioSource _audioSource;

    void Awake()
    {
        // Check if an AudioManager instance exists
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        
        // retrieve audioClip from folder music
        _audioSource = GetComponent<AudioSource>();
        

        DontDestroyOnLoad(gameObject);
    }

    public static void Play(string clipName)
    {
        AudioClip clip = Resources.Load<AudioClip>($"Music/{clipName}");
     
        if (clip != null)
        {
            instance._audioSource.PlayOneShot(clip);
        }
        else
        {
            Debug.LogError($"AudioClip '{clipName}' not found in Resources/Music folder.");
        }
    }
}
