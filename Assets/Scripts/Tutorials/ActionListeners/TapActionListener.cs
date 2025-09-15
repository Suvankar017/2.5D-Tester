public class TapActionListener : TutorialActionListenerBase
{
    private bool hasTapped;

    public override bool Listen() => hasTapped;

    public void SetTapState(bool tapped) => hasTapped = tapped;
}