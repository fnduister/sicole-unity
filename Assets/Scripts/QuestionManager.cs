using UnityEngine;

public class QuestionManager : MonoBehaviour
{
    private Question[] _questions;
    private int _currentQuestionIndex = 0;
    private bool _isQuestionAnswered = false;
    private bool _isLastAnswerCorrect = false;

    public void SetQuestions(Question[] questions)
    {
        _questions = questions;
        _currentQuestionIndex = 0;
        _isQuestionAnswered = false;
    }

    public Question GetCurrentQuestion()
    {
        if (_questions != null && _currentQuestionIndex < _questions.Length)
            return _questions[_currentQuestionIndex];
        return null;
    }

    public void AnswerQuestion(int answerIndex)
    {
        var currentQuestion = GetCurrentQuestion();
        if (currentQuestion != null)
        {
            _isLastAnswerCorrect = answerIndex == currentQuestion.CorrectAnswerIndex;
            _isQuestionAnswered = true;
        }
    }

    public void ShowNextQuestion()
    {
        if (_currentQuestionIndex < _questions.Length - 1)
        {
            _currentQuestionIndex++;
            _isQuestionAnswered = false;
        }
    }

    public bool IsQuestionAnswered() => _isQuestionAnswered;
    public bool IsLastAnswerCorrect() => _isLastAnswerCorrect;
    public bool HasMoreQuestions() => _currentQuestionIndex < _questions.Length - 1;
}