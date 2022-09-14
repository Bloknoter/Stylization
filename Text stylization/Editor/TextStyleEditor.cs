using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


namespace Stylization
{
    [CustomEditor(typeof(TextStyle))]
    public class TextStyleEditor : StyleEditor<TextStyle, TextStyleHandler>
    {
        private SerializedProperty staticText;

        private SerializedProperty text;

        private SerializedProperty textType;

        private SerializedProperty textfontsize;

        private SerializedProperty foreground;

        private SerializedProperty material;

        private SerializedProperty maskable;

        // TextMeshPro

        private SerializedProperty textFont;

        private SerializedProperty fontStyle;

        private SerializedProperty characterSpacing;

        private SerializedProperty wordSpacing;

        private SerializedProperty lineSpacing;

        private SerializedProperty paragraphSpacing;

        private SerializedProperty horizontalAlignment;

        private SerializedProperty verticalAlignment;

        // Legacy Text

        private SerializedProperty legacyTextFont;

        private SerializedProperty legacyFontStyle;

        private SerializedProperty legacyAlignment;

        private SerializedProperty legacyAlignByGeometry;

        private GUIContent emptyLabel;

        protected override void Init()
        {
            staticText = serializedObject.FindProperty("staticText");
            text = serializedObject.FindProperty("text");

            textType = serializedObject.FindProperty("textType");
            textfontsize = serializedObject.FindProperty("textfontsize");
            foreground = serializedObject.FindProperty("foreground");
            material = serializedObject.FindProperty("material");
            maskable = serializedObject.FindProperty("maskable");

            textFont = serializedObject.FindProperty("textFont");
            fontStyle = serializedObject.FindProperty("fontStyle");
            characterSpacing = serializedObject.FindProperty("characterSpacing");
            wordSpacing = serializedObject.FindProperty("wordSpacing");
            lineSpacing = serializedObject.FindProperty("lineSpacing");
            paragraphSpacing = serializedObject.FindProperty("paragraphSpacing");
            horizontalAlignment = serializedObject.FindProperty("horizontalAlignment");
            verticalAlignment = serializedObject.FindProperty("verticalAlignment");

            legacyTextFont = serializedObject.FindProperty("legacyTextFont");
            legacyFontStyle = serializedObject.FindProperty("legacyFontStyle");
            legacyAlignment = serializedObject.FindProperty("legacyAlignment");
            legacyAlignByGeometry = serializedObject.FindProperty("legacyAlignByGeometry");

            emptyLabel = new GUIContent();
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            TextStyle componentStyle = (TextStyle)target;

            EditorGUILayout.PropertyField(staticText, new GUIContent("Static text"));

            if(staticText.boolValue)
            {
                EditorGUILayout.PropertyField(text, new GUIContent());
            }

            EditorGUILayout.Separator();

            EditorGUILayout.PropertyField(textType, new GUIContent("Text type"));

            EditorGUILayout.Separator();
            EditorGUILayout.Separator();

            if (componentStyle.TextType == TextTypes.TextMeshPro)
            {
                EditorGUILayout.PropertyField(textFont, new GUIContent("Font"));
                EditorGUILayout.PropertyField(fontStyle, new GUIContent("Text style"));
                EditorGUILayout.PropertyField(textfontsize, new GUIContent("Font size"));
                if (textfontsize.floatValue < 0.01f)
                    textfontsize.floatValue = 0.01f;
            }
            else if (componentStyle.TextType == TextTypes.Legacy)
            {
                EditorGUILayout.PropertyField(legacyTextFont, new GUIContent("Font"));
                EditorGUILayout.PropertyField(legacyFontStyle, new GUIContent("Text type"));
                textfontsize.floatValue = EditorGUILayout.IntField("Font size", (int)textfontsize.floatValue);
                if (textfontsize.floatValue < 1f)
                    textfontsize.floatValue = 1f;
            }
            else if (componentStyle.TextType == TextTypes.Mixed)
            {
                textfontsize.floatValue = EditorGUILayout.IntField("Font size", (int)textfontsize.floatValue);
                if (textfontsize.floatValue < 1f)
                    textfontsize.floatValue = 1f;
            }
            EditorGUILayout.PropertyField(foreground, new GUIContent("Foreground"));

            EditorGUILayout.Separator();
            EditorGUILayout.Separator();

            if (componentStyle.TextType == TextTypes.TextMeshPro)
            {
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("Spacing options", GUILayout.Width(100));
                EditorGUILayout.LabelField("Character", GUILayout.Width(60));
                EditorGUILayout.PropertyField(characterSpacing, emptyLabel, GUILayout.Width(50));

                EditorGUILayout.LabelField(emptyLabel, GUILayout.Width(10));

                EditorGUILayout.LabelField("Word", GUILayout.Width(70));
                EditorGUILayout.PropertyField(wordSpacing, emptyLabel, GUILayout.Width(50));

                EditorGUILayout.EndHorizontal();

                EditorGUILayout.BeginHorizontal();

                EditorGUILayout.LabelField("", GUILayout.Width(100));
                EditorGUILayout.LabelField("Line", GUILayout.Width(60));
                EditorGUILayout.PropertyField(lineSpacing, emptyLabel, GUILayout.Width(50));

                EditorGUILayout.LabelField(emptyLabel, GUILayout.Width(10));

                EditorGUILayout.LabelField("Paragraph", GUILayout.Width(70));
                EditorGUILayout.PropertyField(paragraphSpacing, emptyLabel, GUILayout.Width(50));

                EditorGUILayout.EndHorizontal();

                EditorGUILayout.Separator();
                EditorGUILayout.Separator();

                EditorGUILayout.PropertyField(horizontalAlignment, new GUIContent("Horizontal alignment"));
                EditorGUILayout.PropertyField(verticalAlignment, new GUIContent("Vertical alignment"));
            }
            else if (componentStyle.TextType == TextTypes.Legacy)
            {
                EditorGUILayout.PropertyField(lineSpacing, new GUIContent("Line spacing"));
                EditorGUILayout.PropertyField(legacyAlignment, new GUIContent("Alignment"));
                EditorGUILayout.PropertyField(legacyAlignByGeometry, new GUIContent("Align By Geometry"));
            }
            else if (componentStyle.TextType == TextTypes.Mixed)
            {
                EditorGUILayout.PropertyField(lineSpacing, new GUIContent("Line spacing"));
            }

            EditorGUILayout.Separator();
            EditorGUILayout.Separator();

            EditorGUILayout.PropertyField(material, new GUIContent("Material"));

            EditorGUILayout.PropertyField(maskable, new GUIContent("Maskable"));


            EditorGUILayout.Separator();
            EditorGUILayout.Separator();

            DrawApplyButton();

            EditorGUILayout.Separator();

            DrawApplyResult();

            serializedObject.ApplyModifiedProperties();
        }

        protected override ProcessResult CanApply()
        {
            TextStyle componentStyle = (TextStyle)target;

            if (componentStyle.TextType == TextTypes.TextMeshPro)
            {
                if (componentStyle.TextFont != null)
                    return new ProcessResult(ProcessResult.ResultStatus.Success);
                else
                    return new ProcessResult(ProcessResult.ResultStatus.Error, "Text font field is not assigned");
            }
            else if (componentStyle.TextType == TextTypes.Legacy)
            {
                if (componentStyle.LegacyTextFont != null)
                    return new ProcessResult(ProcessResult.ResultStatus.Success);
                else
                    return new ProcessResult(ProcessResult.ResultStatus.Error, "Text font field is not assigned");
            }
            else if (componentStyle.TextType == TextTypes.Mixed)
                return new ProcessResult(ProcessResult.ResultStatus.Success);

            return new ProcessResult(ProcessResult.ResultStatus.Error, "Unknown error. Send message to the developer"); ;
        }

        protected override void SetDirtyElements(TextStyleHandler handler)
        {
            if (handler.Text != null)
                EditorUtility.SetDirty(handler.Text);
            if (handler.LegacyText != null)
                EditorUtility.SetDirty(handler.LegacyText);
        }
    }
}
