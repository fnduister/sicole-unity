using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class PauseGameMenu : MonoBehaviour
{
    private Button _quitButton;
    private Button _resumeButton;
    private Button _restartButton;
    public SyllabusGameManager GameManager;

    private void OnEnable()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;
        _quitButton = root.Q<Button>("QuitButton");
        _resumeButton = root.Q<Button>("ResumeButton");
        _restartButton = root.Q<Button>("RestartButton");
        
        if(_quitButton == null || _resumeButton == null || _restartButton == null)
            throw new System.Exception("Missing fields in GroupSelection");
        
        _quitButton.clicked += () => UnityEngine.SceneManagement.SceneManager.LoadScene("GameListScene");
        _resumeButton.clicked += () => GameManager.ResumeGame();
        _restartButton.clicked += () => GameManager.Start();
    }
}
