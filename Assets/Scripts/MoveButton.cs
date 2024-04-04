using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MoveButton : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    [SerializeField] Canvas _canvas;
    [SerializeField] RectTransform _anchor;
    RectTransform m_transform;
    CanvasGroup m_canvasGroup;
    Vector2 initialPosition;

    private void Awake()
    {
        m_transform = GetComponent<RectTransform>();
        m_canvasGroup = GetComponent<CanvasGroup>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("beginning drag");
        m_canvasGroup.alpha = .5f;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("dragging");
        m_transform.anchoredPosition += eventData.delta;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        m_canvasGroup.alpha = 1f;
        Debug.Log("ending drag");
        m_transform.position = _anchor.position;

        Debug.Log(initialPosition);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
