using UnityEngine;
using UnityEngine.UI;
using ScriptableObjectArchitecture;

[RequireComponent(typeof(UITapAndDrag), typeof(Image))]
public abstract class TapAndDragSelectionUI<Element, ElementVariable> : MonoBehaviour where Element : ScriptableObject where ElementVariable : Variable<Element>
{
    public Image image;
    public Element element;
    public ElementVariable selectedElement;
    public GameEvent tapEvent;
    public GameEvent dragBeginEvent;
    public GameEvent dragEndEvent;

    private UITapAndDrag tapAndDrag;

    private void Awake()
    {
        tapAndDrag = GetComponent<UITapAndDrag>();
        if (image == null)
            image = GetComponent<Image>();
    }

    private void OnEnable()
    {
        tapAndDrag.OnTap += OnTap;
        tapAndDrag.OnDragBegin += OnDragBegin;
        tapAndDrag.OnDragEnd += OnDragEnd;
    }

    private void OnDisable()
    {
        tapAndDrag.OnTap -= OnTap;
        tapAndDrag.OnDragBegin -= OnDragBegin;
        tapAndDrag.OnDragEnd -= OnDragEnd;
    }

    private void Reset()
    {
        image = GetComponent<Image>();
    }

    private void OnTap()
    {
        if (selectedElement != null && element != null)
            selectedElement.value = element;
        
        if (tapEvent != null)
            tapEvent.Raise();
    }

    private void OnDragBegin()
    {
        image.enabled = false;

        if (selectedElement != null && element != null)
            selectedElement.value = element;

        if (dragBeginEvent != null)
            dragBeginEvent.Raise();
    }

    private void OnDragEnd()
    {
        image.enabled = true;

        if (dragEndEvent != null)
            dragEndEvent.Raise();
    }
}
