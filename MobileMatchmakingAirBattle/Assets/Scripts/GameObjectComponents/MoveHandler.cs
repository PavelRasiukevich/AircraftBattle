using UnityEngine;

namespace Assets.Scripts.GameObjectComponents
{
    public class MoveHandler
    {

        public void MoveRemote(Rigidbody remoteBody, Vector3 networkPosition)
            => remoteBody.position = Vector3.MoveTowards(remoteBody.position, networkPosition, Time.fixedDeltaTime);

        public void Move(Rigidbody bodyToMove, InputHandler handler)
            => bodyToMove.velocity = handler.Velocity;
    }
}