using System;
using DefaultNamespace;
using JetBrains.Annotations;
using Zenject;

namespace SaveSystem
{
    [UsedImplicitly]
    public class TimeSaveLoader : ISaveLoader,IInitializable,IDisposable
    {
        private IGameRepository _gameRepository;
        private ServerTimeManager _serverTimeManager;
        private TimeManagerProvider _timeManagerProvider;

        private (DateTime, DateTime) _dates;

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
            _timeManagerProvider.OnTimeReceived += AddEnterTime;
        }

        void IDisposable.Dispose()
        {
            AddExitTime(_timeManagerProvider.GetExitTime());
            _timeManagerProvider.OnTimeReceived -= AddEnterTime;
            Save(_dates);
        }

        private void AddEnterTime(DateTime time)
        {
            _dates.Item1 = time;
        }

        private void AddExitTime(DateTime time)
        {
            _dates.Item2 = time;
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