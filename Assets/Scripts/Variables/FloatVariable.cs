using UnityEngine;

namespace ScriptableObjectArchitecture
{
    [CreateAssetMenu(fileName = "NewFloatVariable", menuName = "Variable/Float")]
    public class FloatVariable : Variable<float> { }
}
