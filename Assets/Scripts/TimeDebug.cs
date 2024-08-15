using System;
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
            if (_gameRepository.TryGetData(out (DateTime,DateTime) data))
            {
                    TimeSpan timeIn = data.Item2-data.Item1;
                    Debug.Log($"Время входа: {data.Item1} Время выхода:{data.Item2} Прошло времени {timeIn}");
                
            }
        }
    }
}