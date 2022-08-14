using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Stylization
{
    [CreateAssetMenu(fileName = "New button style", menuName = "Styles/Button style")]
    [System.Serializable]
    public class ButtonStyle : Style
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
        private bool maskable = true;

        public bool Maskable { get { return maskable; } }


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
