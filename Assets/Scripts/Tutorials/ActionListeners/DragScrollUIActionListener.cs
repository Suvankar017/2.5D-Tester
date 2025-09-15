public class DragScrollUIActionListener : TutorialActionListenerBase
{
    private bool hasDragged;

    public override bool Listen() => hasDragged;

    public void SetDragState(bool dragged) => hasDragged = dragged;
}
