using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MoveButton : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    [SerializeField] Canvas _canvas;
    [SerializeField] RectTransform _anchor;
    [SerializeField] Button _button; // Add a reference to the Button component

    RectTransform _transform;
    CanvasGroup _canvasGroup;
    public bool IsDroppedProperly = false;
    
    // TODO: make the button not draggable if it is already in the correct position 
    public bool IsDraggable = true;
    public bool IsCorrect = false;

    private void Awake()
    {
        _transform = GetComponent<RectTransform>();
        _canvasGroup = GetComponent<CanvasGroup>();
        _button = GetComponent<Button>(); // Initialize the Button component
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("beginning drag");
        _canvasGroup.alpha = .5f;
        _canvasGroup.blocksRaycasts = false;
        eventData.pointerDrag.GetComponent<LetterButton>().GetComponent<UnityEngine.UI.Image>().color = Color.white;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("dragging");
        _transform.anchoredPosition += eventData.delta;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        _canvasGroup.alpha = 1f;
        _canvasGroup.blocksRaycasts = true;

        if (!IsDroppedProperly)
        {
            _transform.position = _anchor.position;
        }
        
        IsDroppedProperly = false;
        Debug.Log("ending drag");
    }

    public void ResetButtonPosition()
    {
        IsDraggable = true;
        _transform.position = _anchor.position;
        GetComponent<LetterButton>().GetComponent<UnityEngine.UI.Image>().color = Color.white;
    }
}