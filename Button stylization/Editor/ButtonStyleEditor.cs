using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEditor;


namespace Stylization
{
    [CustomEditor(typeof(ButtonStyle))]
    public class ButtonStyleEditor : StyleEditor<ButtonStyle, ButtonStyleHandler>
    {

        private SerializedProperty bgSprite;

        private SerializedProperty bgMaterial;

        private SerializedProperty bgColor;

        private SerializedProperty maskable;


        private SerializedProperty hasText;

        private SerializedProperty textType;

        private SerializedProperty textfontsize;

        private SerializedProperty foreground;

        private SerializedProperty textFont;

        private SerializedProperty legacyTextFont;

        private GUIContent HasTextLabel;


        protected override void Init()
        {
            bgSprite = serializedObject.FindProperty("bgSprite");
            bgMaterial = serializedObject.FindProperty("bgMaterial");
            bgColor = serializedObject.FindProperty("bgColor");
            maskable = serializedObject.FindProperty("maskable");

            hasText = serializedObject.FindProperty("hasText");
            textType = serializedObject.FindProperty("textType");
            textfontsize = serializedObject.FindProperty("textfontsize");
            foreground = serializedObject.FindProperty("foreground");
            textFont = serializedObject.FindProperty("textFont");
            legacyTextFont = serializedObject.FindProperty("legacyTextFont");

            HasTextLabel = new GUIContent("Has text");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            ButtonStyle buttonStyle = (ButtonStyle)target;

            DrawImageParameters("Background", bgSprite, bgMaterial, bgColor, maskable);

            EditorGUILayout.Separator();

            EditorGUILayout.LabelField("Text", EditorStyles.boldLabel);
            EditorGUILayout.Separator();
            EditorGUI.indentLevel++;

            EditorGUILayout.PropertyField(hasText, HasTextLabel);

            if (hasText.boolValue)
            {
                EditorGUILayout.PropertyField(textType, new GUIContent("Text type"));

                if (buttonStyle.TextType == ButtonStyle.TextTypes.TextMeshPro)
                {
                    EditorGUILayout.PropertyField(textFont, new GUIContent("Font"));
                    EditorGUILayout.PropertyField(textfontsize, new GUIContent("Font size"));
                    if (textfontsize.floatValue < 0.01f)
                        textfontsize.floatValue = 0.01f;
                }
                else if(buttonStyle.TextType == ButtonStyle.TextTypes.Legacy)
                {
                    EditorGUILayout.PropertyField(legacyTextFont, new GUIContent("Font"));
                    textfontsize.floatValue = EditorGUILayout.IntField("Font size", (int)textfontsize.floatValue);
                    if (textfontsize.floatValue < 1f)
                        textfontsize.floatValue = 1f;
                }
                else if(buttonStyle.TextType == ButtonStyle.TextTypes.Mixed)
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

            DrawApplyButton();

            EditorGUILayout.Separator();

            DrawApplyResult();

            serializedObject.ApplyModifiedProperties();
        }

        protected override ProcessResult CanApply()
        {
            ButtonStyle buttonStyle = (ButtonStyle)target;
            if (!buttonStyle.HasText)
                return new ProcessResult(ProcessResult.ResultStatus.Success);

            if (buttonStyle.TextType == ButtonStyle.TextTypes.TextMeshPro)
            {
                if (buttonStyle.TextFont != null)
                    return new ProcessResult(ProcessResult.ResultStatus.Success);
                else
                    return new ProcessResult(ProcessResult.ResultStatus.Error, "Text font field is not assigned");
            }
            else if (buttonStyle.TextType == ButtonStyle.TextTypes.Legacy)
            {
                if (buttonStyle.LegacyTextFont != null)
                    return new ProcessResult(ProcessResult.ResultStatus.Success);
                else
                    return new ProcessResult(ProcessResult.ResultStatus.Error, "Text font field is not assigned");
            }
            else if(buttonStyle.TextType == ButtonStyle.TextTypes.Mixed)
                return new ProcessResult(ProcessResult.ResultStatus.Success);

            return new ProcessResult(ProcessResult.ResultStatus.Error, "Unknown error. Send message to the developer"); ;
        }

        protected override void SetDirtyElements(ButtonStyleHandler handler)
        {
            if (handler.ButtonBG != null)
                EditorUtility.SetDirty(handler.ButtonBG);
            if (handler.ButtonText != null)
                EditorUtility.SetDirty(handler.ButtonText);
            if (handler.LegacyButtonText != null)
                EditorUtility.SetDirty(handler.LegacyButtonText);
        }
    }
}
