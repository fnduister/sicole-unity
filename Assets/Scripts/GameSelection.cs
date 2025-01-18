using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class GameSelection : MonoBehaviour
{
    private Button _syllabus1Button;
    
    private void OnEnable()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;
        _syllabus1Button = root.Q<Button>("Syllabus1Button");
        
        if(_syllabus1Button == null)
            throw new System.Exception("Missing fields in GroupSelection");
        
        _syllabus1Button.clicked += LoadGameSelectionScene;
    }

    private void LoadGameSelectionScene()
    {
        Debug.Log("loading scene");
        SceneManager.LoadScene("Syllabe2");
    }

    private void OnDisable()
    {
        _syllabus1Button.clicked -= LoadGameSelectionScene;
    }
}
