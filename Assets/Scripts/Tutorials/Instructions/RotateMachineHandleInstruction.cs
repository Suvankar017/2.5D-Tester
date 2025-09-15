using UnityEngine;
using DG.Tweening;

public class RotateMachineHandleInstruction : TutorialInstructionBase
{
    public float duration = 1.0f;
    public Transform rotateGesture;

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
        if (rotateGesture != null)
            rotateGesture.gameObject.SetActive(false);

        tweenSequence?.Kill();
        tweenSequence = null;
    }

    private void PlayInstruction()
    {
        if (rotateGesture == null)
        {
            Debug.LogError("Rotate Gesture is not assigned!", gameObject);
            return;
        }

        rotateGesture.DOKill();
        rotateGesture.gameObject.SetActive(true);

        tweenSequence?.Kill();
        tweenSequence = DOTween.Sequence();

        tweenSequence.Append(rotateGesture.DORotate(new Vector3(0, 0, -360.0f), duration, RotateMode.FastBeyond360)
            .SetEase(Ease.Linear))
            .SetLoops(-1, LoopType.Restart);
    }
}
