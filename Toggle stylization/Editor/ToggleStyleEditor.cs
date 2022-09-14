using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEditor;


namespace Stylization
{
    [CustomEditor(typeof(ToggleStyle))]
    public class ToggleStyleEditor : StyleEditor<ToggleStyle, ToggleStyleHandler>
    {
        private ImagePropsSet bgProps;

        private ImagePropsSet checkmarkProps;

        private TextPropsSet textProps;


        private SerializedProperty hasText;


        protected override void Init()
        {
            bgProps = new ImagePropsSet();

            bgProps.spritePropName = "bgSprite";
            bgProps.materialPropName = "bgMaterial";
            bgProps.colorPropName = "bgColor";
            bgProps.maskablePropName = "bgMaskable";

            FindImageProperties(bgProps);


            checkmarkProps = new ImagePropsSet();

            checkmarkProps.spritePropName = "checkmarkSprite";
            checkmarkProps.materialPropName = "checkmarkMaterial";
            checkmarkProps.colorPropName = "checkmarkColor";
            checkmarkProps.maskablePropName = "checkmarkMaskable";

            FindImageProperties(checkmarkProps);


            textProps = new TextPropsSet();

            hasText = serializedObject.FindProperty("hasText");
            textProps.staticTextPropName = "staticText";
            textProps.textPropName = "text";

            textProps.textTypePropName = "textType";
            textProps.textfontsizePropName = "textfontsize";
            textProps.foregroundPropName = "foreground";
            textProps.materialPropName = "material";
            textProps.maskablePropName = "maskable";

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
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            DrawImageParameters("Background", bgProps);

            EditorGUILayout.Separator();

            DrawImageParameters("Checkmark", checkmarkProps);

            EditorGUILayout.Separator();

            EditorGUILayout.LabelField("Text", EditorStyles.boldLabel);
            EditorGUILayout.Separator();
            EditorGUI.indentLevel++;

            EditorGUILayout.PropertyField(hasText, new GUIContent("Has text"));

            if (hasText.boolValue)
            {
                EditorGUILayout.Separator();

                DrawTextParameters(textProps);
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

            if (thisStyle.TextType == TextTypes.TextMeshPro)
            {
                if (thisStyle.TextFont != null)
                    return new ProcessResult(ProcessResult.ResultStatus.Success);
                else
                    return new ProcessResult(ProcessResult.ResultStatus.Error, "Text font field is not assigned");
            }
            else if (thisStyle.TextType == TextTypes.Legacy)
            {
                if (thisStyle.LegacyTextFont != null)
                    return new ProcessResult(ProcessResult.ResultStatus.Success);
                else
                    return new ProcessResult(ProcessResult.ResultStatus.Error, "Text font field is not assigned");
            }
            else if (thisStyle.TextType == TextTypes.Mixed)
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
