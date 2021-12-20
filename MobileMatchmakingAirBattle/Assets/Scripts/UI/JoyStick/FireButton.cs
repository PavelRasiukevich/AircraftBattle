using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts.UI.JoyStick
{
    public class FireButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        public static bool IsFire { get; set; }

        public void OnPointerDown(PointerEventData eventData)
        {
            IsFire = true;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            IsFire = false;
        }
    }
}