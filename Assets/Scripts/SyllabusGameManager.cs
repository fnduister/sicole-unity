using UnityEngine;

public class SyllabusGameManager : GameManager
{
    [Header("Syllabus Game Settings")]
    public string RoundDisplay;

    private QuestionManager _questionManager;
    private int _correctAnswers = 0;
    private int _totalQuestions = 0;

    public override GameCategory GameCategory => GameCategory.Syllabus;

    protected override void InitializeGameSpecificData(GameConfig.SceneData gameData)
    {
        _questionManager = GetComponent<QuestionManager>();
        if (_questionManager == null)
        {
            _questionManager = gameObject.AddComponent<QuestionManager>();
        }

        // Get questions directly from GameConfig scriptable object
        if (gameData.Questions == null || gameData.Questions.Length == 0)
        {
            Debug.LogError("No questions found in GameConfig scriptable object!");
            return;
        }

        InitializeQuestionsFromGameConfig(gameData.Questions);
    }

    private void InitializeQuestionsFromGameConfig(GameConfig.QuestionData[] questionData)
    {
        // Convert GameConfig.QuestionData to Question objects
        var questions = new Question[questionData.Length];
        for (int i = 0; i < questionData.Length; i++)
        {
            questions[i] = new Question
            {
                QuestionText = questionData[i].Question,
                Choices = questionData[i].Answers,
                CorrectAnswerIndex = questionData[i].CorrectAnswerIndex
            };
        }

        _questionManager.SetQuestions(questions);
        _totalQuestions = questions.Length;
        
        Debug.Log($"Loaded {_totalQuestions} questions from GameConfig");
    }

    public override int GetScore()
    {
        return _correctAnswers;
    }

    protected override void UpdateGameState()
    {
        if (_questionManager != null && _questionManager.IsQuestionAnswered())
        {
            if (_questionManager.IsLastAnswerCorrect())
            {
                _correctAnswers++;
                OnPositiveAction();
            }
            else
            {
                OnNegativeAction();
            }

            _currentRound++;

            if (_currentRound <= MaxRounds && _questionManager.HasMoreQuestions())
            {
                _questionManager.ShowNextQuestion();
                UpdateRoundDisplay();
            }
            else
            {
                CheckGameStatus();
            }
        }
    }

    protected override void CheckGameStatus()
    {
        if (_currentRound > MaxRounds || !_questionManager.HasMoreQuestions())
        {
            UIManager?.ShowGameComplete(GetScore(), _totalQuestions);
        }
    }

    private void UpdateRoundDisplay()
    {
        RoundDisplay = $"Round {_currentRound}/{MaxRounds}";
        UIManager?.UpdateRoundDisplay(RoundDisplay);
    }

    public void AnswerSelected(int answerIndex)
    {
        if (_questionManager != null && !_questionManager.IsQuestionAnswered())
        {
            _questionManager.AnswerQuestion(answerIndex);
            UpdateGameState();
        }
    }
}