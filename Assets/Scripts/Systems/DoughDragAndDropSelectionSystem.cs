using UnityEngine;

public class DoughDragAndDropSelectionSystem : MonoBehaviour
{
    public SpriteRenderer ghostDough;
    public DoughElementVariable selectedDough;

    private Camera worldCamera;
    private bool isDragging;

    private void Awake()
    {
        isDragging = false;
    }

    private void Start()
    {
        worldCamera = Camera.main;
        if (worldCamera == null)
            worldCamera = Camera.current;
    }

    private void Update()
    {
        if (!isDragging)
            return;

        Vector3 mousePositionWS = worldCamera.ScreenToWorldPoint(Input.mousePosition);
        
        if (ghostDough != null)
        {
            Vector3 ghostPosition = ghostDough.transform.position;
            ghostDough.transform.position = new(mousePositionWS.x, mousePositionWS.y, ghostPosition.z);
        }
    }

    public void OnDragBegin()
    {
        isDragging = true;

        if (ghostDough != null)
        {
            if (selectedDough != null)
                ghostDough.color = selectedDough.value.color;

            ghostDough.gameObject.SetActive(true);
        }
    }

    public void OnDragEnd()
    {
        if (isDragging)
            HandleDroping();

        isDragging = false;

        if (ghostDough != null)
            ghostDough.gameObject.SetActive(false);
    }

    private void HandleDroping()
    {
        if (selectedDough == null)
            return;

        Vector2 bottomLeftPoint = ghostDough.transform.position - new Vector3(0.5f, 0.5f);
        Vector2 topRightPoint = ghostDough.transform.position + new Vector3(0.5f, 0.5f);
        Collider2D collider = Physics2D.OverlapArea(bottomLeftPoint, topRightPoint);

        if (collider == null || !collider.TryGetComponent(out IDroppable<DoughElement> droppableDoughElement))
            return;

        droppableDoughElement.OnDrop(selectedDough.value);
    }
}
