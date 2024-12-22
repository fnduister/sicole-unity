using UnityEngine;

public class Syllabe1 : MonoBehaviour
{
    [SerializeField] QuestionSO questionSO;
    [SerializeField] AudioManager audioManager;
    [SerializeField] GameObject[] buttons = new GameObject[4];
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        string[] choices = questionSO.GetChoices();
        int answerIndex = questionSO.GetAnswerIndex();
        
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].GetComponentInChildren<TMPro.TextMeshProUGUI>().text = choices[i];
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
