using System;
using UnityEngine;
using System.IO;
using HexaSort.SaveLoadSystem;

namespace HexaSort.SaveLoad
{
    public static class SaveSystem
    {
        private static string BasePath
        {
            get
            {
                string dataPath = Path.Combine(Application.dataPath, "Data");
                
                if (!Directory.Exists(dataPath))
                {
                    Directory.CreateDirectory(dataPath);
                }

                return dataPath;
            }
        }

        public static void Save<T>(T data, string fileName)
        {
            try
            {
                string json = JsonUtility.ToJson(data, true);
                string fullPath = Path.Combine(BasePath, fileName);
                File.WriteAllText(fullPath, json);
                Debug.Log($"[SAVE SYSTEM] Đã lưu thành công tại: {fullPath}");
            }
            catch (Exception e)
            {
                Debug.LogError($"[SAVE SYSTEM] Lỗi khi lưu file: {e.Message}");
            }
        }

        public static T Load<T>(string fileName)
        {
            string fullPath = Path.Combine(BasePath, fileName);

            if (File.Exists(fullPath))
            {
                try
                {
                    string json = File.ReadAllText(fullPath);
                    
                    T data = JsonUtility.FromJson<T>(json);
                
                    Debug.Log("[SAVE SYSTEM] Đã load dữ liệu thành công.");
                    return data;
                }
                catch (Exception e)
                {
                    Debug.LogError($"[SAVE SYSTEM] File có tồn tại nhưng bị lỗi định dạng: {e.Message}");
                    return default(T);
                }
            }
            else
            {
                Debug.LogWarning("[SAVE SYSTEM] Không tìm thấy file save.");
                return default(T);
            }
        }
        
        public static void DeleteSave(string fileName)
        {
            string fullPath = Path.Combine(BasePath, fileName);
            if (File.Exists(fullPath))
            {
                File.Delete(fullPath);
                Debug.Log("[SaveSystem] Đã xóa file save.");
            }
        }
    }
}