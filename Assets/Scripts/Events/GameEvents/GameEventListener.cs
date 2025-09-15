using UnityEngine;
using UnityEngine.Events;

namespace ScriptableObjectArchitecture
{
    public class GameEventListener : MonoBehaviour
    {
        [SerializeField]
        private GameEvent @event;
        [Space]
        public UnityEvent response;

        private void OnEnable()
        {
            if (@event != null)
                @event.AddListener(this);
        }

        private void OnDisable()
        {
            if (@event != null)
                @event.RemoveListener(this);
        }

        public void OnEventRaised() => response?.Invoke();
    }
}