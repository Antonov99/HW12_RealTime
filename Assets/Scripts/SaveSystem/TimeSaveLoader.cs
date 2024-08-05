using System;
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

        [Inject]
        public void Construct(IGameRepository gameRepository, ServerTimeManager serverTimeManager)
        {
            _gameRepository = gameRepository;
            _serverTimeManager = serverTimeManager;
        }

        void IInitializable.Initialize()
        {

        }

        void IDisposable.Dispose()
        {

        }

        public void Save<T>(T data)
        {
            _gameRepository.SetData(data);
        }

        public void Load()
        {
            _gameRepository.LoadState();
        }
    }
}