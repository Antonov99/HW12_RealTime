using System.Collections.Generic;
using System;
using System.IO;
using JetBrains.Annotations;
using Newtonsoft.Json;
using UnityEngine;

namespace SaveSystem
{
    [UsedImplicitly]
    public sealed class GameRepository:IGameRepository
    {
        private Dictionary<Type, string> _storage=new();
        private const string _FILE_NAME = "/Storage.json";
        private readonly string _filePath = Application.persistentDataPath + _FILE_NAME;

        public void SetData<T>(T data)
        {
            Type dataType = typeof(T);
            string serializedData = JsonConvert.SerializeObject(data);
            _storage[dataType] = serializedData;
        }

        public T GetData<T>()
        {
            Type dataType = typeof(T);
            string serializedData = _storage[dataType];
            T deserializedData =JsonConvert.DeserializeObject<T>(serializedData);

            return deserializedData;
        }

        public bool TryGetData<T>(out T data)
        {
            Type dataType = typeof(T);
            if (_storage.TryGetValue(dataType, out string serializedData))
            {
                data = JsonConvert.DeserializeObject<T>(serializedData);
                return true;
            }
            data = default;
            return false;
        }

        public void SaveState()
        {
            string serializedData = JsonConvert.SerializeObject(_storage);
            File.WriteAllText(_filePath,serializedData);
            
            Debug.Log("Saved");
        }

        public void LoadState()
        {
            if (!File.Exists(_filePath)) return;

            string savedStorage = File.ReadAllText(_filePath);
            
            _storage = JsonConvert.DeserializeObject<Dictionary<Type,string>>(savedStorage);
            
            Debug.Log("Loaded");
        }
    }
}