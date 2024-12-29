using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MoveButton : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    [SerializeField] Canvas _canvas;
    [SerializeField] RectTransform _anchor;

    RectTransform _transform;
    CanvasGroup _canvasGroup;
    public bool IsDroppedProperly = false;

    private void Awake()
    {
        _transform = GetComponent<RectTransform>();
        _canvasGroup = GetComponent<CanvasGroup>();
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
        _transform.position = _anchor.position;
        GetComponent<LetterButton>().GetComponent<UnityEngine.UI.Image>().color = Color.white;
    }
}
