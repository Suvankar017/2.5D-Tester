using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;

public class WaitForDuration : MonoBehaviour
{
    public float duration = 1.0f;
    public bool ignoreTimeScale = true;
    [Space]
    public UnityEvent onDurationComplete;

    private Tween tween;

    public void StartTimer()
    {
        if (tween != null)
            return;

        tween = DOVirtual.DelayedCall(duration, OnTimerCompleted, ignoreTimeScale);
    }

    public void RestartTimer()
    {
        tween?.Kill();
        tween = DOVirtual.DelayedCall(duration, OnTimerCompleted, ignoreTimeScale);
    }

    private void OnTimerCompleted()
    {
        tween = null;
        onDurationComplete?.Invoke();
    }
}