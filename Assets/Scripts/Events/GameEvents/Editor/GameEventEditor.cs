using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace ScriptableObjectArchitecture
{
    [CustomEditor(typeof(GameEvent), true)]
    public class GameEventEditor : Editor
    {
        private readonly GUIStyle headerStyle = new();
        private readonly GUIStyle normalLinkButtonStyle = new();
        private readonly GUIStyle hoverLinkButtonStyle = new();
        private readonly GUIContent listenerContent = new();

        private void OnEnable()
        {
            EditorApplication.update += Repaint;

            InitGUIStyles();
        }

        private void OnDisable()
        {
            EditorApplication.update -= Repaint;
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            ShowRaiseButton();
            ShowListenersInfo();
        }

        private void InitGUIStyles()
        {
            headerStyle.fontSize = 16;
            headerStyle.fontStyle = FontStyle.Bold;
            headerStyle.normal.textColor = Color.white;

            normalLinkButtonStyle.fontSize = 13;
            normalLinkButtonStyle.fontStyle = FontStyle.Bold;
            //normalLinkButtonStyle.normal.textColor = new(0.769f, 0.769f, 0.769f, 1.0f);
            normalLinkButtonStyle.normal.textColor = new(0.506f, 0.706f, 1.0f, 1.0f);
            normalLinkButtonStyle.margin = new(0, 0, 2, 2);
            normalLinkButtonStyle.richText = true;

            hoverLinkButtonStyle.fontSize = normalLinkButtonStyle.fontSize;
            hoverLinkButtonStyle.fontStyle = normalLinkButtonStyle.fontStyle;
            hoverLinkButtonStyle.normal.textColor = Color.yellow;
            hoverLinkButtonStyle.margin = normalLinkButtonStyle.margin;
            hoverLinkButtonStyle.richText = normalLinkButtonStyle.richText;
        }

        private bool TryGetListeners(GameEvent gameEvent, out List<GameEventListener> listeners)
        {
            listeners = null;

            FieldInfo listenerFieldInfo = typeof(GameEvent).GetField("listeners", BindingFlags.NonPublic | BindingFlags.Instance);
            if (listenerFieldInfo == null)
                return false;

            listeners = listenerFieldInfo.GetValue(gameEvent) as List<GameEventListener>;
            return true;
        }

        private void ShowRaiseButton()
        {
            GUILayout.Space(5.0f);

            if (GUILayout.Button("Raise", GUILayout.Height(30.0f)))
            {
                GameEvent gameEvent = target as GameEvent;
                gameEvent.Raise();
            }
        }

        private void ShowListenersInfo()
        {
            int listenerCount = 0;
            if (TryGetListeners(target as GameEvent, out List<GameEventListener> listeners))
                listenerCount = listeners.Count;

            GUILayout.Space(5.0f);
            GUILayout.Label($"Listener: {listenerCount}", headerStyle);
            GUILayout.Space(3.0f);

            for (int i = 0; i < listenerCount; i++)
            {
                GameEventListener listener = listeners[i];

                if (DrawClickableLabel(listener.name))
                    EditorGUIUtility.PingObject(listener);

                if (i < listenerCount - 1)
                    GUILayout.Space(2.0f);
            }
        }

        private bool DrawClickableLabel(string text)
        {
            listenerContent.text = text;

            Rect rect = GUILayoutUtility.GetRect(listenerContent, normalLinkButtonStyle);
            Vector2 tightSize = normalLinkButtonStyle.CalcSize(listenerContent);
            Rect tightRect = new(rect) { width = tightSize.x };
            Event currentEvent = Event.current;

            bool isHovering = tightRect.Contains(currentEvent.mousePosition);
            if (isHovering)
                text = $"<u>{text}</u>";

            GUI.Label(tightRect, text, isHovering ? hoverLinkButtonStyle : normalLinkButtonStyle);

            if (currentEvent.type == EventType.Repaint || currentEvent.type == EventType.Layout)
                EditorGUIUtility.AddCursorRect(tightRect, MouseCursor.Link);

            return isHovering && currentEvent.type == EventType.MouseDown;
        }
    }
}
