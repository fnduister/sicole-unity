using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;

public class SyllabusGameManager: MonoBehaviour
{
    public string RoundDisplay;
    [Range(1, 50)]
    public int MaxRound = 3;
    protected int _currentRound = 1;
    public ProgressBarTimer ProgressBarTimer;
    public AudioVoice PositiveAudioVoice; 
    public AudioVoice NegativeAudioVoice;
    public float roundProgress = 0;
    public float roundLimit = 100;
    [Range(1, 50)]
    public float speed = 1;
    public GameObject PauseGameMenu;
    public GameObject GameOverMenu;

    public virtual int GetScore()
    {
        throw new System.NotImplementedException();
    }
    
    protected virtual void Reset()
    {
        ProgressBarTimer.OnProgressBarLimitReached -= UpdateGameState;
        ProgressBarTimer.OnProgressBarUpdate -= OnProgressBarChange;
        Time.timeScale = 1;
        ProgressBarTimer.Reset();
        SetRound();
    }

    public void Start()
    {
        Reset();
        ProgressBarTimer.OnProgressBarUpdate += OnProgressBarChange;
        ProgressBarTimer.OnProgressBarLimitReached += UpdateGameState;
        ProgressBarTimer.ValueLimit = roundLimit;
        ProgressBarTimer.Speed = speed;
        ProgressBarTimer.IsOneShot = true;
        // Instantiate a new pause menu and game over menu
        PauseGameMenu.SetActive(false);
        GameOverMenu.SetActive(false);
    }

    protected virtual void SetRound()
    {
        throw new System.NotImplementedException();
    }

    // Remove listener when the object is destroyed
    void OnDisable()
    {
        ProgressBarTimer.OnProgressBarLimitReached -= UpdateGameState;
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
    }

    protected virtual void UpdateGameState()
    {
        throw new System.NotImplementedException();
    }
    
    public virtual void PauseGame()
    {
        Time.timeScale = 0;
        PauseGameMenu.GetComponent<UIDocument>().sortingOrder = 50;
        ProgressBarTimer.Pause();
        PauseGameMenu.SetActive(true);
    }
    
    public virtual void ResumeGame()
    {
        Time.timeScale = 1;
        ProgressBarTimer.Start();
        PauseGameMenu.SetActive(false);
    }

    public virtual void ScoreUp()
    {
        CheckGameStatus();
        AudioManager.Play(PositiveAudioVoice.GetRandomClip(), AudioType.Music);
    }
    
    protected virtual void CheckGameStatus()
    {
        throw new System.NotImplementedException();
    }
    
    public virtual void ScoreDown()
    {
        CheckGameStatus();
        AudioManager.Play(NegativeAudioVoice.GetRandomClip(), AudioType.Music);
    }
}