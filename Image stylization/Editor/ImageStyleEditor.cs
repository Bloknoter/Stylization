using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEditor;


namespace Stylization
{
    [CustomEditor(typeof(ImageStyle))]
    public class ImageStyleEditor : StyleEditor<ImageStyle, ImageStyleHandler>
    {
        private SerializedProperty sprite;

        private SerializedProperty material;

        private SerializedProperty color;

        private SerializedProperty maskable;

        protected override void Init()
        {
            sprite = serializedObject.FindProperty("sprite");
            material = serializedObject.FindProperty("material");
            color = serializedObject.FindProperty("color");
            maskable = serializedObject.FindProperty("maskable");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            DrawImageParameters(sprite, material, color, maskable);

            EditorGUILayout.Separator();

            DrawApplyButton();

            EditorGUILayout.Separator();

            DrawApplyResult();

            serializedObject.ApplyModifiedProperties();
        }

        protected override ProcessResult CanApply()
        {
            return new ProcessResult(ProcessResult.ResultStatus.Success);
        }

        protected override void SetDirtyElements(ImageStyleHandler handler)
        {
            if (handler.Image != null)
                EditorUtility.SetDirty(handler);
        }
    }
}
