using System;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class EndGameMenu : MonoBehaviour
{
    private Label _star1;
    private Label _star2;
    private Label _star3;
    private Button _restartButton;
    private Button _scoreButton;
    private Button _bestScoreButton;
    public GameObject Manager;
    private SyllabusGameManager _manager;

    private void OnEnable()
    {
        _manager = Manager.GetComponent<SyllabusGameManager>();
        
        var root = GetComponent<UIDocument>().rootVisualElement;
        
        _star1 = root.Q<Label>("Star1");
        _star2 = root.Q<Label>("Star2");
        _star3 = root.Q<Label>("Star3");
        _scoreButton = root.Q<Button>("ScoreButton");
        _bestScoreButton = root.Q<Button>("BestScoreButton");
        
        _restartButton = root.Q<Button>("PlayAgainButton");
        
        if(_star1 == null || _star2 == null || _star3 == null || _restartButton == null)
            throw new System.Exception("Missing fields in GroupSelection");
        
        _scoreButton.text = $"{_manager.GetScore()}%";
        
        CalculateStars(_manager.GetScore());
        
        _restartButton.clicked += () => _manager.Start();
    }

    private void CalculateStars(int score)
    {
        var currentStar1 = _star1.GetClasses().Where(c => c.Contains("star-")).ToList();
        var currentStar2 = _star2.GetClasses().Where(c => c.Contains("star-")).ToList();
        var currentStar3 = _star3.GetClasses().Where(c => c.Contains("star-")).ToList();

        _star1.RemoveFromClassList(currentStar1[0]);
        _star2.RemoveFromClassList(currentStar2[0]);
        _star3.RemoveFromClassList(currentStar3[0]);

        switch (score)
        {
            case 0:
                _star1.AddToClassList("star-empty");
                _star2.AddToClassList("star-empty");
                _star3.AddToClassList("star-empty");
                break;
            case < 18 and >= 0:
                _star1.AddToClassList("star-half");
                _star2.AddToClassList("star-empty");
                _star3.AddToClassList("star-empty");
                break;
            case < 36 and >= 18:
                _star1.AddToClassList("star-full");
                _star2.AddToClassList("star-empty");
                _star3.AddToClassList("star-empty");
                break;
            case < 54 and >= 36:
                _star1.AddToClassList("star-full");
                _star2.AddToClassList("star-half");
                _star3.AddToClassList("star-empty");
                break;
            case < 72 and >= 54:
                _star1.AddToClassList("star-full");
                _star2.AddToClassList("star-full");
                _star3.AddToClassList("star-empty");
                break;
            case < 90 and >= 72:
                _star2.AddToClassList("star-full");
                _star2.AddToClassList("star-full");
                _star2.AddToClassList("star-half");
                break;
            case <= 100 and >= 90:
                _star1.AddToClassList("star-full");
                _star2.AddToClassList("star-full");
                _star3.AddToClassList("star-full");
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(score), score, null);
        }
    }
}
