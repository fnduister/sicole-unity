using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public enum GameCategory
{
    Letters,
    Numbers,
    Puzzles,
    Drawing,
    Syllabus
}

public class SelectCategory : MonoBehaviour
{
    private Button _lettersButton;
    private Button _numbersButton;
    private Button _drawingButton;
    private Action _lettersButtonAction;
    private Action _numbersButtonAction;
    private Action _drawingButtonAction;

    private void OnEnable()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;
        _lettersButton = root.Q<Button>("LettersButton");
        _numbersButton = root.Q<Button>("NumbersButton");
        _drawingButton = root.Q<Button>("DrawingButton");

        if (root == null || _lettersButton == null || _numbersButton == null || _drawingButton == null)
            throw new Exception("Missing fields in SelectCategory");

        _lettersButtonAction = () => Select(GameCategory.Letters);
        _numbersButtonAction = () => Select(GameCategory.Numbers);
        _drawingButtonAction = () => Select(GameCategory.Drawing);

        _lettersButton.clicked += _lettersButtonAction;
        _numbersButton.clicked += _numbersButtonAction;
        _drawingButton.clicked += _drawingButtonAction;
    }

    private void OnDisable()
    {
        if (_lettersButton != null) _lettersButton.clicked -= _lettersButtonAction;
        if (_numbersButton != null) _numbersButton.clicked -= _numbersButtonAction;
        if (_drawingButton != null) _drawingButton.clicked -= _drawingButtonAction;
    }

    private void Select(GameCategory category)
    {
        Debug.Log(category);
        switch (category)
        {
            case GameCategory.Letters:
                UnityEngine.SceneManagement.SceneManager.LoadScene("GameListScene");
                Debug.Log("Letters");
                break;
            case GameCategory.Numbers:
                UnityEngine.SceneManagement.SceneManager.LoadScene("GameListScene");
                Debug.Log("Numbers");
                break;
            case GameCategory.Puzzles:
                UnityEngine.SceneManagement.SceneManager.LoadScene("GameListScene");
                Debug.Log("Puzzles");
                break;
            case GameCategory.Drawing:
                UnityEngine.SceneManagement.SceneManager.LoadScene("GameListScene");
                Debug.Log("Drawing");
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(category), category, null);
        }
    }
}