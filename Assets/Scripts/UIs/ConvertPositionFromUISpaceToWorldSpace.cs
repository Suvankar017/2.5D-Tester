using UnityEngine;

public class ConvertPositionFromUISpaceToWorldSpace : MonoBehaviour
{
    public RectTransform source;
    public Transform point;

    public void Calculate()
    {
        if (source == null || point == null)
            return;

        Vector3 pos = source.position;
        point.position = new(pos.x, pos.y, point.position.z);
    }
}
