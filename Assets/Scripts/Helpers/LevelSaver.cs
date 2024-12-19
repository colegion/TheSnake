using System.IO;
using System.Linq;
using UnityEngine;

namespace Helpers
{
    public class LevelSaver
    {
        private const string LevelsFolder = "Assets/Resources/Levels";

        public void SaveLevelWithIndex(LevelData levelData, int levelIndex)
        {
            if (!Directory.Exists(LevelsFolder))
            {
                Directory.CreateDirectory(LevelsFolder);
            }
            
            string fileName = $"level{levelIndex}.json";
            string filePath = Path.Combine(LevelsFolder, fileName);
            
            string jsonData = JsonUtility.ToJson(levelData, true);
            
            File.WriteAllText(filePath, jsonData);
            
#if UNITY_EDITOR
            UnityEditor.AssetDatabase.Refresh();
#endif

            Debug.Log($"Level saved: {filePath}");
        }

        public int GetNextLevelIndex()
        {
            var levelFiles = Resources.LoadAll<TextAsset>($"Levels");

            var indices = levelFiles
                .Select(file => ParseLevelIndex(file.name))
                .Where(index => index.HasValue)
                .Select(index => index.Value);

            var enumerable = indices.ToList();
            int maxIndex = enumerable.Any() ? enumerable.Max() : -1;

            return maxIndex + 1;
        }

        private int? ParseLevelIndex(string fileName)
        {
            if (fileName.StartsWith("level"))
            {
                string numberPart = fileName.Substring(5);
                if (int.TryParse(numberPart, out int index))
                {
                    return index;
                }
            }

            return null;
        }
    }
}