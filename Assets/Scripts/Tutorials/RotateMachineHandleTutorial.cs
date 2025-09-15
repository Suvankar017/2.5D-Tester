using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;

public class RotateMachineHandleTutorial : MonoBehaviour
{
    public bool playOnStart;

    [Header("References")]
    public Transform rotateGesture;
    public TutorialActionListenerBase actionListener;
    [Space]
    public UnityEvent onComplete;

    private bool isActionListened;
    private Sequence tweenSequence;

    private void Awake()
    {
        isActionListened = false;
    }

    private void Start()
    {
        if (playOnStart)
            GiveInstruction();
    }

    private void Update()
    {
        WaitForPlayerAction();
    }

    public void Play() => GiveInstruction();

    public void Replay()
    {
        isActionListened = false;
        GiveInstruction();
    }

    private void GiveInstruction()
    {
        if (rotateGesture == null)
        {
            Debug.LogError("Rotate Gesture is not assigned!", gameObject);
            return;
        }

        rotateGesture.gameObject.SetActive(true);

        tweenSequence?.Kill();
        tweenSequence = DOTween.Sequence();

        tweenSequence.Append(rotateGesture.DORotate(new Vector3(0, 0, -360.0f), 2.0f, RotateMode.FastBeyond360)
            .SetEase(Ease.Linear))
            .SetLoops(-1, LoopType.Restart);
    }

    private void WaitForPlayerAction()
    {
        if (isActionListened)
            return;

        if (actionListener == null)
        {
            Debug.LogError("Action Listener is not assigned!", gameObject);
            return;
        }

        if (!actionListener.Listen())
            return;

        isActionListened = true;
        OnComplete();
    }

    private void OnComplete()
    {
        rotateGesture.gameObject.SetActive(false);
        tweenSequence?.Kill();
        tweenSequence = null;
        onComplete?.Invoke();
    }
}
