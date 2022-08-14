using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Stylization
{
    [AddComponentMenu("Stylization/Slider style handler")]
    public class SliderStyleHandler : StyleHandler<SliderStyle>
    {
        [SerializeField]
        private Image bg;

        public Image BG { get { return bg; } }

        [SerializeField]
        private Image fill;

        public Image Fill { get { return fill; } }

        [SerializeField]
        private Image handle;

        public Image Handle { get { return handle; } }

        private void Reset()
        {
            Slider slider = GetComponent<Slider>();
            if (slider != null)
            {
                if (slider.fillRect != null)
                {
                    fill = slider.fillRect.gameObject.GetComponent<Image>();
                }
                RectTransform sliderTransform = slider.GetComponent<RectTransform>();
                if (sliderTransform.childCount > 0)
                    bg = sliderTransform.GetChild(0).GetComponent<Image>();
                if (slider.handleRect != null)
                    handle = slider.handleRect.gameObject.GetComponent<Image>();
            }
        }

        protected override bool UpdateComponents(bool debug)
        {
            if (bg != null)
            {
                bg.sprite = Style.BGSprite;
                bg.material = Style.BGMaterial;
                bg.color = Style.BGColor;
                bg.maskable = Style.BGMaskable;
            }
            else
            {
                if (debug)
                {
                    Debug.LogError("Background field is null. Please, assign this field", this);
                    return false;
                }
            }

            if (fill != null)
            {
                fill.sprite = Style.FillSprite;
                fill.material = Style.FillMaterial;
                fill.color = Style.FillColor;
                fill.maskable = Style.FillMaskable;
            }
            else
            {
                if (debug)
                {
                    Debug.LogError("Fill field is null. Please, assign this field", this);
                    return false;
                }
            }

            if (handle != null)
            {
                handle.sprite = Style.HandleSprite;
                handle.material = Style.HandleMaterial;
                handle.color = Style.HandleColor;
                fill.maskable = Style.HandleMaskable;
            }
            else
            {
                if (debug)
                {
                    Debug.LogError("Handle field is null. Please, assign this field", this);
                    return false;
                }
            }
            return true;
        }

        [ContextMenu("Clear data")]
        private void ClearComponentsData()
        {
            bg = null;
            fill = null;
            handle = null;
        }
    }
}
