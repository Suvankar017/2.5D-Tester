using UnityEngine;
using UnityEngine.Events;

public class InitializeOnNthFrame : MonoBehaviour
{
    public bool disableGameObjectOnComplete;
    public bool disableComponentOnComplete;
    [Space]
    public int frameNumber = 1;
    [Space]
    public UnityEvent onInitialized;

    private bool isInitialized;

    private void Awake()
    {
        isInitialized = false;
    }

    private void Update()
    {
        if (isInitialized || Time.frameCount != frameNumber)
            return;
        
        isInitialized = true;
        onInitialized?.Invoke();

        if (disableComponentOnComplete)
            enabled = false;

        if (disableGameObjectOnComplete)
            gameObject.SetActive(false);
    }
}
