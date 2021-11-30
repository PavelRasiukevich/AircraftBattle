using UnityEngine;
using Utils.Enums;

namespace Assets.Scripts.Core
{
    public abstract class BaseScreen : MonoBehaviour
    {
        public abstract ScreenType Type { get; }

        public void ShowScreen() => gameObject.SetActive(true);

        public void HideScreen() => gameObject.SetActive(false);
    }
}