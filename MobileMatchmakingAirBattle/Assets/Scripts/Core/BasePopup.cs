using UnityEngine;
using Utils.Enums;

namespace Assets.Scripts.Core
{
    public abstract class BasePopup : MonoBehaviour
    {
        public abstract PopupType Type { get; }

        public virtual BasePopup Config(string text) => this;

        public virtual BasePopup Config() => this;

        public virtual void Show() => gameObject.SetActive(true);

        public virtual void Hide() => gameObject.SetActive(false);
    }
}