using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


namespace Stylization
{
    [CustomEditor(typeof(TextStyleHandler))]
    public class TextHandlerEditor : Editor
    {
        private SerializedProperty style;

        private SerializedProperty text;

        private SerializedProperty legacyText;

        private void OnEnable()
        {
            style = serializedObject.FindProperty("style");
            text = serializedObject.FindProperty("text");
            legacyText = serializedObject.FindProperty("legacyText");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            TextStyle oldStyle = (TextStyle)style.objectReferenceValue;

            EditorGUILayout.ObjectField(style, new GUIContent("Style"));

            TextStyle componentStyle = (TextStyle)style.objectReferenceValue;

            if (style.objectReferenceValue != null)
            {
                if(oldStyle != style.objectReferenceValue)
                {
                    serializedObject.ApplyModifiedProperties();
                    ((TextStyleHandler)target).TryUpdateComponents();
                }

                EditorGUILayout.Separator();

                if (componentStyle.TextType == TextStyle.TextTypes.TextMeshPro)
                    EditorGUILayout.ObjectField(text, new GUIContent("Text"));
                else if (componentStyle.TextType == TextStyle.TextTypes.Legacy)
                    EditorGUILayout.ObjectField(legacyText, new GUIContent("Text"));
                else if(componentStyle.TextType == TextStyle.TextTypes.Mixed)
                {
                    EditorGUILayout.Separator();
                    EditorGUILayout.LabelField("Assign one of this fields:");
                    EditorGUILayout.ObjectField(text, new GUIContent("TextMeshPro"));
                    EditorGUILayout.LabelField("or");
                    EditorGUILayout.ObjectField(legacyText, new GUIContent("Legacy Text"));
                }
            }

            serializedObject.ApplyModifiedProperties();
        }
    }
}
