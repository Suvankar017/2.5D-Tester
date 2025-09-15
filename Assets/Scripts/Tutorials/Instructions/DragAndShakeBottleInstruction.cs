using UnityEngine;
using DG.Tweening;

public class DragAndShakeBottleInstruction : TutorialInstructionBase
{
    public Transform handGesture;
    public Transform pointA;
    public Transform pointB;
    public Transform pointC;
    public Transform pointD;

    private Sequence tweenSequence;

    public override void Play()
    {
        if (tweenSequence != null)
            return;

        PlayInstruction();
    }

    public override void Replay()
    {
        PlayInstruction();
    }

    public override void Stop()
    {
        if (handGesture != null)
            handGesture.gameObject.SetActive(false);

        tweenSequence?.Kill();
        tweenSequence = null;
    }

    private void PlayInstruction()
    {
        if (handGesture == null || pointA == null || pointB == null || pointC == null || pointD == null)
        {
            Debug.LogError("Hand Gesture or points are not assigned!", gameObject);
            return;
        }

        handGesture.position = pointA.position;
        handGesture.gameObject.SetActive(true);

        tweenSequence?.Kill();
        tweenSequence = DOTween.Sequence();

        tweenSequence.Append(handGesture.DOMove(pointB.position, 1.0f).SetEase(Ease.InOutSine))
            .Append(handGesture.DOMove(pointC.position, 0.0625f).SetEase(Ease.Linear))
            .Append(handGesture.DOMove(pointD.position, 0.125f).SetEase(Ease.Linear).SetLoops(5, LoopType.Yoyo))
            .AppendInterval(0.2f)
            .SetLoops(-1, LoopType.Restart);
    }
}
