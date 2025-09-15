using UnityEngine;
using UnityEngine.Events;

public class LinearTutorial : TutorialBase
{
    public bool playOnStart;
    public bool disableGameObjectOnComplete;
    public bool disableComponentOnComplete;
    [Space]
    public TutorialInstructionBase instruction;
    public TutorialActionListenerBase actionListener;
    [Space]
    public UnityEvent onCompleted;

    private bool isActionListened;
    private bool isPlaying;

    private void Awake()
    {
        isActionListened = false;
        isPlaying = false;
    }

    private void Start()
    {
        if (playOnStart)
            Play();
    }

    private void Update()
    {
        if (isPlaying)
            ListenForAction();
    }

    public override void Play()
    {
        if (isPlaying)
            return;

        isPlaying = true;

        if (instruction != null)
            instruction.Play();
        else
            Debug.LogError("Instruction is not assigned!", gameObject);
    }

    public override void Replay()
    {
        isActionListened = false;
        isPlaying = true;

        if (instruction != null)
            instruction.Replay();
        else
            Debug.LogError("Instruction is not assigned!", gameObject);
    }

    public override void Stop()
    {
        isActionListened = false;
        isPlaying = false;

        if (instruction != null)
            instruction.Stop();
        else
            Debug.LogError("Instruction is not assigned!", gameObject);
    }

    private void ListenForAction()
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
        isPlaying = false;

        if (instruction != null)
            instruction.Stop();
        else
            Debug.LogError("Instruction is not assigned!", gameObject);

        if (disableGameObjectOnComplete)
            gameObject.SetActive(false);

        if (disableComponentOnComplete)
            enabled = false;

        onCompleted?.Invoke();
    }
}
