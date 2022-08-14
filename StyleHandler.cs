using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Stylization
{
    public abstract class StyleHandler<T> : MonoBehaviour where T : Style
    {
        [SerializeField]
        private T style;

        public T Style { get { return style; } }

        public bool UpdateComponents()
        {
            return UpdateComponents(true);
        }

        public bool TryUpdateComponents()
        {
            return UpdateComponents(false);
        }

        protected abstract bool UpdateComponents(bool debug);
    }
}
