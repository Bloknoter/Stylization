using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEditor;


namespace Stylization
{
    [CustomEditor(typeof(ImageStyle))]
    public class ImageStyleEditor : StyleEditor<ImageStyle, ImageStyleHandler>
    {
        private ImagePropsSet props;

        protected override void Init()
        {
            props = new ImagePropsSet();

            props.spritePropName = "sprite";
            props.materialPropName = "material";
            props.colorPropName = "color";
            props.maskablePropName = "maskable";

            FindImageProperties(props);
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            DrawImageParameters(props);

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
