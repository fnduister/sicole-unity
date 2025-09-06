using System;
using JetBrains.Annotations;
using UnityEngine;
using Random = UnityEngine.Random;

public enum AudioClipType
{
    Positive,
    Negative,
    Background
}

[CreateAssetMenu(fileName = "GameConfig", menuName = "Game/GameConfig")]
[Serializable]
public class GameConfig : ScriptableObject
{
    [Header("Game Identity")] public string GameName;

    public GameType GameType;
    public GameCategory GameCategory;

    [Header("Game Settings")] public float TimeLimit = 30f;

    public float GameSpeed = 1f;
    public int MaxRounds = 3;

    [Header("Audio")] public AudioClip[] BackgroundAudios;

    public AudioClip[] NegativeAudios;
    public AudioClip[] PositiveAudios;

    [Header("Game-Specific Data")] public QuestionData[] Questions; // Only used for question games

    public ActionData[] Actions; // Only used for action games
    public PuzzleData PuzzleInfo; // Only used for puzzle games

    [CanBeNull]
    public AudioClip GetRandomAudio(AudioClipType audioClipType)
    {
        return audioClipType switch
        {
            AudioClipType.Positive => PositiveAudios[Random.Range(0, PositiveAudios.Length)],
            AudioClipType.Negative => NegativeAudios[Random.Range(0, NegativeAudios.Length)],
            AudioClipType.Background => BackgroundAudios[Random.Range(0, NegativeAudios.Length)],
            _ => null
        };
    }
}

// Different data structures for different game types
[Serializable]
public class QuestionData
{
    public string Question;
    public string[] Answers;
    public int CorrectAnswerIndex;
}

[Serializable]
public class ActionData
{
    public string ActionName;
    public float TimingWindow;
    public Sprite ActionSprite;
}

[Serializable]
public class PuzzleData
{
    public int GridSize;
    public Sprite[] PuzzlePieces;
    public Vector2[] CorrectPositions;
}