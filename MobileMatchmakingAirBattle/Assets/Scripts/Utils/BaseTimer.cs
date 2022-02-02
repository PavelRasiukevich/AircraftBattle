namespace Assets.Scripts.Utils
{
    public class BaseTimer
    {
        public float ElapsedTime { get; private set; }

        public float Treshold { get; private set; }

        public bool IsTimerStoped { get; private set; }

        public BaseTimer()
        {
        }

        public BaseTimer(float value)
        {
            Treshold = value;
            ElapsedTime = Treshold;
        }

        public void Tick(float tick)
        {
            if (IsTimerStoped) return;

            ElapsedTime += tick;

            IsTimerStoped = ElapsedTime >= Treshold;
        }

        /// <summary>
        /// Test method for research porposes
        /// </summary>
        /// <param name="tick">Time step</param>
        public void StartTimer(float tick)
        {
        }

        public void ResetTimer()
        {
            ElapsedTime = 0.0f;
            IsTimerStoped = false;
        }
    }
}