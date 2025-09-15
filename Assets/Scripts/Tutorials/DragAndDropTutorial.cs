using UnityEngine;
using UnityEngine.Events;

public class DragAndDropTutorial : MonoBehaviour
{
    public bool playOnStart;

    [Header("References")]
    public TutorialInstructionBase instruction;
    public TutorialActionListenerBase actionListener;
    [Space]
    public UnityEvent onComplete;

    private bool isActionListened;

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
        if (instruction == null)
        {
            Debug.LogError("Instruction is not assigned!", gameObject);
            return;
        }

        instruction.Play();
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
        if (instruction != null)
            instruction.Stop();

        onComplete?.Invoke();
    }
}
