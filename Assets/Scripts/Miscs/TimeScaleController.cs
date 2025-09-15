using UnityEngine;

public class TimeScaleController : MonoBehaviour
{
    [Range(0.0f, 1.0f)]
    public float timeScale = 1.0f;

    private void Update()
    {
        Time.timeScale = timeScale;
    }

    public void PauseTime() => timeScale = 0.0f;

    public void ResetTime() => timeScale = 1.0f;
}
