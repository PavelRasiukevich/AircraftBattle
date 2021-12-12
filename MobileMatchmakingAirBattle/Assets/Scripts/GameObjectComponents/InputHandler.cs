using Assets.Scripts.UI.JoyStick;
using UnityEngine;

namespace Assets.Scripts.GameObjectComponents
{
    public class InputHandler : MonoBehaviour
    {
        public float Horizontal { get; private set; }
        public float Vertical { get; private set; }

        public Vector2 PlayersInput { get; private set; }

        private void Update()
        {
            JoyStickInput();
        }

        /*   private void KeyBoardInput()
           {
               Horizontal = Input.GetAxis("Horizontal");
               Vertical = Input.GetAxis("Vertical");

               PlayersInput = new Vector3(Horizontal, 0, Vertical);
           }*/

        private void JoyStickInput() => PlayersInput = JoyStick.JoystickInput;
    }
}