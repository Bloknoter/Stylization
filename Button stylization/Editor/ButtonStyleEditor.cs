using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEditor;


namespace Stylization
{
    [CustomEditor(typeof(ButtonStyle))]
    public class ButtonStyleEditor : StyleEditor<ButtonStyle, ButtonStyleHandler>
    {

        private ImagePropsSet bgProps;

        private TextPropsSet textProps;


        private SerializedProperty hasText;


        private GUIContent HasTextLabel;


        protected override void Init()
        {
            bgProps = new ImagePropsSet();

            bgProps.spritePropName = "bgSprite";
            bgProps.materialPropName = "bgMaterial";
            bgProps.colorPropName = "bgColor";
            bgProps.maskablePropName = "maskable";

            FindImageProperties(bgProps);

            textProps = new TextPropsSet();

            hasText = serializedObject.FindProperty("hasText");
            textProps.staticTextPropName = "staticText";
            textProps.textPropName = "text";

            textProps.textTypePropName = "textType";
            textProps.textfontsizePropName = "textfontsize";
            textProps.foregroundPropName = "foreground";
            textProps.materialPropName = "material";
            textProps.maskablePropName = "textMaskable";

            textProps.textFontPropName = "textFont";
            textProps.fontStylePropName = "fontStyle";
            textProps.characterSpacingPropName = "characterSpacing";
            textProps.wordSpacingPropName = "wordSpacing";
            textProps.lineSpacingPropName = "lineSpacing";
            textProps.paragraphSpacingPropName = "paragraphSpacing";
            textProps.horizontalAlignmentPropName = "horizontalAlignment";
            textProps.verticalAlignmentPropName = "verticalAlignment";

            textProps.legacyTextFontPropName = "legacyTextFont";
            textProps.legacyFontStylePropName = "legacyFontStyle";
            textProps.legacyAlignmentPropName = "legacyAlignment";
            textProps.legacyAlignByGeometryPropName = "legacyAlignByGeometry";

            FindTextProperties(textProps);

            HasTextLabel = new GUIContent("Has text");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            DrawImageParameters("Background", bgProps);

            EditorGUILayout.Separator();

            EditorGUILayout.LabelField("Text", EditorStyles.boldLabel);
            EditorGUILayout.Separator();
            EditorGUI.indentLevel++;

            EditorGUILayout.PropertyField(hasText, HasTextLabel);

            if (hasText.boolValue)
            {
                EditorGUILayout.Separator();

                DrawTextParameters(textProps);
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

            if (buttonStyle.TextType == TextTypes.TextMeshPro)
            {
                if (buttonStyle.TextFont != null)
                    return new ProcessResult(ProcessResult.ResultStatus.Success);
                else
                    return new ProcessResult(ProcessResult.ResultStatus.Error, "Text font field is not assigned");
            }
            else if (buttonStyle.TextType == TextTypes.Legacy)
            {
                if (buttonStyle.LegacyTextFont != null)
                    return new ProcessResult(ProcessResult.ResultStatus.Success);
                else
                    return new ProcessResult(ProcessResult.ResultStatus.Error, "Text font field is not assigned");
            }
            else if(buttonStyle.TextType == TextTypes.Mixed)
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
