using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts.UI.JoyStick
{
    public class FireButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        public static bool IsFiring { get; private set; }

        private Action _act;

        public void OnPointerDown(PointerEventData eventData)
        {
            Debug.Log("OnPointerDown FireButton");
            IsFiring = true;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            Debug.Log("OnPointerUp FireButton");
            IsFiring = false;
        }

    }
}