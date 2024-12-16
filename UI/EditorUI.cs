using System;
using UnityEditor;
using UnityEngine;

#if UNITY_EDITOR

namespace HeavyCavStudios.Core.UI
{
    public static class EditorUI
    {
        public static void VerticalBoxHeader(int fontSize, string title, GUIStyle style, Action internalDrawCall = null)
        {
            var headerStyle = new GUIStyle(style)
            {
                fontSize = fontSize,
                fontStyle = FontStyle.Bold,
                margin = new RectOffset(10, 10, 10, 10) // Add margin if needed
            };

            EditorGUILayout.BeginVertical("box");
            EditorGUILayout.LabelField(title, headerStyle);
            internalDrawCall?.Invoke();
            EditorGUILayout.EndVertical();
        }

        public static string TextArea(string label, string text, GUILayoutOption layoutOption, GUIStyle labelStyle)
        {
            GUILayout.Label(label, labelStyle);
            return EditorGUILayout.TextField(text, layoutOption);
        }
    }
}
#endif