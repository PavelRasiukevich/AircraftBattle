using Assets.Scripts.Structs;
using TO;
using UnityEngine;

namespace Assets.Scripts.GameObjectComponents
{
    public class MoveHandler : MonoBehaviour
    {
        private float _gasCoefficient = 1.0f;
        private Vector3 _angularVelocity;

        private Quaternion _returnRotation;
        private Vector3 _resetedReturnAngle;

        public void MoveWithJoyStick(Rigidbody bodyToMove, InputParameters inputParams, Speed speed)
        {

            _gasCoefficient = Mathf.Clamp(_gasCoefficient, 0.1f, 1.0f);

            _angularVelocity.x = inputParams.Input.y;
            _angularVelocity.y = 0.0f;
            _angularVelocity.z = inputParams.Input.x * -1;

            if (inputParams.IsStickPressed)
            {

                _gasCoefficient += 0.01f;

                Quaternion rotation = Quaternion.Euler(_angularVelocity * speed.RotationSpeed);
                bodyToMove.MoveRotation(bodyToMove.rotation * rotation);
            }
            else
            {
                _gasCoefficient -= 0.001f;

                FreeFall(bodyToMove);

                _resetedReturnAngle.x = 0.0f;
                _resetedReturnAngle.y = bodyToMove.rotation.eulerAngles.y;
                _resetedReturnAngle.z = 0.0f;

                _returnRotation = Quaternion.Euler(_resetedReturnAngle);
                bodyToMove.rotation = Quaternion.Lerp(bodyToMove.rotation, _returnRotation, Time.fixedDeltaTime);
            }

            bodyToMove.velocity = transform.TransformDirection(_gasCoefficient * speed.MoveSpeed * Vector3.forward);
        }

        private void Move(InputParameters inputParams, Speed speed)
        {

        }

        public void MoveUncontrollable(Rigidbody bodyToMove, Speed speed)
            => bodyToMove.velocity = transform.TransformDirection(speed.MoveSpeed * Vector3.forward);

        #region Utilities
        private void FreeFall(Rigidbody bodyToMove)
        {
        }
        #endregion
    }
}