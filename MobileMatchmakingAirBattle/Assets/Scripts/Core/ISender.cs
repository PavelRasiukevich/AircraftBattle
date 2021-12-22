namespace Assets.Scripts.Core
{
    public interface ISender
    {
        public delegate void HealthSender(float value, float max);
        public event HealthSender Notify;
    }
}