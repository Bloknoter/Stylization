using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEditor;


namespace Stylization
{
    [CustomEditor(typeof(SliderStyle))]
    public class SliderStyleEditor : StyleEditor<SliderStyle, SliderStyleHandler>
    {
        private SerializedProperty bgSprite;

        private SerializedProperty bgMaterial;

        private SerializedProperty bgColor;

        private SerializedProperty bgMaskable;


        private SerializedProperty fillSprite;

        private SerializedProperty fillMaterial;

        private SerializedProperty fillColor;

        private SerializedProperty fillMaskable;


        private SerializedProperty hasHandle;

        private SerializedProperty handleSprite;

        private SerializedProperty handleMaterial;

        private SerializedProperty handleColor;

        private SerializedProperty handleMaskable;

        protected override void Init()
        {
            bgSprite = serializedObject.FindProperty("bgSprite");
            bgMaterial = serializedObject.FindProperty("bgMaterial");
            bgColor = serializedObject.FindProperty("bgColor");
            bgMaskable = serializedObject.FindProperty("bgMaskable");

            fillSprite = serializedObject.FindProperty("fillSprite");
            fillMaterial = serializedObject.FindProperty("fillMaterial");
            fillColor = serializedObject.FindProperty("fillColor");
            fillMaskable = serializedObject.FindProperty("fillMaskable");

            hasHandle = serializedObject.FindProperty("hasHandle");
            handleSprite = serializedObject.FindProperty("handleSprite");
            handleMaterial = serializedObject.FindProperty("handleMaterial");
            handleColor = serializedObject.FindProperty("handleColor");
            handleMaskable = serializedObject.FindProperty("handleMaskable");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            DrawImageParameters("Background", bgSprite, bgMaterial, bgColor, bgMaskable);

            EditorGUILayout.Separator();

            DrawImageParameters("Fill", fillSprite, fillMaterial, fillColor, fillMaskable);

            EditorGUILayout.Separator();

            EditorGUILayout.LabelField("Handle", EditorStyles.boldLabel);

            EditorGUILayout.Separator();
            EditorGUI.indentLevel++;

            EditorGUILayout.PropertyField(hasHandle, new GUIContent("Has handle"));

            if (hasHandle.boolValue)
            {
                EditorGUILayout.Separator();

                DrawImageParameters(handleSprite, handleMaterial, handleColor, handleMaskable);
            }

            EditorGUI.indentLevel--;

            EditorGUILayout.Separator();
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

        protected override void SetDirtyElements(SliderStyleHandler handler)
        {
            if (handler.BG != null)
                EditorUtility.SetDirty(handler.BG);
            if (handler.Fill != null)
                EditorUtility.SetDirty(handler.Fill);
            if (handler.Handle != null)
                EditorUtility.SetDirty(handler.Handle);
        }
    }
}
