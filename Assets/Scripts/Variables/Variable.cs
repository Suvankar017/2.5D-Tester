using UnityEngine;

namespace ScriptableObjectArchitecture
{
    public abstract class Variable<T> : ScriptableObject
    {
        public T value;
    }
}
