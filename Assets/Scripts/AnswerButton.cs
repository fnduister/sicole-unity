using UnityEngine;
using UnityEngine.EventSystems;

public class AnswerButton : MonoBehaviour, IDropHandler
{
    public int buttonId;
    public string ExpectedLetter;
    public GameObject GameManager;
    public bool GoodAnswerPopulated = false;
    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag == null) return;
        
        eventData.pointerDrag.GetComponent<RectTransform>().position = GetComponent<RectTransform>().position;
        eventData.pointerDrag.GetComponent<MoveButton>().IsDroppedProperly = true;
        Debug.Log($"expected letter: {ExpectedLetter}, dropped letter: {eventData.pointerDrag.GetComponent<LetterButton>().letter}");
            
        // check if the dropped letter is the expected letter
        if (eventData.pointerDrag.GetComponent<LetterButton>().letter == ExpectedLetter)
        {
            Debug.Log("correct");
            // log the expected letter
            GoodAnswerPopulated = true;
            // move button can be moved anymore
            eventData.pointerDrag.GetComponent<MoveButton>().IsCorrect = true;
                
            GameManager.GetComponent<SyllabusGameManager>().ScoreUp();
                
            // chenge the color of the letter to green
            eventData.pointerDrag.GetComponent<LetterButton>().GetComponent<UnityEngine.UI.Image>().color = Color.green;
        }
        else
        {
            GameManager.GetComponent<SyllabusGameManager>().ScoreDown();
            eventData.pointerDrag.GetComponent<LetterButton>().GetComponent<UnityEngine.UI.Image>().color = Color.red;
            Debug.Log("incorrect");
        }
            
        Debug.Log("dropped");
    }
}
