using System;
using Cysharp.Threading.Tasks;
using JetBrains.Annotations;
using Zenject;

namespace DefaultNamespace
{
    [UsedImplicitly]
    public class TimeManagerProvider
    {
        private ServerTimeManager _serverTimeManager;

        private DateTime _currentTime;

        public event Action<DateTime> OnTimeReceived; 
        
        [Inject]
        public void Construct(ServerTimeManager serverTimeManager)
        {
            _serverTimeManager = serverTimeManager;
        }

        public void Initialize()
        {
            GetStartTime();
        }

        private async UniTaskVoid GetStartTime()
        {
            while (!_serverTimeManager.isActualTimeReceived)
                await UniTask.Yield(PlayerLoopTiming.Update);

            _currentTime = _serverTimeManager.GetCurrentTime();
            OnTimeReceived?.Invoke(_currentTime);
        }

        public DateTime GetExitTime()
        {
            return _serverTimeManager.GetCurrentTime();
        }
    }
}