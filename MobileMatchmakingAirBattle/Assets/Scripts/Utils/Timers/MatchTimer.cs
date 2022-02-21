namespace Assets.Scripts.Utils.Timers
{
    public class MatchTimer : BaseTimer
    {
        public MatchTimer(float value) : base(value)
        {
            TimeAmmount = value;
            Treshold = 0;
        }

        public float TimeAmmount { get; protected set; }
        public override float ElapsedTime { get; protected set; }
        public override float Treshold { get; protected set; }
        public override bool IsStopped { get; protected set; }

        public override void Tick(float tick)
        {
            if (IsStopped) return;

            TimeAmmount -= tick;

            IsStopped = TimeAmmount <= Treshold;
        }

        public override void ResetTimer()
        {
            ElapsedTime = 0.0f;
            IsStopped = false;
        }
    }
}