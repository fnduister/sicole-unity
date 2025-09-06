using UnityEngine;
using System.Collections;

public class SceneManager : MonoBehaviour
{
    [Header("Scene Configuration")]
    public GameConfig GameConfig;

    [Header("Managers")]
    public UIManager UIManager;

    private GameManager _currentGameManager;
    private GameState _currentGameState = GameState.MainMenu;
    
    public static SceneManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            
            // Subscribe to UI events
            UIManager.OnPauseRequested += HandlePauseRequested;
            UIManager.OnResumeRequested += HandleResumeRequested;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void StartGame(GameConfig gameConfig)
    {
        if (gameConfig == null)
        {
            Debug.LogError("Game data is null");
            return;
        }

        StartCoroutine(LoadGameCoroutine(gameConfig));
    }

    private IEnumerator LoadGameCoroutine(GameConfig gameConfig)
    {
        SetGameState(GameState.Loading);
        yield return StartCoroutine(UIManager.StartTransition());

        _currentGameManager = CreateGameManager(gameConfig.GameType);

        // Subscribe to all GameManager events
        _currentGameManager.OnGameCompleted += OnGameCompleted;
        _currentGameManager.OnGameStarted += OnGameStarted;
        _currentGameManager.OnRoundCompleted += OnRoundCompleted;
        _currentGameManager.OnTimerUpdated += OnTimerUpdated;
        _currentGameManager.OnTimerExpired += OnTimerExpired;

        _currentGameManager.Initialize(gameConfig);

        SetGameState(GameState.InGame);
        yield return StartCoroutine(UIManager.CompleteTransition());
    }

    private GameManager CreateGameManager(GameType gameType)
    {
        return gameType switch
        {
            GameType.MultipleChoice or GameType.TrueFalse or GameType.Syllabus
                => new SyllabusGameManager(),
            _ => throw new System.NotImplementedException($"Game type {gameType} not implemented!")
        };
    }

    // UI event handlers
    private void HandlePauseRequested()
    {
        if (_currentGameState == GameState.InGame)
        {
            _currentGameManager?.PauseGame();
        }
    }

    private void HandleResumeRequested()
    {
        if (_currentGameState == GameState.Paused)
        {
            _currentGameManager?.ResumeGame();
        }
    }

    // GameManager event handlers
    private void OnGameCompleted(int score, int maxScore)
    {
        SetGameState(GameState.GameComplete);
        UIManager?.ShowGameResults(score, maxScore);
        UnsubscribeFromGameManager();
    }

    private void OnGameStarted()
    {
        Debug.Log("Game started successfully");
    }

    private void OnRoundCompleted()
    {
        Debug.Log("Round completed");
    }

    private void OnGamePaused()
    {
        SetGameState(GameState.Paused);
        UIManager?.ShowPauseMenu();
    }

    private void OnGameResumed()
    {
        SetGameState(GameState.InGame);
        UIManager?.HidePauseMenu();
    }

    private void OnTimerUpdated(float timeRemaining)
    {
        UIManager?.UpdateTimer(timeRemaining);
    }

    private void OnTimerExpired()
    {
        Debug.Log("Timer expired!");
        UIManager?.ShowTimerExpired();
    }

    public void SetGameState(GameState newState)
    {
        if (_currentGameState != newState)
        {
            _currentGameState = newState;
            UIManager?.HandleStateChange(newState);
        }
    }

    public void ReturnToMainMenu()
    {
        UnsubscribeFromGameManager();
        SetGameState(GameState.MainMenu);
    }

    private void UnsubscribeFromGameManager()
    {
        if (_currentGameManager != null)
        {
            _currentGameManager.OnGameCompleted -= OnGameCompleted;
            _currentGameManager.OnGameStarted -= OnGameStarted;
            _currentGameManager.OnRoundCompleted -= OnRoundCompleted;
            _currentGameManager.OnTimerUpdated -= OnTimerUpdated;
            _currentGameManager.OnTimerExpired -= OnTimerExpired;
            _currentGameManager.Cleanup();
            _currentGameManager = null;
        }
    }

    // Properties
    public GameState CurrentState => _currentGameState;
}