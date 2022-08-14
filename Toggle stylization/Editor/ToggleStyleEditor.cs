using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEditor;


namespace Stylization
{
    [CustomEditor(typeof(ToggleStyle))]
    public class ToggleStyleEditor : StyleEditor<ToggleStyle, ToggleStyleHandler>
    {
        private SerializedProperty bgSprite;

        private SerializedProperty bgMaterial;

        private SerializedProperty bgColor;

        private SerializedProperty bgMaskable;


        private SerializedProperty checkmarkSprite;

        private SerializedProperty checkmarkMaterial;

        private SerializedProperty checkmarkColor;

        private SerializedProperty checkmarkMaskable;


        private SerializedProperty hasText;

        private SerializedProperty textType;

        private SerializedProperty textfontsize;

        private SerializedProperty foreground;

        private SerializedProperty textFont;

        private SerializedProperty legacyTextFont;


        protected override void Init()
        {
            bgSprite = serializedObject.FindProperty("bgSprite");
            bgMaterial = serializedObject.FindProperty("bgMaterial");
            bgColor = serializedObject.FindProperty("bgColor");
            bgMaskable = serializedObject.FindProperty("bgMaskable");

            checkmarkSprite = serializedObject.FindProperty("checkmarkSprite");
            checkmarkMaterial = serializedObject.FindProperty("checkmarkMaterial");
            checkmarkColor = serializedObject.FindProperty("checkmarkColor");
            checkmarkMaskable = serializedObject.FindProperty("checkmarkMaskable");

            hasText = serializedObject.FindProperty("hasText");
            textType = serializedObject.FindProperty("textType");
            textfontsize = serializedObject.FindProperty("textfontsize");
            foreground = serializedObject.FindProperty("foreground");
            textFont = serializedObject.FindProperty("textFont");
            legacyTextFont = serializedObject.FindProperty("legacyTextFont");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            ToggleStyle buttonStyle = (ToggleStyle)target;

            DrawImageParameters("Background", bgSprite, bgMaterial, bgColor, bgMaskable);

            EditorGUILayout.Separator();

            DrawImageParameters("Checkmark", checkmarkSprite, checkmarkMaterial, checkmarkColor, checkmarkMaskable);

            EditorGUILayout.Separator();

            EditorGUILayout.LabelField("Text", EditorStyles.boldLabel);
            EditorGUILayout.Separator();
            EditorGUI.indentLevel++;

            EditorGUILayout.PropertyField(hasText, new GUIContent("Has text"));

            if (hasText.boolValue)
            {
                EditorGUILayout.PropertyField(textType, new GUIContent("Text type"));

                if (buttonStyle.TextType == ToggleStyle.TextTypes.TextMeshPro)
                {
                    EditorGUILayout.PropertyField(textFont, new GUIContent("Font"));
                    EditorGUILayout.PropertyField(textfontsize, new GUIContent("Font size"));
                    if (textfontsize.floatValue < 0.01f)
                        textfontsize.floatValue = 0.01f;
                }
                else if (buttonStyle.TextType == ToggleStyle.TextTypes.Legacy)
                {
                    EditorGUILayout.PropertyField(legacyTextFont, new GUIContent("Font"));
                    textfontsize.floatValue = EditorGUILayout.IntField("Font size", (int)textfontsize.floatValue);
                    if (textfontsize.floatValue < 1f)
                        textfontsize.floatValue = 1f;
                    
                }
                else if(buttonStyle.TextType == ToggleStyle.TextTypes.Mixed)
                {
                    textfontsize.floatValue = EditorGUILayout.IntField("Font size", (int)textfontsize.floatValue);
                    if (textfontsize.floatValue < 1f)
                        textfontsize.floatValue = 1f;
                }
                EditorGUILayout.PropertyField(foreground, new GUIContent("Foreground"));
            }
            EditorGUI.indentLevel--;

            EditorGUILayout.Separator();



            EditorGUILayout.Separator();
            EditorGUILayout.Separator();

            DrawApplyButton();

            EditorGUILayout.Separator();

            DrawApplyResult();

            serializedObject.ApplyModifiedProperties();
        }

        protected override ProcessResult CanApply()
        {
            ToggleStyle thisStyle = (ToggleStyle)target;
            if (!thisStyle.HasText)
                return new ProcessResult(ProcessResult.ResultStatus.Success);

            if (thisStyle.TextType == ToggleStyle.TextTypes.TextMeshPro)
            {
                if (thisStyle.TextFont != null)
                    return new ProcessResult(ProcessResult.ResultStatus.Success);
                else
                    return new ProcessResult(ProcessResult.ResultStatus.Error, "Text font field is not assigned");
            }
            else if (thisStyle.TextType == ToggleStyle.TextTypes.Legacy)
            {
                if (thisStyle.LegacyTextFont != null)
                    return new ProcessResult(ProcessResult.ResultStatus.Success);
                else
                    return new ProcessResult(ProcessResult.ResultStatus.Error, "Text font field is not assigned");
            }
            else if (thisStyle.TextType == ToggleStyle.TextTypes.Mixed)
                return new ProcessResult(ProcessResult.ResultStatus.Success);

            return new ProcessResult(ProcessResult.ResultStatus.Error, "Unknown error. Send message to the developer"); ;
        }

        protected override void SetDirtyElements(ToggleStyleHandler handler)
        {
            if (handler.BG != null)
                EditorUtility.SetDirty(handler.BG);
            if (handler.Checkmark != null)
                EditorUtility.SetDirty(handler.Checkmark);
            if (handler.Text != null)
                EditorUtility.SetDirty(handler.Text);
            if (handler.LegacyText != null)
                EditorUtility.SetDirty(handler.LegacyText);
        }
    }
}
