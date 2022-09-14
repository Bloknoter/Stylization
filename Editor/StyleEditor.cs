using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEditor;


namespace Stylization
{
    public abstract class StyleEditor<TStyle, THandler> : Editor where TStyle : Style where THandler : StyleHandler<TStyle>
    {
        private List<ProcessResult> updateResults = new List<ProcessResult>();

        private ProcessResult checkResult;

        private Vector2 scrollPos;

        private GUIContent warningIcon;

        private GUIContent emptyLabel;

        private void OnEnable()
        {
            warningIcon = EditorGUIUtility.IconContent("console.warnicon.sml");
            emptyLabel = new GUIContent();
            Init();
        }

        protected abstract void Init();

        protected void FindImageProperties(ImagePropsSet targetSet)
        {
            targetSet.sprite = serializedObject.FindProperty(targetSet.spritePropName);
            targetSet.material = serializedObject.FindProperty(targetSet.materialPropName);
            targetSet.color = serializedObject.FindProperty(targetSet.colorPropName);
            targetSet.maskable = serializedObject.FindProperty(targetSet.maskablePropName);
        }

        protected void FindTextProperties(TextPropsSet targetSet)
        {
            targetSet.staticText = serializedObject.FindProperty(targetSet.staticTextPropName);
            targetSet.text = serializedObject.FindProperty(targetSet.textPropName);

            targetSet.textType = serializedObject.FindProperty(targetSet.textTypePropName);
            targetSet.textfontsize = serializedObject.FindProperty(targetSet.textfontsizePropName);
            targetSet.foreground = serializedObject.FindProperty(targetSet.foregroundPropName);
            targetSet.material = serializedObject.FindProperty(targetSet.materialPropName);
            targetSet.maskable = serializedObject.FindProperty(targetSet.maskablePropName);

            targetSet.textFont = serializedObject.FindProperty(targetSet.textFontPropName);
            targetSet.fontStyle = serializedObject.FindProperty(targetSet.fontStylePropName);
            targetSet.characterSpacing = serializedObject.FindProperty(targetSet.characterSpacingPropName);
            targetSet.wordSpacing = serializedObject.FindProperty(targetSet.wordSpacingPropName);
            targetSet.lineSpacing = serializedObject.FindProperty(targetSet.lineSpacingPropName);
            targetSet.paragraphSpacing = serializedObject.FindProperty(targetSet.paragraphSpacingPropName);
            targetSet.horizontalAlignment = serializedObject.FindProperty(targetSet.horizontalAlignmentPropName);
            targetSet.verticalAlignment = serializedObject.FindProperty(targetSet.verticalAlignmentPropName);

            targetSet.legacyTextFont = serializedObject.FindProperty(targetSet.legacyTextFontPropName);
            targetSet.legacyFontStyle = serializedObject.FindProperty(targetSet.legacyFontStylePropName);
            targetSet.legacyAlignment = serializedObject.FindProperty(targetSet.legacyAlignmentPropName);
            targetSet.legacyAlignByGeometry = serializedObject.FindProperty(targetSet.legacyAlignByGeometryPropName);
        }


        protected void DrawImageParameters(ImagePropsSet propsSet)
        {
            EditorGUILayout.PropertyField(propsSet.sprite, new GUIContent("Sprite"));

            EditorGUILayout.PropertyField(propsSet.material, new GUIContent("Material"));

            EditorGUILayout.PropertyField(propsSet.color, new GUIContent("Color"));

            EditorGUILayout.PropertyField(propsSet.maskable, new GUIContent("Maskable"));
        }

        protected void DrawImageParameters(string header, ImagePropsSet propsSet)
        {
            EditorGUILayout.LabelField(header, EditorStyles.boldLabel);
            EditorGUILayout.Separator();
            EditorGUI.indentLevel++;

            DrawImageParameters(propsSet);

            EditorGUI.indentLevel--;
        }


        protected void DrawTextParameters(TextPropsSet propsSet)
        {
            EditorGUILayout.PropertyField(propsSet.staticText, new GUIContent("Static text"));

            if (propsSet.staticText.boolValue)
            {
                EditorGUILayout.PropertyField(propsSet.text, new GUIContent());
            }

            EditorGUILayout.Separator();

            EditorGUILayout.PropertyField(propsSet.textType, new GUIContent("Text type"));

            EditorGUILayout.Separator();
            EditorGUILayout.Separator();

            if (propsSet.textType.enumValueIndex == (int)TextTypes.TextMeshPro)
            {
                EditorGUILayout.PropertyField(propsSet.textFont, new GUIContent("Font"));
                EditorGUILayout.PropertyField(propsSet.fontStyle, new GUIContent("Text style"));
                EditorGUILayout.PropertyField(propsSet.textfontsize, new GUIContent("Font size"));
                if (propsSet.textfontsize.floatValue < 0.01f)
                    propsSet.textfontsize.floatValue = 0.01f;
            }
            else if (propsSet.textType.enumValueIndex == (int)TextTypes.Legacy)
            {
                EditorGUILayout.PropertyField(propsSet.legacyTextFont, new GUIContent("Font"));
                EditorGUILayout.PropertyField(propsSet.legacyFontStyle, new GUIContent("Text type"));
                propsSet.textfontsize.floatValue = EditorGUILayout.IntField("Font size", (int)propsSet.textfontsize.floatValue);
                if (propsSet.textfontsize.floatValue < 1f)
                    propsSet.textfontsize.floatValue = 1f;
            }
            else if (propsSet.textType.enumValueIndex == (int)TextTypes.Mixed)
            {
                propsSet.textfontsize.floatValue = EditorGUILayout.IntField("Font size", (int)propsSet.textfontsize.floatValue);
                if (propsSet.textfontsize.floatValue < 1f)
                    propsSet.textfontsize.floatValue = 1f;
            }
            EditorGUILayout.PropertyField(propsSet.foreground, new GUIContent("Foreground"));

            EditorGUILayout.Separator();
            EditorGUILayout.Separator();

            if (propsSet.textType.enumValueIndex == (int)TextTypes.TextMeshPro)
            {
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("Spacing options", GUILayout.Width(100));
                EditorGUILayout.LabelField("Character", GUILayout.Width(60));
                EditorGUILayout.PropertyField(propsSet.characterSpacing, emptyLabel, GUILayout.Width(50));

                EditorGUILayout.LabelField(emptyLabel, GUILayout.Width(10));

                EditorGUILayout.LabelField("Word", GUILayout.Width(70));
                EditorGUILayout.PropertyField(propsSet.wordSpacing, emptyLabel, GUILayout.Width(50));

                EditorGUILayout.EndHorizontal();

                EditorGUILayout.BeginHorizontal();

                EditorGUILayout.LabelField("", GUILayout.Width(100));
                EditorGUILayout.LabelField("Line", GUILayout.Width(60));
                EditorGUILayout.PropertyField(propsSet.lineSpacing, emptyLabel, GUILayout.Width(50));

                EditorGUILayout.LabelField(emptyLabel, GUILayout.Width(10));

                EditorGUILayout.LabelField("Paragraph", GUILayout.Width(70));
                EditorGUILayout.PropertyField(propsSet.paragraphSpacing, emptyLabel, GUILayout.Width(50));

                EditorGUILayout.EndHorizontal();

                EditorGUILayout.Separator();
                EditorGUILayout.Separator();

                EditorGUILayout.PropertyField(propsSet.horizontalAlignment, new GUIContent("Horizontal alignment"));
                EditorGUILayout.PropertyField(propsSet.verticalAlignment, new GUIContent("Vertical alignment"));
            }
            else if (propsSet.textType.enumValueIndex == (int)TextTypes.Legacy)
            {
                EditorGUILayout.PropertyField(propsSet.lineSpacing, new GUIContent("Line spacing"));
                EditorGUILayout.PropertyField(propsSet.legacyAlignment, new GUIContent("Alignment"));
                EditorGUILayout.PropertyField(propsSet.legacyAlignByGeometry, new GUIContent("Align By Geometry"));
            }
            else if (propsSet.textType.enumValueIndex == (int)TextTypes.Mixed)
            {
                EditorGUILayout.PropertyField(propsSet.lineSpacing, new GUIContent("Line spacing"));
            }

            EditorGUILayout.Separator();
            EditorGUILayout.Separator();

            EditorGUILayout.PropertyField(propsSet.material, new GUIContent("Material"));

            EditorGUILayout.PropertyField(propsSet.maskable, new GUIContent("Maskable"));
        }

        protected void DrawTextParameters(string header, TextPropsSet propsSet)
        {
            EditorGUILayout.LabelField(header, EditorStyles.boldLabel);
            EditorGUILayout.Separator();
            EditorGUI.indentLevel++;

            DrawTextParameters(propsSet);

            EditorGUI.indentLevel--;
        }


        protected void DrawTMProParameters(TextPropsSet propsSet)
        {
            EditorGUILayout.PropertyField(propsSet.staticText, new GUIContent("Static text"));

            if (propsSet.staticText.boolValue)
            {
                EditorGUILayout.PropertyField(propsSet.text, new GUIContent());
            }

            EditorGUILayout.Separator();

            EditorGUILayout.PropertyField(propsSet.textType, new GUIContent("Text type"));

            EditorGUILayout.Separator();
            EditorGUILayout.Separator();

            EditorGUILayout.PropertyField(propsSet.textFont, new GUIContent("Font"));
            EditorGUILayout.PropertyField(propsSet.fontStyle, new GUIContent("Text style"));
            EditorGUILayout.PropertyField(propsSet.textfontsize, new GUIContent("Font size"));
            if (propsSet.textfontsize.floatValue < 0.01f)
                propsSet.textfontsize.floatValue = 0.01f;

            EditorGUILayout.PropertyField(propsSet.foreground, new GUIContent("Foreground"));

            EditorGUILayout.Separator();
            EditorGUILayout.Separator();

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Spacing options", GUILayout.Width(100));
            EditorGUILayout.LabelField("Character", GUILayout.Width(60));
            EditorGUILayout.PropertyField(propsSet.characterSpacing, emptyLabel, GUILayout.Width(50));

            EditorGUILayout.LabelField(emptyLabel, GUILayout.Width(10));

            EditorGUILayout.LabelField("Word", GUILayout.Width(70));
            EditorGUILayout.PropertyField(propsSet.wordSpacing, emptyLabel, GUILayout.Width(50));

            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();

            EditorGUILayout.LabelField("", GUILayout.Width(100));
            EditorGUILayout.LabelField("Line", GUILayout.Width(60));
            EditorGUILayout.PropertyField(propsSet.lineSpacing, emptyLabel, GUILayout.Width(50));

            EditorGUILayout.LabelField(emptyLabel, GUILayout.Width(10));

            EditorGUILayout.LabelField("Paragraph", GUILayout.Width(70));
            EditorGUILayout.PropertyField(propsSet.paragraphSpacing, emptyLabel, GUILayout.Width(50));

            EditorGUILayout.EndHorizontal();

            EditorGUILayout.Separator();
            EditorGUILayout.Separator();

            EditorGUILayout.PropertyField(propsSet.horizontalAlignment, new GUIContent("Horizontal alignment"));
            EditorGUILayout.PropertyField(propsSet.verticalAlignment, new GUIContent("Vertical alignment"));

            EditorGUILayout.Separator();
            EditorGUILayout.Separator();

            EditorGUILayout.PropertyField(propsSet.material, new GUIContent("Material"));

            EditorGUILayout.PropertyField(propsSet.maskable, new GUIContent("Maskable"));
        }

        protected void DrawTMProParameters(string header, TextPropsSet propsSet)
        {
            EditorGUILayout.LabelField(header, EditorStyles.boldLabel);
            EditorGUILayout.Separator();
            EditorGUI.indentLevel++;

            DrawTMProParameters(propsSet);

            EditorGUI.indentLevel--;
        }


        protected void DrawLegacyTextParameters(TextPropsSet propsSet)
        {
            EditorGUILayout.PropertyField(propsSet.staticText, new GUIContent("Static text"));

            if (propsSet.staticText.boolValue)
            {
                EditorGUILayout.PropertyField(propsSet.text, new GUIContent());
            }

            EditorGUILayout.Separator();

            EditorGUILayout.PropertyField(propsSet.textType, new GUIContent("Text type"));

            EditorGUILayout.Separator();
            EditorGUILayout.Separator();

            EditorGUILayout.PropertyField(propsSet.legacyTextFont, new GUIContent("Font"));
            EditorGUILayout.PropertyField(propsSet.legacyFontStyle, new GUIContent("Text type"));
            propsSet.textfontsize.floatValue = EditorGUILayout.IntField("Font size", (int)propsSet.textfontsize.floatValue);
            if (propsSet.textfontsize.floatValue < 1f)
                propsSet.textfontsize.floatValue = 1f;

            EditorGUILayout.PropertyField(propsSet.foreground, new GUIContent("Foreground"));

            EditorGUILayout.Separator();
            EditorGUILayout.Separator();

            EditorGUILayout.PropertyField(propsSet.lineSpacing, new GUIContent("Line spacing"));
            EditorGUILayout.PropertyField(propsSet.legacyAlignment, new GUIContent("Alignment"));
            EditorGUILayout.PropertyField(propsSet.legacyAlignByGeometry, new GUIContent("Align By Geometry"));

            EditorGUILayout.Separator();
            EditorGUILayout.Separator();

            EditorGUILayout.PropertyField(propsSet.material, new GUIContent("Material"));

            EditorGUILayout.PropertyField(propsSet.maskable, new GUIContent("Maskable"));
        }

        protected void DrawLegacyTextParameters(string header, TextPropsSet propsSet)
        {
            EditorGUILayout.LabelField(header, EditorStyles.boldLabel);
            EditorGUILayout.Separator();
            EditorGUI.indentLevel++;

            DrawLegacyTextParameters(propsSet);

            EditorGUI.indentLevel--;
        }


        protected void DrawApplyButton()
        {
            if (GUILayout.Button("Apply", GUILayout.MinWidth(100)))
            {
                checkResult = CanApply();
                if (checkResult.Status == ProcessResult.ResultStatus.Success)
                {
                    ApplyParams();
                }
            }
        }

        private void ApplyParams()
        {
            updateResults.Clear();
            THandler[] allHandlers = FindObjectsOfType<THandler>();
            for (int i = 0; i < allHandlers.Length; i++)
            {
                if (allHandlers[i].Style == target)
                {
                    bool result = allHandlers[i].UpdateComponents();
                    if (result == true)
                        updateResults.Add(new ProcessResult(ProcessResult.ResultStatus.Success, allHandlers[i]));
                    else
                        updateResults.Add(new ProcessResult(ProcessResult.ResultStatus.Error, allHandlers[i]));
                    SetDirtyElements(allHandlers[i]);
                }
            }
        }

        protected void DrawApplyResult()
        {
            if (checkResult != null && checkResult.Status == ProcessResult.ResultStatus.Error)
            {
                GUIContent errorContent = new GUIContent(checkResult.Message, warningIcon.image);
                EditorGUILayout.LabelField(errorContent);
            }
            else if (updateResults.Count > 0)
            {
                EditorGUILayout.BeginVertical("Tooltip");

                EditorGUILayout.LabelField($"Applied to:");
                EditorGUILayout.Separator();

                bool hasScroll = false;
                if (updateResults.Count > 8)
                {
                    hasScroll = true;
                    scrollPos = EditorGUILayout.BeginScrollView(scrollPos, false, true, GUILayout.Height(150));
                }

                for (int i = 0; i < updateResults.Count; i++)
                {
                    if (updateResults[i] != null)
                    {
                        EditorGUILayout.BeginHorizontal();
                        if(GUILayout.Button("->", GUILayout.Width(40)))
                        {
                            Selection.activeObject = updateResults[i].AssociatedObject;
                        }
                        if (updateResults[i].Status == ProcessResult.ResultStatus.Success)
                            EditorGUILayout.LabelField($"{((THandler)updateResults[i].AssociatedObject).gameObject.name}");
                        else
                        {
                            EditorGUILayout.LabelField(warningIcon, GUILayout.Width(20));
                            EditorGUILayout.LabelField($"{((THandler)updateResults[i].AssociatedObject).gameObject.name} : error. Check the console for details");
                        }
                        EditorGUILayout.EndHorizontal();
                    }
                    else
                    {
                        updateResults.RemoveAt(i);
                        i--;
                    }
                }
                if (hasScroll)
                    EditorGUILayout.EndScrollView();
                EditorGUILayout.EndVertical();
            }
            else
            {
                if (checkResult != null && checkResult.Status == ProcessResult.ResultStatus.Success)
                {
                    EditorGUILayout.BeginVertical("Tooltip");

                    EditorGUILayout.LabelField($"components with this style not found");

                    EditorGUILayout.EndVertical();
                }
            }
        }

        protected abstract ProcessResult CanApply();

        protected abstract void SetDirtyElements(THandler handler);

        protected class ImagePropsSet
        {
            public SerializedProperty sprite;

            public SerializedProperty material;

            public SerializedProperty color;

            public SerializedProperty maskable;


            public string spritePropName = "";

            public string materialPropName = "";

            public string colorPropName = "";

            public string maskablePropName = "";
        }

        protected class TextPropsSet
        {
            public SerializedProperty staticText;

            public SerializedProperty text;

            public SerializedProperty textType;

            public SerializedProperty textfontsize;

            public SerializedProperty foreground;

            public SerializedProperty material;

            public SerializedProperty maskable;

            // TextMeshPro

            public SerializedProperty textFont;

            public SerializedProperty fontStyle;

            public SerializedProperty characterSpacing;

            public SerializedProperty wordSpacing;

            public SerializedProperty lineSpacing;

            public SerializedProperty paragraphSpacing;

            public SerializedProperty horizontalAlignment;

            public SerializedProperty verticalAlignment;

            // Legacy Text

            public SerializedProperty legacyTextFont;

            public SerializedProperty legacyFontStyle;

            public SerializedProperty legacyAlignment;

            public SerializedProperty legacyAlignByGeometry;


            // Name of properties

            public string staticTextPropName;

            public string textPropName;

            public string textTypePropName;

            public string textfontsizePropName;

            public string foregroundPropName;

            public string materialPropName;

            public string maskablePropName;

            // TextMeshPro

            public string textFontPropName;

            public string fontStylePropName;

            public string characterSpacingPropName;

            public string wordSpacingPropName;

            public string lineSpacingPropName;

            public string paragraphSpacingPropName;

            public string horizontalAlignmentPropName;

            public string verticalAlignmentPropName;

            // Legacy Text

            public string legacyTextFontPropName;

            public string legacyFontStylePropName;

            public string legacyAlignmentPropName;

            public string legacyAlignByGeometryPropName;
        }
    }
}
