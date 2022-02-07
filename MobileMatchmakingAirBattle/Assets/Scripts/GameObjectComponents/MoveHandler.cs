using Assets.Scripts.Structs;
using TO;
using UnityEngine;

namespace Assets.Scripts.GameObjectComponents
{
    public class MoveHandler : MonoBehaviour
    {
        public Transform View { get; set; }

        private Quaternion _returnRotation;
        private Vector3 _resetedReturnAngle;

        public void Pilot(Rigidbody bodyToMove, InputParameters inputValues, Speed speed)
        {
            bodyToMove.velocity = View.forward * speed.MoveSpeed;

            RotatePlane(bodyToMove, inputValues, speed);
        }

        public void DragToBattleField(Rigidbody bodyToMove, Speed speed)
            => bodyToMove.velocity = transform.TransformDirection(speed.MoveSpeed * Vector3.forward);

        private void RotatePlane(Rigidbody bodyToRotate, InputParameters inputValues, Speed speed)
        {

            if (IsInputPerformed(inputValues))
            {
                Quaternion targetRotation = Quaternion.Euler(Vector3.up * inputValues.Input.x);
                bodyToRotate.MoveRotation(bodyToRotate.rotation * targetRotation);

                Quaternion viewTargetRotation = Quaternion.Euler(inputValues.Input.y, 0, inputValues.Input.x * -1);
                View.rotation *= viewTargetRotation;
            }
            else
            {
                _resetedReturnAngle = ResetReturnAngleValues(_resetedReturnAngle);
                _returnRotation = Quaternion.Euler(_resetedReturnAngle);
                View.rotation = Quaternion.Slerp(View.rotation, _returnRotation, Time.fixedDeltaTime * speed.RotationSpeed);
            }
        }

        #region Utilities

        private Vector3 ResetReturnAngleValues(Vector3 vector)
        {
            vector.x = View.rotation.eulerAngles.x;
            vector.y = View.rotation.eulerAngles.y;
            vector.z = 0.0f;

            return vector;
        }

        private bool IsInputPerformed(InputParameters values)
            => Mathf.Abs(values.Input.x) > 0 || Mathf.Abs(values.Input.y) > 0;

        #endregion
    }
}