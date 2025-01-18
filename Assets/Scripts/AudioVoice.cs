using UnityEngine;

[CreateAssetMenu(fileName = "AudioVoice", menuName = "AudioClipGroup")]
public class AudioVoice : ScriptableObject
{
    public string[] audioClips;
    
    // get random audio clip
    public string GetRandomClip()
    {
        return audioClips[Random.Range(0, audioClips.Length)];         
    }
}
