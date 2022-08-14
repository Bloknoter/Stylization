using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Stylization
{
    [CreateAssetMenu(fileName = "New slider style", menuName = "Styles/Slider style")]
    public class SliderStyle : Style
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


        // Fill

        [SerializeField]
        private Sprite fillSprite;

        public Sprite FillSprite { get { return fillSprite; } }

        [SerializeField]
        private Material fillMaterial;

        public Material FillMaterial { get { return fillMaterial; } }


        [SerializeField]
        private Color fillColor = Color.white;

        public Color FillColor { get { return fillColor; } }

        [SerializeField]
        private bool fillMaskable = true;

        public bool FillMaskable { get { return fillMaskable; } }


        // Handle

        [SerializeField]
        private bool hasHandle;

        public bool HasHandle { get { return hasHandle; } }

        [SerializeField]
        private Sprite handleSprite;

        public Sprite HandleSprite { get { return handleSprite; } }

        [SerializeField]
        private Material handleMaterial;

        public Material HandleMaterial { get { return handleMaterial; } }


        [SerializeField]
        private Color handleColor = Color.white;

        public Color HandleColor { get { return handleColor; } }

        [SerializeField]
        private bool handleMaskable = true;

        public bool HandleMaskable { get { return handleMaskable; } }
    }
}
