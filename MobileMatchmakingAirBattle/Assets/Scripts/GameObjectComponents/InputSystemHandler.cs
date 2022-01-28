using Assets.Scripts.Structs;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

namespace Assets.Scripts.GameObjectComponents
{
    public class InputSystemHandler : MonoBehaviour
    {
        private Actions _actions;
        private InputAction _moveAction;
        private ButtonControl _buttonControl;

        private Vector2 _inputValues;

        public InputParameters InputParams { get; private set; }
        public bool IsCenterPressed { get; private set; }


        #region EVENTS

        public delegate void Attack();
        public event Attack Attacking;

        #endregion

        private void Awake()
        {
            _actions = new Actions();
            _moveAction = _actions.PlayerActions.Moves;
            _buttonControl = Gamepad.current.leftStickButton;

            InputParams = new InputParameters();
        }

        private void OnEnable()
        {
            _actions.PlayerActions.Enable();
        }

        private void OnDisable()
        {
            _actions.PlayerActions.Disable();
        }

        private void Update()
        {
            if (_actions.PlayerActions.Fire.inProgress)
                Attacking?.Invoke();

            IsCenterPressed = _buttonControl.isPressed;

            print($"InputHandlerUpdate - {IsCenterPressed}");

            KeyboardInput();
        }

        private void KeyboardInput()
        {
            var tempInputParams = InputParams;

            _inputValues = _moveAction.ReadValue<Vector2>();

            tempInputParams.Input = _inputValues;
            tempInputParams.IsStickPressed = IsCenterPressed;

            InputParams = tempInputParams;
        }

        #region Callbacks
        #endregion
    }
}