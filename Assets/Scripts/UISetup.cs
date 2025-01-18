using UnityEngine;
using UnityEngine.UIElements;

public class UISetup : MonoBehaviour
{
    public SyllabusGameManager GameManager;
    private Button _pauseButton;
    
    private void OnEnable()
    {
        
        var root = GetComponent<UIDocument>().rootVisualElement;
        
        _pauseButton = root.Q<Button>("PauseButton");
        
        if(_pauseButton == null)
            throw new System.Exception("Missing fields in GroupSelection");
        
        _pauseButton.clicked += () => GameManager.PauseGame();
    }
    
    void Start()
    {
        var document = GetComponent<UIDocument>();
        document.rootVisualElement.dataSource = GameManager;
    }
}
