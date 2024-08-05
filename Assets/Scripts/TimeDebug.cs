using System;
using System.Collections.Generic;
using SaveSystem;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace DefaultNamespace
{
    public class TimeDebug : MonoBehaviour
    {
        private IGameRepository _gameRepository;

        [Inject]
        public void Construct(IGameRepository gameRepository)
        {
            _gameRepository = gameRepository;
        }

        [Button]
        private void DebugTime()
        {
            if (_gameRepository.TryGetData(out List<DateTime> data))
            {
                Debug.Log("пустой список");
                for (int i = 1; i < data.Count; i++)
                {
                    TimeSpan timeIn = data[i] - data[i - 1];
                    Debug.Log($"Время входа: {data[i - 1]} Время выхода:{data[i]} Прошло времени {timeIn}");
                }
            }
        }
    }
}