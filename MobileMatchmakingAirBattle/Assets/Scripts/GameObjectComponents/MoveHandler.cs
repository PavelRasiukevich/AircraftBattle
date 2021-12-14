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
            gasCoefficient = Mathf.Clamp(gasCoefficient, 0.5f, 1.0f);

            var angularVelocity = new Vector3(joystickInput.y, 0.0f, joystickInput.x * -1);

            if (gasPressed)
            {
                gasCoefficient += 0.01f;

                Quaternion rotation = Quaternion.Euler(angularVelocity);
                bodyToMove.MoveRotation(bodyToMove.rotation * rotation);
            }
            else
            {
                gasCoefficient -= 0.01f;

                FreeFall(bodyToMove);

                var returnRotation = Quaternion.Euler(new Vector3(0.0f, bodyToMove.rotation.eulerAngles.y, 0.0f));
                bodyToMove.rotation = Quaternion.Slerp(bodyToMove.rotation, returnRotation, Time.fixedDeltaTime);
            }

            bodyToMove.MovePosition(bodyToMove.position + transform.TransformDirection(gasCoefficient * speed.MoveSpeed * Vector3.forward));
        }

        public void MoveUncontrollable(Rigidbody bodyToMove, Speed speed)
            => bodyToMove.MovePosition(transform.position + transform.TransformDirection(speed.MoveSpeed * Vector3.forward));

        #region Utilities
        private void FreeFall(Rigidbody bodyToMove)
        {
            var bodyPosition = bodyToMove.position;
            bodyPosition.y -= 0.1f;
            bodyToMove.position = bodyPosition;
        }
        #endregion
    }
}