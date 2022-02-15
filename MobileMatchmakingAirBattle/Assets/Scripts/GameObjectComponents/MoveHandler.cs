using Assets.Scripts.Interfaces;
using Assets.Scripts.Structs;
using TO;
using UnityEngine;

namespace Assets.Scripts.GameObjectComponents
{
    public class MoveHandler : MonoBehaviour, IReturnToBattle
    {
        public Transform View { get; set; }
        public Rigidbody Body { get; set; }

        private Quaternion _returnRotation;
        private Vector3 _resetedReturnAngle;

        public void Pilot(in InputParameters inputValues, in Properties props)
        {
            Body.velocity = View.forward * props.MoveSpeed;

            Rotate(inputValues, props);
        }

        private void Rotate(in InputParameters inputValues, in Properties props)
        {
            var rotationAroundYInWorldSpace = inputValues.Input.x * props.RotationSpeed * Vector3.up;
            Body.angularVelocity = rotationAroundYInWorldSpace * Mathf.Deg2Rad;

            if (IsInputPerformed(inputValues))
            {
                Quaternion viewTargetRotation = Quaternion.Euler(inputValues.Input.y, 0, inputValues.Input.x * -1);
                View.rotation *= viewTargetRotation;
            }
            else
            {
                _resetedReturnAngle = ResetReturnAngleValues(_resetedReturnAngle);
                _returnRotation = Quaternion.Euler(_resetedReturnAngle);
                View.rotation = Quaternion.Slerp(View.rotation, _returnRotation, Time.fixedDeltaTime * props.ReturnSpeed);
            }
        }

        public void DragToBattleField(Properties speed)
            => Body.velocity = transform.TransformDirection(speed.MoveSpeed * Vector3.forward);

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

        void IReturnToBattle.Return()
        {
            var angle = Body.rotation.eulerAngles + 180 * Vector3.up;
            Body.transform.eulerAngles = Vector3.Slerp(Body.transform.eulerAngles, angle, 180);
        }

        #endregion
    }
}