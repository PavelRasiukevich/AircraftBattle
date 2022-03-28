using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class AssetDatabaseExample : MonoBehaviour
{
#if UNITY_EDITOR
    [MenuItem("AssetDatabase/Refresh Example")]
    public static void RefreshExample()
    {
        var folderList = new List<string> { "Textures", "Models", "Sounds" };
        foreach (var folder in folderList)
        {
            Directory.CreateDirectory($"Assets/{folder}");
        }

        foreach (var folder in folderList)
        {
            //Output will be false
            Debug.Log(AssetDatabase.IsValidFolder($"Assets/{folder}"));
        }

        AssetDatabase.Refresh();
        foreach (var folder in folderList)
        {
            //Output will be true
            Debug.Log(AssetDatabase.IsValidFolder($"Assets/{folder}"));
        }
    }
#endif
}