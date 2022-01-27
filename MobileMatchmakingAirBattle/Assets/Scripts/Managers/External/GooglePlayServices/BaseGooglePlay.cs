using Managers.External.GooglePlayServices.Services;

namespace Managers.External.GooglePlayServices
{
    public class BaseGooglePlay
    {
        public BaseGooglePlayAchievements Achievements { get; } = new BaseGooglePlayAchievements();
        public BaseGooglePlayAuthenticate Authenticate { get; } = new BaseGooglePlayAuthenticate();
    }
}