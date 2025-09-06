using UnityEngine;
using UnityEngine.UIElements;

public class LoginController : MonoBehaviour
{
    private Button _loginButton;
    private TextField _usernameField;

    private void OnEnable()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;
        var loginPanel = root.Q<VisualElement>("LoginPanel");

        _loginButton = loginPanel.Q<Button>("LoginButton");
        _usernameField = loginPanel.Q<TextField>("UsernameField");

        _loginButton.clicked += OnLoginClicked;
    }

    private void OnLoginClicked()
    {
        if (!string.IsNullOrEmpty(_usernameField.value))
        {
            // Handle login logic
            UIStateManager.Instance.SetState(UIState.GroupSelection);
        }
    }
}