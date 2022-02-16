using System.IO;
using UnityEngine;

namespace Utils
{
    public static class Data
    {
        #region PlayerPrefs

        public static void SetInt(string key, int value) => PlayerPrefs.SetInt(key, value);

        public static int GetInt(string key) => PlayerPrefs.GetInt(key);

        #endregion

        #region ToFile

        public static void Set(string key, object obj) => SaveToFile(key, JsonUtility.ToJson(obj, true));

        public static T Get<T>(string key) => JsonUtility.FromJson<T>(ReadFromFile(key));

        public static bool IsExists(string key) => File.Exists(FilePath(key));

        public static void Delete(string key) => File.Delete(FilePath(key));

        #endregion


        #region PRIVATE

        private static void SaveToFile(string fileName, string fileContent) =>
            File.WriteAllText(FilePath(fileName), fileContent);

        private static string ReadFromFile(string fileName) =>
            File.Exists(FilePath(fileName)) ? File.ReadAllText(FilePath(fileName)) : string.Empty;

        private static string FilePath(string fileName) => Path.Combine(Application.persistentDataPath, fileName);

        #endregion
    }
}