using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class Connect : MonoBehaviour
{
    private TextField _username;
    private TextField _password;
    private Button _login;
    private Button _skipLogin;
    private void OnEnable()
    {
        AudioManager.Play("Casual Theme #1 (Looped)", AudioType.Ambient);
        var root = GetComponent<UIDocument>().rootVisualElement;
        _username = root.Q<TextField>("Username");
        _password = root.Q<TextField>("Password");
        _login = root.Q<Button>("Login");
        _skipLogin = root.Q<Button>("SkipLogin");
        
        if(_username == null || _password == null || _login == null || _skipLogin == null)
            throw new Exception("Missing fields in Connect");
        
        _login.clicked += OnLogin;
        _skipLogin.clicked += OnLogin;
    }

    private void OnLogin()
    {
        Debug.Log("Logging");
        UnityEngine.SceneManagement.SceneManager.LoadScene("GroupSelectionScene");
    }

    private void OnDisable()
    {
        _login.clicked -= OnLogin;
        _skipLogin.clicked -= OnLogin;
    }
}
