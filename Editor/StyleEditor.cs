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


        private void OnEnable()
        {
            warningIcon = EditorGUIUtility.IconContent("console.warnicon.sml");

            Init();
        }

        protected abstract void Init();

        protected void DrawImageParameters(SerializedProperty sprite, SerializedProperty material,
            SerializedProperty color, SerializedProperty maskable)
        {
            EditorGUILayout.PropertyField(sprite, new GUIContent("Sprite"));

            EditorGUILayout.PropertyField(material, new GUIContent("Material"));

            EditorGUILayout.PropertyField(color, new GUIContent("Color"));

            EditorGUILayout.PropertyField(maskable, new GUIContent("Maskable"));
        }

        protected void DrawImageParameters(string header, SerializedProperty sprite, SerializedProperty material, 
            SerializedProperty color, SerializedProperty maskable)
        {
            EditorGUILayout.LabelField(header, EditorStyles.boldLabel);
            EditorGUILayout.Separator();
            EditorGUI.indentLevel++;

            EditorGUILayout.PropertyField(sprite, new GUIContent("Sprite"));

            EditorGUILayout.PropertyField(material, new GUIContent("Material"));

            EditorGUILayout.PropertyField(color, new GUIContent("Color"));

            EditorGUILayout.PropertyField(maskable, new GUIContent("Maskable"));

            EditorGUI.indentLevel--;
        }

        protected void DrawApplyButton()
        {
            if (GUILayout.Button("Apply", GUILayout.MinWidth(100)))
            {
                checkResult = CanApply();
                if (checkResult.Status == ProcessResult.ResultStatus.Success)
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
    }
}
