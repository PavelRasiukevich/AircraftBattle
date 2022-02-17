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
        private int _fireButtonValue;

        public InputParameters InputParams { get; private set; }


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
            KeyboardInput();
        }

        private void KeyboardInput()
        {
            var tempInputParams = InputParams;

            _inputValues = _moveAction.ReadValue<Vector2>();

            tempInputParams.Input = _inputValues;
            tempInputParams.HasFire = _actions.PlayerActions.Fire.inProgress;
            tempInputParams.HasMove = _actions.PlayerActions.Moves.inProgress;

            InputParams = tempInputParams;

        }
    }
}