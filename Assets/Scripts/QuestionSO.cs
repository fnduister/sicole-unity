using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "New Choices", menuName = "QuestionSyllabe1", order = 0)]
public class QuestionSO : ScriptableObject
{
    [SerializeField] List<string> choices = new List<string>();
    
    public List<string> GetChoices(int n)
    {
        var indexes = Helpers.GenerateDistinctIntegers(n);
        return indexes.Select(index => choices[index]).ToList();
    }
}
