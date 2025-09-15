using UnityEngine;

namespace ScriptableObjectArchitecture
{
    [CreateAssetMenu(fileName = "NewStringVariable", menuName = "Variable/String")]
    public class StringVariable : Variable<string> { }
}
