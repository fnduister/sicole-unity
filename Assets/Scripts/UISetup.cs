using UnityEngine;
using UnityEngine.UIElements;

public class UISetup : MonoBehaviour
{
    public GameManager GameManager;
    private Button _pauseButton;
    [SerializeField] private GameConfig[] availableGameConfigs; // Different game types
    
    public void OnGameButtonClicked(int gameTypeIndex)
    {
        if (gameTypeIndex >= availableGameConfigs.Length) return;
        
        // Set the config before starting the game
        SceneManager.Instance.StartGame(availableGameConfigs[gameTypeIndex]);
    }
    
    private void OnEnable()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;
        
        _pauseButton = root.Q<Button>("PauseButton");
        
        if(_pauseButton == null)
            throw new System.Exception("Missing fields in GroupSelection");
    }
    
    void Start()
    {
        var document = GetComponent<UIDocument>();
        document.rootVisualElement.dataSource = GameManager;
    }
}
