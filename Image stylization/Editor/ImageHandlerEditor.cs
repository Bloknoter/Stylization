using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEditor;


namespace Stylization
{
    [CustomEditor(typeof(ImageStyleHandler))]
    public class ImageHandlerEditor : Editor
    {
        private SerializedProperty style;

        private SerializedProperty image;

        private void OnEnable()
        {
            style = serializedObject.FindProperty("style");
            image = serializedObject.FindProperty("image");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            ImageStyle oldStyle = (ImageStyle)style.objectReferenceValue;

            EditorGUILayout.ObjectField(style, new GUIContent("Style"));

            if (style.objectReferenceValue != null)
            {
                if (oldStyle != style.objectReferenceValue)
                {
                    serializedObject.ApplyModifiedProperties();
                    ((ImageStyleHandler)target).TryUpdateComponents();
                }

                EditorGUILayout.Separator();

                EditorGUILayout.ObjectField(image, new GUIContent("Image"));
            }

            serializedObject.ApplyModifiedProperties();
        }
    }
}
