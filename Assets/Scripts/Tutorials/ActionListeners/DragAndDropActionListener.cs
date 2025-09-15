public class DragAndDropActionListener : TutorialActionListenerBase
{
    private bool hasDragAndDropped;

    public override bool Listen() => hasDragAndDropped;

    public void SetDragAndDropState(bool dragAndDropped) => hasDragAndDropped = dragAndDropped;
}
