using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;

public class ScrollUITutorial : MonoBehaviour
{
    public bool playOnStart;

    [Header("References")]
    public Transform hand;
    public Transform pointA;
    public Transform pointB;
    public TutorialActionListenerBase actionListener;
    [Space]
    public UnityEvent onComplete;

    private bool isActionListened;
    private bool isCompleted;
    private Sequence tweenSequence;

    private void Awake()
    {
        isActionListened = false;
        isCompleted = false;
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
        isCompleted = false;
        GiveInstruction();
    }

    private void GiveInstruction()
    {
        if (hand == null || pointA == null || pointB == null)
        {
            Debug.LogError("Hand or points are not assigned!", gameObject);
            return;
        }

        hand.position = pointA.position;
        hand.gameObject.SetActive(true);

        tweenSequence?.Kill();
        tweenSequence = DOTween.Sequence();

        tweenSequence.Append(hand.DOMove(pointB.position, 1.5f).SetEase(Ease.OutBack))
            .AppendInterval(0.2f)
            .Append(hand.DOMove(pointA.position, 1.5f).SetEase(Ease.OutBack))
            .AppendInterval(0.2f)
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
        if (isCompleted)
            return;

        isCompleted = true;
        hand.gameObject.SetActive(false);
        tweenSequence?.Kill();
        tweenSequence = null;
        onComplete?.Invoke();
    }
}
