using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

using UnityEditor;


namespace Stylization
{
    [CustomEditor(typeof(ButtonStyleHandler))]
    public class ButtonStyleHadlerEditor : Editor
    {
        private SerializedProperty style;

        private SerializedProperty buttonBG;

        private SerializedProperty buttonText;

        private SerializedProperty legacyButtonText;

        private void OnEnable()
        {
            style = serializedObject.FindProperty("style");
            buttonBG = serializedObject.FindProperty("buttonBG");
            buttonText = serializedObject.FindProperty("buttonText");
            legacyButtonText = serializedObject.FindProperty("legacyButtonText");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            ButtonStyle oldStyle = (ButtonStyle)style.objectReferenceValue;

            EditorGUILayout.ObjectField(style, new GUIContent("Style"));

            ButtonStyle buttonStyle = (ButtonStyle)style.objectReferenceValue;

            if (style.objectReferenceValue != null)
            {
                if (oldStyle != style.objectReferenceValue)
                {
                    serializedObject.ApplyModifiedProperties();
                    ((ButtonStyleHandler)target).TryUpdateComponents();
                }
                EditorGUILayout.Separator();

                EditorGUILayout.ObjectField(buttonBG, new GUIContent("Background"));
                if (buttonStyle.HasText)
                {
                    if (buttonStyle.TextType == TextTypes.TextMeshPro)
                        EditorGUILayout.ObjectField(buttonText, new GUIContent("Text"));
                    else if(buttonStyle.TextType == TextTypes.Legacy)
                        EditorGUILayout.ObjectField(legacyButtonText, new GUIContent("Text"));
                    else if (buttonStyle.TextType == TextTypes.Mixed)
                    {
                        EditorGUILayout.Separator();
                        EditorGUILayout.LabelField("Assign one of this fields:");
                        EditorGUILayout.ObjectField(buttonText, new GUIContent("TextMeshPro"));
                        EditorGUILayout.LabelField("or");
                        EditorGUILayout.ObjectField(legacyButtonText, new GUIContent("Legacy Text"));
                    }
                }
            }

            serializedObject.ApplyModifiedProperties();
        }
    }
}