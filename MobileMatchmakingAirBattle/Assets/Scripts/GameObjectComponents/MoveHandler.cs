using UnityEngine;

namespace Assets.Scripts.GameObjectComponents
{
    public class MoveHandler : MonoBehaviour
    {
        private float _gasCoefficient = 1.0f;
        private Vector3 _angularVelocity;

        Transform

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
            _gasCoefficient = Mathf.Clamp(_gasCoefficient, 0.5f, 1.0f);
            _angularVelocity = new Vector3(joystickInput.y, 0.0f, joystickInput.x * -1);

            #region Old
            /* if (gasPressed)
             {
                 gasCoefficient += 0.01f;

                 Quaternion rotation = Quaternion.Euler(_angularVelocity);
                 bodyToMove.MoveRotation(bodyToMove.rotation * rotation);
             }
             else
             {
                 gasCoefficient -= 0.01f;

                 FreeFall(bodyToMove);

                 var returnRotation = Quaternion.Euler(new Vector3(0.0f, bodyToMove.rotation.eulerAngles.y, 0.0f));
                 bodyToMove.rotation = Quaternion.Lerp(bodyToMove.rotation, returnRotation, Time.fixedDeltaTime);
             }*/
            #endregion

            #region New

            bodyToMove.angularVelocity = ClampVelocity(bodyToMove);

            if (gasPressed)
            {

                bodyToMove.AddRelativeTorque(Vector3.right, ForceMode.VelocityChange);

                //RestrictRotation(bodyToMove);
            }
            else
            {
                bodyToMove.angularVelocity = Vector3.Lerp(bodyToMove.angularVelocity * 0.01f, Vector3.zero, 0.5f);
            }

            #endregion

            bodyToMove.velocity = transform.TransformDirection(_gasCoefficient * speed.MoveSpeed * Vector3.forward);
        }

        private void RestrictRotation(Rigidbody bodyToMove)
        {
            var rotY = bodyToMove.rotation.eulerAngles;
            rotY.y = 0.0f;
            bodyToMove.rotation = Quaternion.Euler(rotY);

            /*if (bodyToMove.rotation.eulerAngles.z >= 45 && bodyToMove.rotation.eulerAngles.z <= 315)
            {
                var angVelZ = bodyToMove.angularVelocity;
                angVelZ.z = 0;
                bodyToMove.angularVelocity = angVelZ;
            }

            if (bodyToMove.rotation.eulerAngles.x >= 315 && bodyToMove.rotation.eulerAngles.x <= 45)
            {
                var angVelX = bodyToMove.angularVelocity;
                angVelX.x = 0;
                bodyToMove.angularVelocity = angVelX;
            }*/
        }

        private static Vector3 ClampVelocity(Rigidbody bodyToMove)
        {
            var angVel = bodyToMove.angularVelocity;

            angVel.z = Mathf.Clamp(angVel.z, -1.0f, 1.0f);
            angVel.x = Mathf.Clamp(angVel.x, -1.0f, 1.0f);
            angVel.y = Mathf.Clamp(angVel.y, 0.0f, 0.0f);

            return angVel;
        }

        public void MoveUncontrollable(Rigidbody bodyToMove, Speed speed)
            => bodyToMove.velocity = transform.TransformDirection(_gasCoefficient * speed.MoveSpeed * Vector3.forward);

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