using System.IO;
using Mvp.Services;
using Newtonsoft.Json;
using UnityEngine;

namespace Maze.Services.Save
{
    public class SaveService : Service
    {
        private readonly string _saveFolderPath;

        public SaveService()
        {
            _saveFolderPath = Application.persistentDataPath;

            if (!Directory.Exists(_saveFolderPath))
            {
                Directory.CreateDirectory(_saveFolderPath);
#if UNITY_EDITOR
                Debug.Log($"[SaveService] Created folder at {_saveFolderPath}");
#endif
            }
        }

        public void Save<T>(string fileName, T data)
        {
            string filePath = Path.Combine(_saveFolderPath, fileName);
            string json = JsonConvert.SerializeObject(data, Formatting.Indented);
            File.WriteAllText(filePath, json);
#if UNITY_EDITOR
            Debug.Log($"[SaveService] Saved data to {filePath}");
#endif
        }

        public T Load<T>(string fileName)
        {
            string filePath = Path.Combine(_saveFolderPath, fileName);
            if (!File.Exists(filePath))
            {
#if UNITY_EDITOR
                Debug.LogWarning($"[SaveService] File not found: {filePath}");
#endif
                return default;
            }

            string json = File.ReadAllText(filePath);
            return JsonConvert.DeserializeObject<T>(json);
        }
    }
}