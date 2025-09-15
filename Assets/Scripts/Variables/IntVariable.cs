using UnityEngine;

namespace ScriptableObjectArchitecture
{
    [CreateAssetMenu(fileName = "NewIntVariable", menuName = "Variable/Int")]
    public class IntVariable : Variable<int> { }
}
