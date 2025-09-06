using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class GroupSelection : MonoBehaviour
{
    private Button _below4Button;
    private Button _between4And8Button;
    private Button _between8And12Button;
    private Button _above12Button;
    
    private void OnEnable()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;
        _below4Button = root.Q<Button>("Below4Button");
        _between4And8Button = root.Q<Button>("Between4And8Button");
        _between8And12Button = root.Q<Button>("Between8And12Button");
        _above12Button = root.Q<Button>("Above12Button");
        
        if(_below4Button == null || _between4And8Button == null || _between8And12Button == null || _above12Button == null)
            throw new System.Exception("Missing fields in GroupSelection");
        
        _below4Button.clicked += LoadGameSelectionScene;
        _between4And8Button.clicked += LoadGameSelectionScene;
        _between8And12Button.clicked += LoadGameSelectionScene;
        _above12Button.clicked += LoadGameSelectionScene;
    }

    private void LoadGameSelectionScene()
    {
        Debug.Log("loading scene");
        UnityEngine.SceneManagement.SceneManager.LoadScene("CategoryScene");
    }

    private void OnDisable()
    {
        _below4Button.clicked -= LoadGameSelectionScene;
        _between4And8Button.clicked -= LoadGameSelectionScene;
        _between8And12Button.clicked -= LoadGameSelectionScene;
        _above12Button.clicked -= LoadGameSelectionScene;
    }
}
