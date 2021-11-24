using UnityEngine;

namespace Assets.Scripts.GameObjectComponents
{
    public class InputHandler : MonoBehaviour
    {
        public float Horizontal { get; private set; }
        public float Vertical { get; private set; }

        public Vector3 Velocity { get; set; }

        private void Update()
        {
            DefineInput();
        }

        private void DefineInput()
        {
            Horizontal = Input.GetAxis("Horizontal");
            Vertical = Input.GetAxis("Vertical");

            //Velocity = //todo velocity
        }
    }
}