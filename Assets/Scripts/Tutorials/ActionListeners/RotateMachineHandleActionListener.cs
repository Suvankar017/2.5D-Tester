public class RotateMachineHandleActionListener : TutorialActionListenerBase
{
    private bool hasRotated;

    public override bool Listen() => hasRotated;

    public void SetRotationState(bool rotated) => hasRotated = rotated;
}
