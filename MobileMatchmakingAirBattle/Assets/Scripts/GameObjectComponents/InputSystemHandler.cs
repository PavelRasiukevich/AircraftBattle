using Assets.Scripts.Structs;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Assets.Scripts.GameObjectComponents
{
    public class InputSystemHandler : MonoBehaviour
    {
        private Actions _actions;
        private InputAction _moveAction;

        private Vector2 _inputValues;
        private float _fireButtonValue;

        public InputParameters InputParams { get; private set; }

        #region EVENTS

        public delegate void Attack();

        public event Attack Attacking;

        #endregion

        private void Awake()
        {
            _actions = new Actions();
            _moveAction = _actions.PlayerActions.Moves;

            InputParams = new InputParameters();
        }

        private void OnEnable()
        {
            _actions.PlayerActions.Enable();
            _moveAction.Enable();
        }

        private void OnDisable()
        {
            _actions.PlayerActions.Disable();
            _moveAction.Disable();
        }

        private void Update()
        {
            /*if (_actions.PlayerActions.Fire.inProgress)
                Attacking?.Invoke();*/

            KeyboardInput();
        }

        private void KeyboardInput()
        {
            var tempInputParams = InputParams;

            _inputValues = _moveAction.ReadValue<Vector2>();

            if (_actions.PlayerActions.Fire.phase == InputActionPhase.Performed)
            {
                _fireButtonValue = _actions.PlayerActions.Fire.ReadValue<float>();
            }

            tempInputParams.Input = _inputValues;
            tempInputParams.IsFiring = _fireButtonValue != 0;

            InputParams = tempInputParams;

        }
    }
}