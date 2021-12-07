using UnityEngine;

namespace Assets.Scripts.GameObjectComponents
{
    public class MoveHandler : MonoBehaviour
    {
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

        public void MoveWithJoystickSimple(Rigidbody bodyToMove, Vector3 velocityVector, float speed)
            => bodyToMove.velocity = velocityVector * speed;

        public void MoveWithJoystickSimulated()
        {
        }
    }
}