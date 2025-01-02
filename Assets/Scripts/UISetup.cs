using UnityEngine;
using UnityEngine.UIElements;

public class UISetup : MonoBehaviour
{
    public SyllabusGameManager GameManager;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        var document = GetComponent<UIDocument>();
        document.rootVisualElement.dataSource = GameManager;
    }
}
