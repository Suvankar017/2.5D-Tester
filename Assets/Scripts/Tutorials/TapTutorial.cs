using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;

public class TapTutorial : MonoBehaviour
{
    public bool playOnStart;

    [Header("References")]
    public Transform handGesture;
    public Transform tapPoint;
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

    private void Update() => WaitForPlayerAction();

    public void Play() => GiveInstruction();

    public void Replay()
    {
        isActionListened = false;
        GiveInstruction();
    }

    private void GiveInstruction()
    {
        if (handGesture == null || tapPoint == null)
        {
            Debug.LogError("Hand Gesture or Tap Point are not assigned!", gameObject);
            return;
        }

        handGesture.position = tapPoint.position;
        handGesture.localScale = Vector3.one;
        handGesture.gameObject.SetActive(true);

        tweenSequence?.Kill();
        tweenSequence = DOTween.Sequence();

        tweenSequence.Append(handGesture.DOScale(0.6f, 0.5f).SetEase(Ease.InCubic))
            .SetLoops(-1, LoopType.Restart)
            .SetUpdate(true);
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
        handGesture.gameObject.SetActive(false);
        handGesture.localScale = Vector3.one;
        tweenSequence?.Kill();
        tweenSequence = null;
        onComplete?.Invoke();
    }
}