using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Syllabe2 : SyllabusGameManager
{
    public string[] PossibleSyllabes = new string[8];
    public GameObject[] Letterbuttons = new GameObject[8];
    public GameObject[] SoundButtons = new GameObject[4];
    public GameObject[] AnswerButtons = new GameObject[8];
    
    protected override void SetRound()
    {
        ProgressBarTimer.Start();
        
        // reset the letter buttons
        foreach (var button in Letterbuttons)
        {
            button.GetComponent<MoveButton>().ResetButtonPosition();
        }
        
        // get 4 random syllabes
        string[] syllabes = new string[4];
        
        var possibleSyllabesIndexes = GenerateDistinctIntegers(4, 0, 8);
        
        for (int i = 0; i < 4; i++)
        {
            syllabes[i] = PossibleSyllabes[possibleSyllabesIndexes[i]];
        }

        var letterRandomIndex = GenerateDistinctIntegers(8, 0, 8);
        
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
            SoundButtons[i].GetComponent<SoundButton>().syllabus = syllabes[i];
        }
    }
    
    protected override void UpdateGameState()
    {
        if (_currentRound < MaxRound)
        {
            _currentRound++;
            // RoundDisplay = $"{_currentRound.ToString()}/{MaxRound.ToString()}";
            SetRound();
        }
        else
        {
            Debug.Log("Game Over");
        }
    }
}
