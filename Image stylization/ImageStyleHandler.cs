using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Stylization
{
    [AddComponentMenu("Stylization/Image style handler")]
    public class ImageStyleHandler : StyleHandler<ImageStyle>
    {
        [SerializeField]
        private Image image;

        public Image Image { get { return image; } }

        private void Reset()
        {
            image = GetComponent<Image>();
        }

        protected override bool UpdateComponents(bool debug)
        {
            if (image != null)
            {
                image.sprite = Style.Sprite;
                image.material = Style.Material;
                image.color = Style.Color;
                image.maskable = Style.Maskable;
            }
            else
            {
                if (debug)
                {
                    Debug.LogError("Image field is null. Please, assign this field", this);
                    return false;
                }
            }
            return true;
        }
    }
}
