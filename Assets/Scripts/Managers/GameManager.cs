using System;
using UnityEngine;

public abstract class GameManager
{
    // Events
    public event Action<int, int> OnGameCompleted;
    public event Action OnGameStarted;
    public event Action OnRoundCompleted;
    public event Action<float> OnTimerUpdated;
    public event Action OnTimerExpired;

    // Core game state
    protected GameConfig _currentGameData;
    protected bool isPaused = false;

    // Timer using existing ProgressBarTimer
    protected ProgressBarTimer timer;
    protected bool hasTimeLimit;

    public abstract GameCategory GameCategory { get; }

    public virtual void Initialize(GameConfig gameConfig)
    {
        _currentGameData = gameConfig;

        // Set up timer if game has time limit
        hasTimeLimit = gameConfig.TimeLimit > 0;

        if (hasTimeLimit)
        {
            SetupTimer(gameConfig.TimeLimit);
        }

        InitializeGameSpecificData(gameConfig);
        OnGameStarted?.Invoke();
    }

    protected virtual void SetupTimer(float timeLimit)
    {
        // Create timer GameObject
        var timerObject = new GameObject("GameTimer");
        timer = timerObject.AddComponent<ProgressBarTimer>();

        // Configure timer
        timer.MinValue = 0;
        timer.ValueLimit = timeLimit;
        timer.Speed = 1f; // 1 unit per second
        timer.IsOneShot = true; // Stop when time runs out

        // Subscribe to timer events
        timer.OnProgressBarUpdate += OnTimerProgressUpdate;
        timer.OnProgressBarLimitReached += OnTimerLimitReached;

        // Start the timer
        timer.Reset();
    }

    private void OnTimerProgressUpdate(float progress)
    {
        // Convert progress to time remaining
        float timeRemaining = timer.ValueLimit - progress;
        OnTimerUpdated?.Invoke(timeRemaining);
    }

    private void OnTimerLimitReached()
    {
        OnTimerExpired?.Invoke();
        HandleTimerExpired();
    }

    protected virtual void HandleTimerExpired()
    {
        // Default behavior - complete game with current score
        CompleteGame();
    }

    public virtual void PauseGame()
    {
        isPaused = true;
        timer?.Pause();
    }

    public virtual void ResumeGame()
    {
        isPaused = false;
        if (timer != null && hasTimeLimit)
        {
            timer.Start();
        }
    }

    protected virtual void CompleteGame()
    {
        timer?.Pause(); // Stop the timer
        OnGameCompleted?.Invoke(GetScore(), GetMaxScore());
    }

    public virtual void Cleanup()
    {
        if (!timer) return;
        
        timer.OnProgressBarUpdate -= OnTimerProgressUpdate;
        timer.OnProgressBarLimitReached -= OnTimerLimitReached;
        UnityEngine.Object.Destroy(timer.gameObject);
        timer = null;
    }

    // Properties
    public bool HasTimeLimit => hasTimeLimit;

    // Rest of existing methods...
    public virtual void OnPositiveAction()
    {
        var positiveClip = _currentGameData?.GetRandomAudio(AudioClipType.Positive);
        if (positiveClip != null)
        {
            AudioManager.Play(positiveClip, AudioType.SFX);
        }
        CheckGameStatus();
    }

    public virtual void OnNegativeAction()
    {
        var negativeClip = _currentGameData?.GetRandomAudio(AudioClipType.Negative);
        if (negativeClip != null)
        {
            AudioManager.Play(negativeClip, AudioType.SFX);
        }
        CheckGameStatus();
    }

    // Abstract methods
    protected abstract void InitializeGameSpecificData(GameConfig gameData);
    public abstract int GetScore();
    public abstract int GetMaxScore();
    protected abstract void CheckGameStatus();
}