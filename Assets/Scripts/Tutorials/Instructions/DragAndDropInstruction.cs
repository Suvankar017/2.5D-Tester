using UnityEngine;
using DG.Tweening;

public class DragAndDropInstruction : TutorialInstructionBase
{
    public float duration = 1.0f;
    public Ease ease = Ease.InOutSine;
    [Space]
    public Transform handGesture;
    public Transform pointA;
    public Transform pointB;

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
        if (handGesture == null || pointA == null || pointB == null)
        {
            Debug.LogError("Hand Gesture or points are not assigned!", gameObject);
            return;
        }

        handGesture.DOKill();

        handGesture.position = pointA.position;
        handGesture.gameObject.SetActive(true);

        tweenSequence?.Kill();
        tweenSequence = DOTween.Sequence();

        tweenSequence.Append(handGesture.DOMove(pointB.position, duration).SetEase(ease))
            .AppendInterval(0.2f)
            .SetLoops(-1, LoopType.Restart);
    }
}
