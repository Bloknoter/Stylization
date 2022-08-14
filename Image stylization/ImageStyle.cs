using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Stylization
{
    [CreateAssetMenu(fileName = "New image style", menuName = "Styles/Image style")]
    public class ImageStyle : Style
    {
        [SerializeField]
        private Sprite sprite;

        public Sprite Sprite { get { return sprite; } }

        [SerializeField]
        private Material material;

        public Material Material { get { return material; } }


        [SerializeField]
        private Color color = Color.white;

        public Color Color { get { return color; } }

        [SerializeField]
        private bool maskable = true;

        public bool Maskable { get { return maskable; } }
    }
}
