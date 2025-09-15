using UnityEngine;

public class DoughTapSelectionSystem : MonoBehaviour
{
    public DoughElementVariable selectedDough;
    public NoodleMakerMachine noodleMakerMachine;

    public void OnTap()
    {
        if (selectedDough == null || noodleMakerMachine == null)
            return;

        noodleMakerMachine.OnDrop(selectedDough.value);
    }
}
