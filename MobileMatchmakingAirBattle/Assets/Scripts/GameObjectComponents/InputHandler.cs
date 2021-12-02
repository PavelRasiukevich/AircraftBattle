using UnityEngine;

namespace Assets.Scripts.GameObjectComponents
{
    public class InputHandler : MonoBehaviour
    {
        public float Horizontal { get; private set; }
        public float Vertical { get; private set; }

        public PlayersInput PInput { get; private set; }

        public Vector3 PlayersInput { get; private set; }

        private void Update()
        {
#if UNITY_EDITOR
            KeyBoardInput();
#else
            JoyStickInput();
#endif
        }

        private void KeyBoardInput()
        {
            Horizontal = Input.GetAxis("Horizontal");
            Vertical = Input.GetAxis("Vertical");


            PInput.SetHorizontal(Horizontal);
            PInput.SetVertical(Vertical);

            //Test version
            //optimize for reliable controlls
            PlayersInput = new Vector3(Horizontal, 0, Vertical);
        }

        private void JoyStickInput()
        {
        }
    }

    public struct PlayersInput
    {
        public float Horizontal { get; private set; }
        public float Vertical { get; private set; }

        public void SetHorizontal(float value) => Horizontal = value;

        public void SetVertical(float value) => Vertical = value;

    }
}