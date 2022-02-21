namespace Assets.Scripts.Utils.Timers
{
    public abstract class BaseTimer
    {
        public abstract float ElapsedTime { get; protected set; }

        public abstract float Treshold { get; protected set; }

        public abstract bool IsStopped { get; protected set; }

        public BaseTimer(float value)
        {
            Treshold = value;
            ElapsedTime = Treshold;
        }

        public abstract void Tick(float tick);

        public abstract void ResetTimer();

    }
}