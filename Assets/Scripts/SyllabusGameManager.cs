using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class SyllabusGameManager: MonoBehaviour
{
    public int GoodMoves = 0;
    public int Moves = 0;
    public string RoundDisplay;
    public int MaxRound = 3;
    protected int _currentRound = 1;
    public ProgressBarTimer ProgressBarTimer;
    public int MaxStepsPerRound = 10;
    public int CurrentStep = 0;
    public AudioVoice PositiveAudioVoice; 
    public AudioVoice NegativeAudioVoice;
    public float roundProgress = 0;
    public float roundLimit = 100;
    [Range(1, 50)]
    public float speed = 1;
    
    void Start()
    {
        RoundDisplay = $"{_currentRound.ToString()}/{MaxRound.ToString()}";
        ProgressBarTimer.OnProgressBarUpdate += OnProgressBarChange;
        ProgressBarTimer.ValueLimit = roundLimit;
        ProgressBarTimer.Speed = speed;
        ProgressBarTimer.IsOneShot = true;
        SetRound();
    }

    protected virtual void SetRound()
    {
        throw new System.NotImplementedException();
    }

    // Remove listener when the object is destroyed
    void OnDestroy()
    {
        ProgressBarTimer.OnProgressBarUpdate -= OnProgressBarChange;
    }

    protected int[] GenerateDistinctIntegers(int n, int minValue, int maxValue)
    {
        var uniqueIntegers = new HashSet<int>();
        
        while (uniqueIntegers.Count < n)
        {
            int newInt = Random.Range(minValue, maxValue);
            uniqueIntegers.Add(newInt);
        }
        
        return uniqueIntegers.ToArray();
    }

    // function listening to progress bar event
    private void OnProgressBarChange(float progress)
    {
        roundProgress = progress;
        
        if (progress > roundLimit)
        {
            Debug.Log($"max was reached: {progress}");
            UpdateGameState();
        }
    }

    protected virtual void UpdateGameState()
    {
        throw new System.NotImplementedException();
    }

    public void ScoreUp()
    {
        ++GoodMoves;
        ++Moves;
        AudioManager.Play(PositiveAudioVoice.GetRandomClip());
    }
    
    public void ScoreDown()
    {
        ++Moves;
        AudioManager.Play(NegativeAudioVoice.GetRandomClip());
    }
}