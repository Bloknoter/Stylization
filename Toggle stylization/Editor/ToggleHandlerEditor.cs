using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEditor;


namespace Stylization
{
    [CustomEditor(typeof(ToggleStyleHandler))]
    public class ToggleHandlerEditor : Editor
    {
        private SerializedProperty style;

        private SerializedProperty bg;

        private SerializedProperty checkmark;

        private SerializedProperty text;

        private SerializedProperty legacyText;

        private void OnEnable()
        {
            style = serializedObject.FindProperty("style");
            bg = serializedObject.FindProperty("bg");
            checkmark = serializedObject.FindProperty("checkmark");
            text = serializedObject.FindProperty("text");
            legacyText = serializedObject.FindProperty("legacyText");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            ToggleStyle oldStyle = (ToggleStyle)style.objectReferenceValue;

            EditorGUILayout.ObjectField(style, new GUIContent("Style"));

            ToggleStyle componentStyle = (ToggleStyle)style.objectReferenceValue;

            if (style.objectReferenceValue != null)
            {
                if (oldStyle != style.objectReferenceValue)
                {
                    serializedObject.ApplyModifiedProperties();
                    ((ToggleStyleHandler)target).TryUpdateComponents();
                }

                EditorGUILayout.Separator();

                EditorGUILayout.ObjectField(bg, new GUIContent("Background"));
                EditorGUILayout.ObjectField(checkmark, new GUIContent("Checkmark"));
                if (componentStyle.HasText)
                {
                    if (componentStyle.TextType == TextTypes.TextMeshPro)
                        EditorGUILayout.ObjectField(text, new GUIContent("Text"));
                    else if (componentStyle.TextType == TextTypes.Legacy)
                        EditorGUILayout.ObjectField(legacyText, new GUIContent("Text"));
                    else if(componentStyle.TextType == TextTypes.Mixed)
                    {
                        EditorGUILayout.Separator();
                        EditorGUILayout.LabelField("Assign one of this fields:");
                        EditorGUILayout.ObjectField(text, new GUIContent("TextMeshPro"));
                        EditorGUILayout.LabelField("or");
                        EditorGUILayout.ObjectField(legacyText, new GUIContent("Legacy Text"));
                    }
                }
            }

            serializedObject.ApplyModifiedProperties();
        }
    }
}
