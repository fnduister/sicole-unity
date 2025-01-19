using UnityEngine;
using UnityEngine.UI;

public class StaticAnswerButton : MonoBehaviour
{
    public string data;
    public int buttonId;
    public bool isAnswer = false;
    private Button _button;
    public SyllabusGameManager GameManager;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(CheckAnswer);
    }
    
    void CheckAnswer()
    {
        Debug.Log($"Button {buttonId} clicked");
        if (isAnswer)
        {
            Debug.Log("Correct answer");
            GameManager.ScoreUp();
            _button.GetComponent<Image>().color = "#2ECC71".ToColor();
        }
        else
        {
            Debug.Log("Incorrect answer");
            _button.GetComponent<Image>().color = "#E74C3C".ToColor();
            GameManager.ScoreDown();
        }
    }

    // Update is called once per frame
    public void Reset()
    {
        GetComponent<Image>().color = Color.white;
    }
}
