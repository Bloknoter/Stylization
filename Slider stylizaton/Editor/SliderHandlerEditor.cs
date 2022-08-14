using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEditor;


namespace Stylization
{
    [CustomEditor(typeof(SliderStyleHandler))]
    public class SliderHandlerEditor : Editor
    {
        private SerializedProperty style;

        private SerializedProperty bg;

        private SerializedProperty fill;

        private SerializedProperty handle;

        private void OnEnable()
        {
            style = serializedObject.FindProperty("style");
            bg = serializedObject.FindProperty("bg");
            fill = serializedObject.FindProperty("fill");
            handle = serializedObject.FindProperty("handle");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            SliderStyle oldStyle = (SliderStyle)style.objectReferenceValue;

            EditorGUILayout.ObjectField(style, new GUIContent("Style"));

            SliderStyle sliderStyle = (SliderStyle)style.objectReferenceValue;

            if (style.objectReferenceValue != null)
            {
                if (oldStyle != style.objectReferenceValue)
                {
                    serializedObject.ApplyModifiedProperties();
                    ((SliderStyleHandler)target).TryUpdateComponents();
                }

                EditorGUILayout.Separator();

                EditorGUILayout.ObjectField(bg, new GUIContent("Background"));
                EditorGUILayout.ObjectField(fill, new GUIContent("Fill"));
                if (sliderStyle.HasHandle)
                    EditorGUILayout.ObjectField(handle, new GUIContent("Handle"));
            }

            serializedObject.ApplyModifiedProperties();
        }
    }
}
