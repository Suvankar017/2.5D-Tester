using UnityEngine;

namespace ScriptableObjectArchitecture
{
    [CreateAssetMenu(fileName = "NewColorVariable", menuName = "Variable/Color")]
    public class ColorVariable : Variable<Color> { }
}
