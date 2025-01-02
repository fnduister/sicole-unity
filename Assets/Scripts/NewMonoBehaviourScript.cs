using UnityEngine;
using UnityEngine.UIElements;

public class NewMonoBehaviourScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        var doc = GetComponent<UIDocument>();
        var gameManager = GetComponent<Syllabe2>();
        doc.rootVisualElement.dataSource = gameManager;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
