using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Syllabe2 : MonoBehaviour
{
    public string[] PossibleSyllabes = new string[8];
    public GameObject[] Letterbuttons = new GameObject[8];
    public GameObject[] SoundButtons = new GameObject[4];
    public GameObject[] AnswerButtons = new GameObject[8];
    public TextMeshProUGUI RoundDisplay;
    public TextMeshProUGUI MaxRoundDisplay;
    public GameObject GoodMoveDisplay;
    public GameObject BadMoveDisplay;
    private int BadMove = 0;
    private int GoodMove = 0;
    public int MaxRound = 3;
    private int _currentRound = 1;
    public int MaxStepsPerRound = 10;
    public int CurrentStep = 0;
    public AudioVoice PositiveAudioVoice; 
    public AudioVoice NegativeAudioVoice; 
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        setRound();
        RoundDisplay.text = _currentRound.ToString();
        MaxRoundDisplay.text = MaxRound.ToString();
        GoodMoveDisplay.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = GoodMove.ToString();
        BadMoveDisplay.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = BadMove.ToString();

    }

    private void setRound()
    {
        // reset the letter buttons
        foreach (var button in Letterbuttons)
        {
            button.GetComponent<MoveButton>().ResetButtonPosition();
        }
        
        // get 4 random syllabes
        string[] syllabes = new string[4];
        
        var possibleSyllabesIndexes = GenerateDistinctIntegers(4, 0, 8);
        
        for (int i = 0; i < 4; i++)
        {
            syllabes[i] = PossibleSyllabes[possibleSyllabesIndexes[i]];
        }

        var letterRandomIndex = GenerateDistinctIntegers(8, 0, 8);
        
        for (int i = 0; i < 4; i++)
        {
            var first = i * 2;
            var second = i * 2 + 1;
            Letterbuttons[letterRandomIndex[first]].GetComponentInChildren<TMPro.TextMeshProUGUI>().text = syllabes[i][0].ToString();
            Letterbuttons[letterRandomIndex[first]].GetComponent<LetterButton>().letter = syllabes[i][0].ToString();
            Letterbuttons[letterRandomIndex[second]].GetComponentInChildren<TMPro.TextMeshProUGUI>().text = syllabes[i][1].ToString();
            Letterbuttons[letterRandomIndex[second]].GetComponent<LetterButton>().letter = syllabes[i][1].ToString();
            
            // set the answer buttons expected letter
            AnswerButtons[first].GetComponent<AnswerButton>().ExpectedLetter = syllabes[i][0].ToString();
            AnswerButtons[second].GetComponent<AnswerButton>().ExpectedLetter = syllabes[i][1].ToString();
            
            // set the sound buttons expected letter
            SoundButtons[i].GetComponent<SoundButton>().syllabus = syllabes[i];
        }
    }

    private void UpdateGameState()
    {
        if (_currentRound < MaxRound)
        {
            _currentRound++;
            RoundDisplay.text = _currentRound.ToString();
            setRound();
        }
        else
        {
            Debug.Log("Game Over");
        }
    }
    
    public void ScoreUp()
    {
        GoodMoveDisplay.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = (++GoodMove).ToString();
        AudioManager.Play(PositiveAudioVoice.GetRandomClip());
    }
    
    public void ScoreDown()
    {
        BadMoveDisplay.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = (++BadMove).ToString();
        AudioManager.Play(NegativeAudioVoice.GetRandomClip());
    }
    
    public int[] GenerateDistinctIntegers(int n, int minValue, int maxValue)
    {
        HashSet<int> uniqueIntegers = new HashSet<int>();
        
        while (uniqueIntegers.Count < n)
        {
            int newInt = Random.Range(minValue, maxValue);
            uniqueIntegers.Add(newInt);
        }
        
        return uniqueIntegers.ToArray();
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
}
