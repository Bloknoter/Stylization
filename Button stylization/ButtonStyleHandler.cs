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
                if(Style.TextType == ButtonStyle.TextTypes.Legacy)
                {
                    if (legacyButtonText != null)
                    {
                        if (Style.LegacyTextFont != null)
                            legacyButtonText.font = Style.LegacyTextFont;
                        legacyButtonText.fontSize = (int)Style.FontSize;
                        legacyButtonText.color = Style.Foreground;
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
                else if(Style.TextType == ButtonStyle.TextTypes.TextMeshPro)
                {
                    if (buttonText != null)
                    {
                        if (Style.TextFont != null)
                            buttonText.font = Style.TextFont;
                        buttonText.fontSize = Style.FontSize;
                        buttonText.color = Style.Foreground;
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
                else if(Style.TextType == ButtonStyle.TextTypes.Mixed)
                {
                    bool has = false;
                    if(buttonText != null)
                    {
                        has = true;
                        buttonText.fontSize = Style.FontSize;
                        buttonText.color = Style.Foreground;
                    }
                    if (legacyButtonText != null)
                    {
                        has = true;
                        legacyButtonText.fontSize = (int)Style.FontSize;
                        legacyButtonText.color = Style.Foreground;
                    }
                    if(!has)
                    {
                        if (debug)
                        {
                            Debug.LogError("None of Text fields is assigned. Please, assign any of this fields", this);
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
