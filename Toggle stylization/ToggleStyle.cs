using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Stylization
{
    [CreateAssetMenu(fileName = "New toggle style", menuName = "Styles/Toggle style")]
    public class ToggleStyle : Style
    {
        // Background

        [SerializeField]
        private Sprite bgSprite;

        public Sprite BGSprite { get { return bgSprite; } }

        [SerializeField]
        private Material bgMaterial;

        public Material BGMaterial { get { return bgMaterial; } }


        [SerializeField]
        private Color bgColor = Color.white;

        public Color BGColor { get { return bgColor; } }

        [SerializeField]
        private bool bgMaskable = true;

        public bool BGMaskable { get { return bgMaskable; } }


        // Check mark

        [SerializeField]
        private Sprite checkmarkSprite;

        public Sprite CheckmarkSprite { get { return checkmarkSprite; } }

        [SerializeField]
        private Material checkmarkMaterial;

        public Material CheckmarkMaterial { get { return checkmarkMaterial; } }


        [SerializeField]
        private Color checkmarkColor = Color.white;

        public Color CheckmarkColor { get { return checkmarkColor; } }

        [SerializeField]
        private bool checkmarkMaskable = true;

        public bool CheckmarkMaskable { get { return checkmarkMaskable; } }

        // Text

        [SerializeField]
        private bool hasText;

        public bool HasText { get { return hasText; } }

        public enum TextTypes
        {
            TextMeshPro,
            Legacy,
            Mixed
        }

        [SerializeField]
        private TextTypes textType = TextTypes.TextMeshPro;

        public TextTypes TextType { get { return textType; } }


        [SerializeField]
        private float textfontsize = 30;

        public float FontSize { get { return textfontsize; } }


        [SerializeField]
        private Color foreground = Color.white;

        public Color Foreground { get { return foreground; } }


        [SerializeField]
        private TMPro.TMP_FontAsset textFont;

        public TMPro.TMP_FontAsset TextFont { get { return textFont; } }

        [SerializeField]
        private Font legacyTextFont;

        public Font LegacyTextFont { get { return legacyTextFont; } }
    }
}
