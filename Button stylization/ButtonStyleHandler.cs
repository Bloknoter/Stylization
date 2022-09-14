using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Stylization
{
    [AddComponentMenu("Stylization/Button style handler")]
    public class ButtonStyleHandler : StyleHandler<ButtonStyle>
    {
        [SerializeField]
        private Image buttonBG;

        public Image ButtonBG { get { return buttonBG; } }

        [SerializeField]
        private TextMeshProUGUI buttonText;

        public TextMeshProUGUI ButtonText { get { return buttonText; } }

        [SerializeField]
        private Text legacyButtonText;

        public Text LegacyButtonText { get { return legacyButtonText; } }

        private void Reset()
        {
            buttonBG = GetComponent<Image>();
            if (transform.childCount > 0)
            {
                buttonText = GetComponentInChildren<TextMeshProUGUI>();
                if (buttonText == null)
                    legacyButtonText = GetComponentInChildren<Text>();
            }
        }

        protected override bool UpdateComponents(bool debug)
        {
            if (buttonBG != null)
            {
                buttonBG.sprite = Style.BGSprite;
                buttonBG.material = Style.BGMaterial;
                buttonBG.color = Style.BGColor;
                buttonBG.maskable = Style.Maskable;
            }
            else
            {
                if (debug)
                {
                    Debug.LogError("Background field is null. Please, assign this field", this);
                    return false;
                }
            }

            if (Style.HasText)
            {
                if (Style.TextType == TextTypes.TextMeshPro)
                {
                    if (buttonText != null)
                    {
                        if (Style.StaticText)
                            buttonText.text = Style.Text;
                        buttonText.fontSize = Style.FontSize;
                        buttonText.color = Style.Foreground;
                        buttonText.material = Style.Material;
                        buttonText.maskable = Style.TextMaskable;
                        if (Style.TextFont != null)
                            buttonText.font = Style.TextFont;
                        buttonText.fontStyle = Style.FontStyle;
                        buttonText.characterSpacing = Style.CharacterSpacing;
                        buttonText.wordSpacing = Style.WordSpacing;
                        buttonText.lineSpacing = Style.LineSpacing;
                        buttonText.paragraphSpacing = Style.ParagraphSpacing;
                        buttonText.horizontalAlignment = Style.HorizontalAlignment;
                        buttonText.verticalAlignment = Style.VerticalAlignment;
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
                    if (legacyButtonText != null)
                    {
                        if (Style.StaticText)
                            legacyButtonText.text = Style.Text;
                        legacyButtonText.fontSize = (int)Style.FontSize;
                        legacyButtonText.color = Style.Foreground;
                        legacyButtonText.material = Style.Material;
                        legacyButtonText.maskable = Style.TextMaskable;
                        if (Style.LegacyTextFont != null)
                            legacyButtonText.font = Style.LegacyTextFont;
                        legacyButtonText.fontStyle = Style.LegacyFontStyle;
                        legacyButtonText.lineSpacing = Style.LineSpacing;
                        legacyButtonText.alignment = Style.LegacyAlignment;
                        legacyButtonText.alignByGeometry = Style.LegacyAlignByGeometry;
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
                else if (Style.TextType == TextTypes.Mixed)
                {
                    bool has = false;
                    if (buttonText != null)
                    {
                        has = true;
                        if (Style.StaticText)
                            buttonText.text = Style.Text;
                        buttonText.fontSize = Style.FontSize;
                        buttonText.color = Style.Foreground;
                        buttonText.material = Style.Material;
                        buttonText.maskable = Style.TextMaskable;
                        buttonText.lineSpacing = Style.LineSpacing;
                    }
                    if (legacyButtonText != null)
                    {
                        has = true;
                        if (Style.StaticText)
                            legacyButtonText.text = Style.Text;
                        legacyButtonText.fontSize = (int)Style.FontSize;
                        legacyButtonText.color = Style.Foreground;
                        legacyButtonText.material = Style.Material;
                        legacyButtonText.maskable = Style.Maskable;
                        legacyButtonText.lineSpacing = Style.LineSpacing;
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
            }
            return true;
        }

        [ContextMenu("Clear data")]
        private void ClearComponentsData()
        {
            buttonBG = null;
            buttonText = null;
            legacyButtonText = null;
        }

    }

}
