using UnityEngine;
using UnityEngine.UI;

public class Syllabe1 : SyllabusGameManager
{
    [SerializeField] string[] PossibleSyllabes;
    [SerializeField] AudioManager AudioManager;
    [SerializeField] GameObject[] SyllabeButtons = new GameObject[4];
    [SerializeField] public Button SoundButton;
    protected override void SetRound()
    {
        ProgressBarTimer.Reset();

        // get 4 random syllabes
        string[] syllabes = new string[4];
        
        var possibleSyllabesIndexes = GenerateDistinctIntegers(4, 0, 8);
        
        for (int i = 0; i < 4; i++)
        {
            syllabes[i] = PossibleSyllabes[possibleSyllabesIndexes[i]];
        }

        var letterRandomIndex = GenerateDistinctIntegers(4, 0, 8);
        
        for (int i = 0; i < 4; i++)
        {
            var first = i * 2;
            var second = i * 2 + 1;
            Letterbuttons[letterRandomIndex[first]].GetComponentInChildren<TMPro.TextMeshProUGUI>().text = syllabes[i][0].ToString();
            Letterbuttons[letterRandomIndex[first]].GetComponent<LetterButton>().letter = syllabes[i][0].ToString();
            Letterbuttons[letterRandomIndex[second]].GetComponentInChildren<TMPro.TextMeshProUGUI>().text = syllabes[i][1].ToString();
            Letterbuttons[letterRandomIndex[second]].GetComponent<LetterButton>().letter = syllabes[i][1].ToString();
            
            // set the answer buttons expected letter
            AnswerButtons[first].GetComponent<AnswerButton>().ExpectedLetter = syllabes[i][0].ToString();
            AnswerButtons[second].GetComponent<AnswerButton>().ExpectedLetter = syllabes[i][1].ToString();
            
            // set the sound buttons expected letter
            SoundButton.GetComponent<SoundButton>().syllabus = syllabes[i];
        }
    }
}
