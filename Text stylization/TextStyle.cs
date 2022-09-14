using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Stylization
{
    [CreateAssetMenu(fileName = "New text style", menuName = "Styles/Text style")]
    public class TextStyle : Style
    {
        // General

        [SerializeField]
        private bool staticText;
        public bool StaticText { get { return staticText; } }

        [SerializeField]
        [TextArea]
        private string text = "";
        public string Text { get { return text; } }


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
        private Material material;
        public Material Material { get { return material; } }


        [SerializeField]
        private bool maskable = true;
        public bool Maskable { get { return maskable; } }


        [SerializeField]
        private float lineSpacing;
        public float LineSpacing { get { return lineSpacing; } }

        // TextMeshPro

        [SerializeField]
        private TMP_FontAsset textFont;
        public TMP_FontAsset TextFont { get { return textFont; } }


        [SerializeField]
        private FontStyles fontStyle = FontStyles.Normal;
        public FontStyles FontStyle { get { return fontStyle; } }


        [SerializeField]
        private float characterSpacing;
        public float CharacterSpacing { get { return characterSpacing; } }


        [SerializeField]
        private float wordSpacing;
        public float WordSpacing { get { return wordSpacing; } }


        [SerializeField]
        private float paragraphSpacing;
        public float ParagraphSpacing { get { return paragraphSpacing; } }


        [SerializeField]
        private HorizontalAlignmentOptions horizontalAlignment = HorizontalAlignmentOptions.Center;
        public HorizontalAlignmentOptions HorizontalAlignment { get { return horizontalAlignment; } }


        [SerializeField]
        private VerticalAlignmentOptions verticalAlignment = VerticalAlignmentOptions.Middle;
        public VerticalAlignmentOptions VerticalAlignment { get { return verticalAlignment; } }

        // Legacy text

        [SerializeField]
        private Font legacyTextFont;
        public Font LegacyTextFont { get { return legacyTextFont; } }


        [SerializeField]
        private FontStyle legacyFontStyle = UnityEngine.FontStyle.Normal;
        public FontStyle LegacyFontStyle { get { return legacyFontStyle; } }


        [SerializeField]
        private TextAnchor legacyAlignment = TextAnchor.MiddleCenter;
        public TextAnchor LegacyAlignment { get { return legacyAlignment; } }


        [SerializeField]
        private bool legacyAlignByGeometry;
        public bool LegacyAlignByGeometry { get { return legacyAlignByGeometry; } }

    }
}
