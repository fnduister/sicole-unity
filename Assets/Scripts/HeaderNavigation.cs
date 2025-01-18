using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class HeaderNavigation : MonoBehaviour
{
    private Button _homeButton;
    private Button _settingButton;
    private System.Action _homeButtonAction;

    private void OnEnable()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;
        _homeButton = root.Q<Button>("HomeButton");
        _settingButton = root.Q<Button>("SettingButton");

        if (_homeButton != null)
        {
            _homeButtonAction = () => SceneManager.LoadScene("GroupSelectionScene");
            _homeButton.clicked += _homeButtonAction;
        }
        else
        {
            Debug.Log("Missing fields in GroupSelection");
        }
        
    }

    private void OnDisable()
    {
        if (_homeButton != null) _homeButton.clicked -= _homeButtonAction;
    }
}