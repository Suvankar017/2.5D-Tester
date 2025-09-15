public class TriggerActionListener : TutorialActionListenerBase
{
    private bool hasTriggered;

    public override bool Listen() => hasTriggered;

    public void SetState(bool isTrigger) => hasTriggered = isTrigger;
}
