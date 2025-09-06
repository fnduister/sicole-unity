using System;
using UnityEngine;

public class Syllabe4 : SyllabusGameManager
{
    public QuestionSO PossibleSyllabes;
    public GameObject[] Letterbuttons = new GameObject[8];
    public GameObject[] SoundButtons = new GameObject[4];
    public GameObject[] AnswerButtons = new GameObject[8];
    public int GoodMoves = 0;
    public int Moves = 0;

    public int MaxMoves = 30;
    // make sure to assign the following in the inspector
    
    protected override void SetRound()
    {
        ProgressBarTimer.Reset();
        
        // reset the letter buttons
        foreach (var button in Letterbuttons)
        {
            button.GetComponent<MoveButton>().ResetButtonPosition();
        }
        
        // get 4 random syllabes
        string[] syllabes = PossibleSyllabes.GetChoices(4).ToArray();

        var letterRandomIndex = Helpers.GenerateDistinctIntegers(8, 0, 8);
        
        for (int i = 0; i < 4; i++)
        {
            var first = i * 2;
            var second = i * 2 + 1;
            Letterbuttons[letterRandomIndex[first]].GetComponentInChildren<TMPro.TextMeshProUGUI>().text = syllabes[i][0].ToString();
            Letterbuttons[letterRandomIndex[first]].GetComponent<LetterButton>().letter = syllabes[i][0].ToString();
            Letterbuttons[letterRandomIndex[second]].GetComponentInChildren<TMPro.TextMeshProUGUI>().text = syllabes[i][1].ToString();
            Letterbuttons[letterRandomIndex[second]].GetComponent<LetterButton>().letter = syllabes[i][1].ToString();
            
            // set the answer buttons expected letter
            AnswerButtons[first].GetComponent<MoveableAnswerButton>().ExpectedLetter = syllabes[i][0].ToString();
            AnswerButtons[second].GetComponent<MoveableAnswerButton>().ExpectedLetter = syllabes[i][1].ToString();
            
            // set the sound buttons expected letter
            SoundButtons[i].GetComponent<SoundButton>().syllabus = syllabes[i];
        }
    }
    
    protected override void UpdateGameState()
    {
        if (_currentRound < MaxRound)
        {
            _currentRound++;
            RoundDisplay = $"{_currentRound.ToString()}/{MaxRound.ToString()}";
            SetRound();
        }
        else
        {
            Debug.Log("Game Over");
            GetScore();
            ProgressBarTimer.Pause();
            GameOverMenu.SetActive(true);
        }
    }
    
    protected override void CheckGameStatus()
    {
        if (GoodMoves != 0 && GoodMoves % AnswerButtons.Length == 0 || Moves <= 0)
        {
            Invoke(nameof(UpdateGameState), 2);
        }
    }

    public override void ScoreUp()
    {
        ++GoodMoves;
        --Moves;
        base.ScoreUp();
    }
    
    public override void ScoreDown()
    {
        --Moves;
        base.ScoreDown();
    }
    
    protected override void Reset()
    {
        Moves = MaxMoves;
        GoodMoves = 0;
        _currentRound = 1;
        RoundDisplay = $"{_currentRound.ToString()}/{MaxRound.ToString()}";
        base.Reset();
    }
    
    public override int GetScore()
    {
        // ReSharper disable once PossibleLossOfFraction
        float score = (float)GoodMoves / (float)(AnswerButtons.Length * MaxRound) * 100;
        Debug.Log($"score: {score}, rounded: {Math.Round(score, 2)}");;
        return (int) Math.Round(score, 2);
    }
}
