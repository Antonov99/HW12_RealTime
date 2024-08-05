using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using DefaultNamespace;

namespace Chest
{
    public class ChestTimer
    {
        private readonly ChestView _chestView;
        private readonly DateTime _openTime;
        private readonly ServerTimeManager _serverTimeManager;

        private readonly CancellationTokenSource _cancellationToken;

        public event Action OnTimeEnded;
        
        public ChestTimer(ChestView chestView, DateTime createTime, int openTime, ServerTimeManager serverTimeManager)
        {
            _chestView = chestView;
            _openTime = createTime.AddSeconds(openTime);
            _serverTimeManager = serverTimeManager;
            _cancellationToken = new CancellationTokenSource();

            ServerTimeManagerInitialize();
        }

        private async UniTaskVoid ServerTimeManagerInitialize()
        {
            await _serverTimeManager.Initialize();
            UpdateTimerAsync();
        }

        private async UniTaskVoid UpdateTimerAsync()
        {
            while (!_serverTimeManager.isActualTimeReceived)
                await UniTask.Yield(PlayerLoopTiming.Update);

            while (!_cancellationToken.Token.IsCancellationRequested)
            {
                TimeSpan timeRemaining = _openTime - _serverTimeManager.GetCurrentTime();

                if (timeRemaining.TotalSeconds > 0)
                {
                    string timerText = string.Format("{0:D2}:{1:D2}:{2:D2}",
                        timeRemaining.Hours,
                        timeRemaining.Minutes,
                        timeRemaining.Seconds);
                    
                    _chestView.SetTimerText(timerText);
                }
                else
                {
                    _chestView.SetTimerText("Open");
                    OnTimeEnded?.Invoke();
                    break;
                }

                await UniTask.Yield(PlayerLoopTiming.Update, _cancellationToken.Token);
            }
        }
    }
}