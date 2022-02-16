using ExitGames.Client.Photon;
using Photon.Realtime;

namespace Utils.Extensions
{
    public static class PhotonPlayerExtension
    {
        public static T GetPropertyValue<T>(this Player player, string property, T defaultValue)
        {
            if (player.CustomProperties.TryGetValue(property, out var value))
                return (T) value;

            return defaultValue;
        }

        public static void SetPropertyValue<T>(this Player player, string property, T value)
        {
            player.SetCustomProperties(new Hashtable {{property, value}});
        }

        public static void AddValueToProperty(this Player player, string property, int value)
        {
            var defaultValue = GetPropertyValue(player, property, 0);
            defaultValue += value;

            player.SetCustomProperties(new Hashtable {{property, defaultValue}});
        }

        public static void DeleteProperty(this Player player, string property)
        {
            player.SetCustomProperties(new Hashtable {{property, null}});
        }
    }
}