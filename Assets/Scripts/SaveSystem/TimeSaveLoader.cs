using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using DefaultNamespace;
using JetBrains.Annotations;
using UnityEngine;
using Zenject;

namespace SaveSystem
{
    [UsedImplicitly]
    public class TimeSaveLoader : ISaveLoader,IInitializable,IDisposable
    {
        private IGameRepository _gameRepository;
        private ServerTimeManager _serverTimeManager;
        private TimeManagerProvider _timeManagerProvider;

        private readonly List<DateTime> _dates = new();

        [Inject]
        public void Construct(IGameRepository gameRepository,  TimeManagerProvider timeManagerProvider)
        {
            _gameRepository = gameRepository;
            _timeManagerProvider = timeManagerProvider;
        }

        void IInitializable.Initialize()
        {
            Load();
            _timeManagerProvider.Initialize();
            _timeManagerProvider.OnTimeReceived += AddTime;
        }

        void IDisposable.Dispose()
        {
            AddTime(_timeManagerProvider.GetExitTime());
            _timeManagerProvider.OnTimeReceived -= AddTime;
            Save(_dates);
        }

        private void AddTime(DateTime time)
        {
            _dates.Add(time);
        }

        public void Save<T>(T data)
        {
            _gameRepository.SetData(data);
            _gameRepository.SaveState();
        }

        public void Load()
        {
            _gameRepository.LoadState();
        }
    }
}