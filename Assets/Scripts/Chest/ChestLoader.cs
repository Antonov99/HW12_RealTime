using System;
using System.IO;
using JetBrains.Annotations;
using Newtonsoft.Json;
using UnityEngine;

namespace Chest
{
    [UsedImplicitly]
    public class ChestLoader
    {
        private const string _JSON_FILE = "ChestData.json";

        public ChestCollection LoadChests()
        {
            string filePath = Path.Combine(Application.streamingAssetsPath, _JSON_FILE);

            if (File.Exists(filePath))
            {
                string jsonContent = File.ReadAllText(filePath);
                ChestCollection chestCollection = JsonConvert.DeserializeObject<ChestCollection>(jsonContent);
                return chestCollection;
            }

            throw new Exception("json");
        }
    }
}