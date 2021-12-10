using Network.External.GooglePlayServices.Services;

namespace Network.External.GooglePlayServices
{
    public class BaseGooglePlay
    {
        public BaseGooglePlayAchievements Achievements { get; set; } 

        public BaseGooglePlayAuthenticate Authenticate { get; set; }

        public BaseGooglePlay()
        {
            Achievements = new BaseGooglePlayAchievements();
            Authenticate = new BaseGooglePlayAuthenticate();
        }
    }
}