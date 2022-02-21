namespace Assets.Scripts.Utils.Timers
{

    public class ReloadTimer : BaseTimer
    {
        public ReloadTimer(float value) : base(value)
        {
            Treshold = value;
            ElapsedTime = Treshold;
        }

        public override float ElapsedTime { get; protected set; }

        public override float Treshold { get; protected set; }

        public override bool IsStopped { get; protected set; }

        public void ChangeReloadTime(float newReloadTime)
        {
            Treshold = newReloadTime;
            ElapsedTime = Treshold;
        }

        public override void Tick(float tick)
        {
            if (IsStopped) return;

            ElapsedTime += tick;
            IsStopped = ElapsedTime >= Treshold;
        }

        public override void ResetTimer()
        {
            ElapsedTime = 0.0f;
            IsStopped = false;
        }
    }
}