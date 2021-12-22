using Assets.Scripts.Structs;
using Assets.Scripts.UI.JoyStick;
using UnityEngine;

namespace Assets.Scripts.GameObjectComponents
{
    public class InputHandler : MonoBehaviour
    {
        public float Horizontal { get; private set; }
        public float Vertical { get; private set; }

        public InputParameters InputParams { get; private set; }

        private Vector2 PlayersInput { get; set; }

        private void Awake()
        {
            InputParams = new InputParameters();
        }

        private void Update()
        {
#if UNITY_EDITOR
            KeyBoardInput();
#else
            JoyStickInput();
#endif
        }

        private void KeyBoardInput()
        {

            Horizontal = Input.GetAxis("Horizontal");
            Vertical = Input.GetAxis("Vertical");

            PlayersInput = new Vector2(Horizontal, Vertical);

            var par = InputParams;
            par.Input = PlayersInput;
            par.IsStickPressed = Mathf.Abs(Horizontal) > 0 || Mathf.Abs(Vertical) > 0;

            InputParams = par;

        }

        private void JoyStickInput()
        {
            var intermediate = InputParams;

            intermediate.Input = JoyStick.JoystickInput;
            intermediate.IsStickPressed = JoyStick.IsPressed;

            InputParams = intermediate;
        }
    }
}