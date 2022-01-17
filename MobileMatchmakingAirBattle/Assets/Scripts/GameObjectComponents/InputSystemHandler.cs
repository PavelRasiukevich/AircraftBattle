using Assets.Scripts.Structs;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Assets.Scripts.GameObjectComponents
{
    public class InputSystemHandler : MonoBehaviour
    {
        private InputActions _inputActions;
        private Vector2 _inputValues;

        public InputParameters InputParams { get; private set; }

        private void Awake()
        {
            _inputActions = new InputActions();
            InputParams = new InputParameters();
        }

        private void OnEnable()
        {
            _inputActions.PlayerActions.Enable();
            _inputActions.PlayerActions.Moves.performed += MovesHandler;
        }

        private void OnDisable()
        {
            _inputActions.PlayerActions.Disable();
            _inputActions.PlayerActions.Moves.performed -= MovesHandler;
        }

        private void Start()
        {

        }

        private void Update()
        {
            KeyboardInput();
        }

        private void KeyboardInput()
        {
            var tempInputParams = InputParams;
            _inputValues = _inputActions.PlayerActions.Moves.ReadValue<Vector2>();

            tempInputParams.Input = _inputValues;

            //delete after refactoring
            tempInputParams.IsStickPressed = Mathf.Abs(_inputValues.x) > 0 || Mathf.Abs(_inputValues.y) > 0;

            InputParams = tempInputParams;
        }

        #region Callbacks
        private void MovesHandler(InputAction.CallbackContext context)
        {

        }
        #endregion
    }
}