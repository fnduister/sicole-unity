using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Question", menuName = "QuestionSyllabe1", order = 0)]
public class QuestionSO : ScriptableObject
{
    [SerializeField] string[] choices = new string[4];
    [SerializeField] int answerIndex = 0;
    
    public string[] GetChoices()
    {
        return choices;
    }
    
    public int GetAnswerIndex()
    {
        return answerIndex;
    }
}
