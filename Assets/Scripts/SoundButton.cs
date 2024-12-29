using UnityEngine;
using UnityEngine.UI;

public class SoundButton : MonoBehaviour
{
    public string syllabus;
    private Button _button;

    void Start()
    {
        _button = GetComponent<Button>();
        
        if (_button != null)
        {
            _button.onClick.AddListener(PlaySound);
        }
    }

    void PlaySound()
    {
        AudioManager.Play(syllabus);
    }
}