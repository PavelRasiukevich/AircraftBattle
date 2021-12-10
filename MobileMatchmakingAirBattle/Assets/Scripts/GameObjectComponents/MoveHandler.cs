using UnityEngine;

namespace Assets.Scripts.GameObjectComponents
{
    public class MoveHandler : MonoBehaviour
    {
        private float gasCoefficient = 1.0f;

        #region NOT USED
        /// <summary>
        /// Move using lag compensated position.
        /// </summary>
        /// <param name="remoteBody"></param>
        /// <param name="networkPosition"></param>
        public void MoveCompensate(Rigidbody remoteBody, Vector3 networkPosition)
            => remoteBody.position = Vector3.Lerp(remoteBody.position, networkPosition, Time.fixedDeltaTime);

        public void MoveWithVelocity(Rigidbody bodyToMove, Vector3 velocityVector, float speed)
            => bodyToMove.velocity = velocityVector * speed;

        public void MoveWithInputAxes(Rigidbody bodyToMove, InputHandler handler)
            => bodyToMove.MovePosition(bodyToMove.position + new Vector3(handler.Horizontal, 0, handler.Vertical));
        #endregion

        public void MoveWithJoyStick(Rigidbody bodyToMove, Vector2 joystickInput, Speed speed, bool gasPressed)
        {
            gasCoefficient = Mathf.Clamp(gasCoefficient, 0.25f, 1.0f);

            if (gasPressed)
            {
                gasCoefficient += 0.01f;
            }
            else
            {
                gasCoefficient -= 0.01f;
            }

            var value_X = joystickInput.x;
            var value_Z = joystickInput.y;

            var rotation = new Vector3(value_Z, 0.0f, value_X * -1);
            var deltaRotation = Quaternion.Euler(rotation * Time.fixedDeltaTime * speed.RotationSpeed);

            bodyToMove.MoveRotation(bodyToMove.rotation * deltaRotation);

            bodyToMove.MovePosition(bodyToMove.position + transform.InverseTransformDirection(Vector3.forward * speed.MoveSpeed * gasCoefficient));
        }

        public void MoveUncontrollable(Rigidbody bodyToMove, Speed speed)
            => bodyToMove.MovePosition(transform.position + transform.InverseTransformDirection(Vector3.forward * speed.MoveSpeed * gasCoefficient));
    }
}