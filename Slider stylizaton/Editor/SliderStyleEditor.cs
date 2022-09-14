using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEditor;


namespace Stylization
{
    [CustomEditor(typeof(SliderStyle))]
    public class SliderStyleEditor : StyleEditor<SliderStyle, SliderStyleHandler>
    {
        private ImagePropsSet bgProps;

        private ImagePropsSet fillProps;

        private SerializedProperty hasHandle;

        private ImagePropsSet handleProps;

        protected override void Init()
        {
            bgProps = new ImagePropsSet();

            bgProps.spritePropName = "bgSprite";
            bgProps.materialPropName = "bgMaterial";
            bgProps.colorPropName = "bgColor";
            bgProps.maskablePropName = "bgMaskable";

            FindImageProperties(bgProps);


            fillProps = new ImagePropsSet();

            fillProps.spritePropName = "fillSprite";
            fillProps.materialPropName = "fillMaterial";
            fillProps.colorPropName = "fillColor";
            fillProps.maskablePropName = "fillMaskable";

            FindImageProperties(fillProps);


            hasHandle = serializedObject.FindProperty("hasHandle");
            handleProps = new ImagePropsSet();

            handleProps.spritePropName = "handleSprite";
            handleProps.materialPropName = "handleMaterial";
            handleProps.colorPropName = "handleColor";
            handleProps.maskablePropName = "handleMaskable";

            FindImageProperties(handleProps);
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            DrawImageParameters("Background", bgProps);

            EditorGUILayout.Separator();

            DrawImageParameters("Fill", fillProps);

            EditorGUILayout.Separator();

            EditorGUILayout.LabelField("Handle", EditorStyles.boldLabel);

            EditorGUILayout.Separator();
            EditorGUI.indentLevel++;

            EditorGUILayout.PropertyField(hasHandle, new GUIContent("Has handle"));

            if (hasHandle.boolValue)
            {
                EditorGUILayout.Separator();

                DrawImageParameters(handleProps);
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
