using UnityEngine;
using System;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("Menu Panels")]
    public GameObject mainMenuPanel;
    public GameObject gameUIPanel;
    public GameObject pauseMenuPanel;
    public GameObject loadingPanel;
    public GameObject gameCompletePanel;

    [Header("UI Elements")]
    public Button pauseButton;
    public TMPro.TextMeshProUGUI timerText;
    public TMPro.TextMeshProUGUI scoreText;
    public TMPro.TextMeshProUGUI finalScoreText;

    // Events for SceneManager to subscribe to
    public event Action OnPauseRequested;
    public event Action OnResumeRequested;

    private void Start()
    {
        // Start in main menu state
        HandleStateChange(GameState.MainMenu);
    }

    // Called by pause button
    public void OnPauseButtonPressed()
    {
        OnPauseRequested?.Invoke();
    }

    // Called by resume button
    public void OnResumeButtonPressed()
    {
        OnResumeRequested?.Invoke();
    }

    // Main state handler - called by SceneManager
    public void HandleStateChange(GameState newState)
    {
        // Hide all panels first
        HideAllPanels();

        // Show appropriate panel based on state
        switch (newState)
        {
            case GameState.MainMenu:
                ShowMainMenu();
                break;

            case GameState.Loading:
                ShowLoading();
                break;

            case GameState.InGame:
                ShowGameUI();
                break;

            case GameState.Paused:
                ShowPauseMenu();
                break;

            case GameState.GameComplete:
                ShowGameComplete();
                break;

            default:
                Debug.LogWarning($"Unhandled state: {newState}");
                break;
        }
    }

    private void HideAllPanels()
    {
        mainMenuPanel?.SetActive(false);
        gameUIPanel?.SetActive(false);
        pauseMenuPanel?.SetActive(false);
        loadingPanel?.SetActive(false);
        gameCompletePanel?.SetActive(false);
    }

    private void ShowMainMenu()
    {
        mainMenuPanel?.SetActive(true);
    }

    private void ShowLoading()
    {
        loadingPanel?.SetActive(true);
    }

    private void ShowGameUI()
    {
        gameUIPanel?.SetActive(true);
        pauseButton?.gameObject.SetActive(true);
    }

    public void ShowPauseMenu()
    {
        pauseMenuPanel?.SetActive(true);
        pauseButton?.gameObject.SetActive(false);
    }

    public void HidePauseMenu()
    {
        pauseMenuPanel?.SetActive(false);
        pauseButton?.gameObject.SetActive(true);
    }

    private void ShowGameComplete()
    {
        gameCompletePanel?.SetActive(true);
    }

    // Game data update methods
    public void UpdateTimer(float timeRemaining)
    {
        if (timerText != null)
        {
            int minutes = Mathf.FloorToInt(timeRemaining / 60);
            int seconds = Mathf.FloorToInt(timeRemaining % 60);
            timerText.text = $"{minutes:00}:{seconds:00}";
        }
    }

    public void UpdateScore(int score, int maxScore)
    {
        if (scoreText != null)
        {
            scoreText.text = $"Score: {score}/{maxScore}";
        }
    }

    public void ShowGameResults(int finalScore, int maxScore)
    {
        if (finalScoreText != null)
        {
            float percentage = maxScore > 0 ? (float)finalScore / maxScore * 100f : 0f;
            finalScoreText.text = $"Final Score: {finalScore}/{maxScore} ({percentage:F1}%)";
        }
    }

    public void ShowTimerExpired()
    {
        // Show timer expired feedback (could be a popup, animation, etc.)
        Debug.Log("Time's up!");
    }

    // Transition methods (if you need loading animations)
    public System.Collections.IEnumerator StartTransition()
    {
        // Add fade out or transition animation here
        yield return new WaitForSeconds(0.5f);
    }

    public System.Collections.IEnumerator CompleteTransition()
    {
        // Add fade in or transition animation here
        yield return new WaitForSeconds(0.5f);
    }
}