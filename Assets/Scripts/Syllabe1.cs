using System;
using UnityEngine;
using UnityEngine.UI;

public class Syllabe1 : SyllabusGameManager
{
    [SerializeField] QuestionSO PossibleSyllabes;
    [SerializeField] AudioManager AudioManager;
    [SerializeField] GameObject[] SyllabeButtons = new GameObject[4];
    [SerializeField] public Button SoundButton;
    public int GoodMoves = 0;
    private bool _wonRound = false;
    private bool _isNewGame = true;
    public int Moves = 0;

    public int MaxMoves = 30;
    
    protected override void SetRound()
    {
        if (_isNewGame)
        {
            ProgressBarTimer.Reset();
            _isNewGame = false;
        }
        
        _wonRound = false;

        // get 4 random syllabes
        string[] syllabes = PossibleSyllabes.GetChoices(4).ToArray();

        var answerIndex = Helpers.GenerateDistinctIntegers(1, 0, 4);

        for(var i = 0; i < 4; i++)
        {
            SyllabeButtons[i].GetComponentInChildren<TMPro.TextMeshProUGUI>().text = syllabes[i];  
            SyllabeButtons[i].GetComponent<StaticAnswerButton>().isAnswer = i == answerIndex[0];
            // set the sound buttons expected letter
            SoundButton.GetComponent<SoundButton>().syllabus = syllabes[answerIndex[0]];
        }
        
        // reset the letter buttons
        foreach (var button in SyllabeButtons)
        {
            button.GetComponent<StaticAnswerButton>().Reset();
        }

    }
        
    protected override void UpdateGameState()
    {
        if (roundProgress < 100 && _currentRound < MaxRound)
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
            _isNewGame = true;
        }
    }
    
    protected override void CheckGameStatus()
    {
        if(_wonRound) Invoke(nameof(UpdateGameState), 1);
    }

    public override void ScoreUp()
    {
        ++GoodMoves;
        _wonRound = true;
        base.ScoreUp();
    }
    
    public override void ScoreDown()
    {
        --Moves;
        base.ScoreDown();
    }
    
    protected override void Reset()
    {
        GoodMoves = 0;
        _currentRound = 1;
        RoundDisplay = $"{_currentRound.ToString()}/{MaxRound.ToString()}";
        base.Reset();
    }
    
    public override int GetScore()
    {
        // ReSharper disable once PossibleLossOfFraction
        float score = (float)GoodMoves / (float)(MaxRound) * 100;
        Debug.Log($"score: {score}, rounded: {Math.Round(score, 2)}");
        return (int) Math.Round(score, 2);
    }
}
