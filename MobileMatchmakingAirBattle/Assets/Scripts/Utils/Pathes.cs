using UnityEngine;

namespace Assets.Scripts.Utils
{
    [CreateAssetMenu(fileName = "Pathes", menuName = "Pathes/Create")]
    public class Pathes : ScriptableObject
    {
        public string AudioAssetsPath;
    }

    public enum AssetTypeFilter
    {
        AudioClip,
    }
}