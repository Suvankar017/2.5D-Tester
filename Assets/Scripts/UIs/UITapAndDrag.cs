using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UITapAndDrag : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public bool isTappable = true;
    public bool isDraggable = true;

    public event Action OnTap;
    public event Action OnDragBegin;
    public event Action OnDragEnd;

    private ScrollRect scrollRect;
    private bool isVerticalDragging;
    private bool hasMouseMoved;

    private void Awake()
    {
        scrollRect = GetComponentInParent<ScrollRect>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        hasMouseMoved = false;
        scrollRect.velocity = Vector2.zero;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (!hasMouseMoved && isTappable)
            OnTap?.Invoke();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        Vector2 delta = eventData.position - eventData.pressPosition;

        if (Mathf.Abs(delta.y) > Mathf.Abs(delta.x))
        {
            isVerticalDragging = true;
            scrollRect.OnBeginDrag(eventData);
        }
        else
        {
            if (isDraggable)
                OnDragBegin?.Invoke();
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        hasMouseMoved = true;

        if (isVerticalDragging)
            scrollRect.OnDrag(eventData);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (isVerticalDragging)
            scrollRect.OnEndDrag(eventData);
        else
        {
            if (isDraggable)
                OnDragEnd?.Invoke();
        }

        isVerticalDragging = false;
    }
}
