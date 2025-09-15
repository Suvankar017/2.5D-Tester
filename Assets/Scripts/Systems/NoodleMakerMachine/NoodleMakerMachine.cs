using UnityEngine;
using DG.Tweening;
using ScriptableObjectArchitecture;

public class NoodleMakerMachine : MonoBehaviour, IDroppable<DoughElement>
{
    public SpriteRenderer machineDoughRenderer;
    public SpriteRenderer plateNoodleRenderer;
    [Space]
    public DoughElementVariable droppedDough;
    [Space]
    public GameEvent doughDroppedEvent;
    public GameEvent noodleMakingCompletedEvent;

    private Tween machineDoughMoveTween;
    private Tween machineDoughFadeTween;
    private Tween plateNoodleMoveTween;
    private Tween plateNoodleFadeTween;
    private Vector3 doughDefaultPositionLS;
    private Vector3 noodleDefaultPositionLS;

    private void Start()
    {
        if (machineDoughRenderer != null)
            doughDefaultPositionLS = machineDoughRenderer.transform.localPosition;

        if (plateNoodleRenderer != null)
            noodleDefaultPositionLS = plateNoodleRenderer.transform.localPosition;
    }

    public void OnDrop(DoughElement dough)
    {
        if (dough == null)
            return;

        HandleMachineDough(dough);
        HandlePlateNoodle(dough);

        if (droppedDough != null)
            droppedDough.value = dough;

        if (doughDroppedEvent != null)
            doughDroppedEvent.Raise();
    }

    private void HandleMachineDough(DoughElement dough)
    {
        if (machineDoughRenderer == null)
            return;

        Color initialColor = dough.color;
        initialColor.a = 1.0f;
        machineDoughRenderer.color = initialColor;
        machineDoughRenderer.transform.localPosition = doughDefaultPositionLS;
        machineDoughRenderer.gameObject.SetActive(true);

        machineDoughMoveTween?.Kill();
        machineDoughFadeTween?.Kill();

        const float duration = 5.0f;

        machineDoughMoveTween = machineDoughRenderer.transform.DOLocalMoveY(2.5f, duration).SetEase(Ease.InOutSine);
        machineDoughFadeTween = machineDoughRenderer.DOFade(0.0f, duration).SetEase(Ease.InSine).OnComplete(() =>
        {
            machineDoughRenderer.gameObject.SetActive(false);
        });
    }

    private void HandlePlateNoodle(DoughElement dough)
    {
        if (plateNoodleRenderer == null)
            return;

        Color initialColor = dough.color;
        initialColor.a = 0.0f;
        plateNoodleRenderer.color = initialColor;
        plateNoodleRenderer.transform.localPosition = noodleDefaultPositionLS;
        plateNoodleRenderer.gameObject.SetActive(true);

        plateNoodleMoveTween?.Kill();
        plateNoodleFadeTween?.Kill();

        const float duration = 5.0f;

        plateNoodleMoveTween = plateNoodleRenderer.transform.DOLocalMoveY(0.625f, duration).SetEase(Ease.InOutSine).OnComplete(OnNoodleMakingCompleted);
        plateNoodleFadeTween = plateNoodleRenderer.DOFade(1.0f, duration).SetEase(Ease.OutSine);
    }

    private void OnNoodleMakingCompleted()
    {
        if (noodleMakingCompletedEvent != null)
            noodleMakingCompletedEvent.Raise();
    }
}
