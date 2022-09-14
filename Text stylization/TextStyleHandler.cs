using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Stylization
{
    [AddComponentMenu("Stylization/Text style handler")]
    public class TextStyleHandler : StyleHandler<TextStyle>
    {
        [SerializeField]
        private TextMeshProUGUI text;
        public TextMeshProUGUI Text { get { return text; } }


        [SerializeField]
        private Text legacyText;
        public Text LegacyText { get { return legacyText; } }


        private void Reset()
        {
            text = GetComponent<TextMeshProUGUI>();
            if (text == null)
                legacyText = GetComponent<Text>();
        }

        protected override bool UpdateComponents(bool debug)
        {
            if (Style.TextType == TextTypes.TextMeshPro)
            {
                if (text != null)
                {
                    if (Style.StaticText)
                        text.text = Style.Text;
                    text.fontSize = Style.FontSize;
                    text.color = Style.Foreground;
                    text.material = Style.Material;
                    text.maskable = Style.Maskable;
                    if (Style.TextFont != null)
                        text.font = Style.TextFont;
                    text.fontStyle = Style.FontStyle;
                    text.characterSpacing = Style.CharacterSpacing;
                    text.wordSpacing = Style.WordSpacing;
                    text.lineSpacing = Style.LineSpacing;
                    text.paragraphSpacing = Style.ParagraphSpacing;
                    text.horizontalAlignment = Style.HorizontalAlignment;
                    text.verticalAlignment = Style.VerticalAlignment;
                }
                else
                {
                    if (debug)
                    {
                        Debug.LogError("Text field is null. Please, assign this field", this);
                        return false;
                    }
                }
            }
            else if (Style.TextType == TextTypes.Legacy)
            {
                if (legacyText != null)
                {
                    if (Style.StaticText)
                        legacyText.text = Style.Text;
                    legacyText.fontSize = (int)Style.FontSize;
                    legacyText.color = Style.Foreground;
                    legacyText.material = Style.Material;
                    legacyText.maskable = Style.Maskable;
                    if (Style.LegacyTextFont != null)
                        legacyText.font = Style.LegacyTextFont;
                    legacyText.fontStyle = Style.LegacyFontStyle;
                    legacyText.lineSpacing = Style.LineSpacing;
                    legacyText.alignment = Style.LegacyAlignment;
                    legacyText.alignByGeometry = Style.LegacyAlignByGeometry;
                }
                else
                {
                    if (debug)
                    {
                        Debug.LogError("Text field is null. Please, assign this field", this);
                        return false;
                    }
                }
            }
            else if(Style.TextType == TextTypes.Mixed)
            {
                bool has = false;
                if (text != null)
                {
                    has = true;
                    if (Style.StaticText)
                        text.text = Style.Text;
                    text.fontSize = Style.FontSize;
                    text.color = Style.Foreground;
                    text.material = Style.Material;
                    text.maskable = Style.Maskable;
                    text.lineSpacing = Style.LineSpacing;
                }
                if (legacyText != null)
                {
                    has = true;
                    if (Style.StaticText)
                        legacyText.text = Style.Text;
                    legacyText.fontSize = (int)Style.FontSize;
                    legacyText.color = Style.Foreground;
                    legacyText.material = Style.Material;
                    legacyText.maskable = Style.Maskable;
                    legacyText.lineSpacing = Style.LineSpacing;
                }
                if (!has)
                {
                    if (debug)
                    {
                        Debug.LogError("None of Text fields is assigned. Please, assign any of this fields", this);
                        return false;
                    }
                }
            }
            return true;
        }

        [ContextMenu("Clear data")]
        private void ClearComponentsData()
        {
            text = null;
            legacyText = null;
        }
    }
}
