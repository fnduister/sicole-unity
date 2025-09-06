using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class GameSelection : MonoBehaviour
{
    private Button _syllabus1Button;
    private Button _syllabus2Button;
    
    private void OnEnable()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;
        _syllabus1Button = root.Q<Button>("Syllabus1Button");
        _syllabus2Button = root.Q<Button>("Syllabus2Button");
        
        if(_syllabus1Button == null)
            throw new System.Exception("Missing fields in GroupSelection");
        
        _syllabus1Button.clicked += () => LoadGameSelectionScene("Syllabe1");
        _syllabus2Button.clicked += () => LoadGameSelectionScene("Syllabe2");
    }

    private void LoadGameSelectionScene(string scene)
    {
        Debug.Log("loading scene");
        UnityEngine.SceneManagement.SceneManager.LoadScene(scene);
    }

    private void OnDisable()
    {
        // _syllabus1Button.clicked -= LoadGameSelectionScene;
    }
}
