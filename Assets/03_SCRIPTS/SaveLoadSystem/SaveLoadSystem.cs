using UnityEngine;
using System.IO;

namespace HexaSort.SaveLoadSystem
{
    public static class SaveLoadSystem
    {
        private static string SaveFileName = "hexasort_save.json";

        public static void Save(PlayerData data)
        {
            string json = JsonUtility.ToJson(data, true);
            string path = Path.Combine(Application.persistentDataPath, SaveFileName);

            try
            {
                File.WriteAllText(path, json);
                Debug.Log($"[SaveSystem] Saved to {path}");
            }
            catch (System.Exception e)
            {
                Debug.LogError($"[SaveSystem] Error saving: {e.Message}");
            }
        }

        public static PlayerData Load()
        {
            string path = Path.Combine(Application.persistentDataPath, SaveFileName);

            if (File.Exists(path))
            {
                try
                {
                    string json = File.ReadAllText(path);
                    PlayerData data = JsonUtility.FromJson<PlayerData>(json);
                    Debug.Log("[SaveSystem] Save file loaded.");
                    return data;
                }
                catch (System.Exception e)
                {
                    Debug.LogError($"[SaveSystem] Error loading: {e.Message}");
                    // Nếu file lỗi, trả về data mới
                    return new PlayerData();
                }
            }
            else
            {
                Debug.Log("[SaveSystem] No save file found. Creating new.");
                return new PlayerData();
            }
        }

        // Hàm xóa save (dùng để test)
        public static void DeleteSave()
        {
            string path = Path.Combine(Application.persistentDataPath, SaveFileName);
            if (File.Exists(path)) File.Delete(path);
        }
    }
}