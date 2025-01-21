using System;
using Cysharp.Threading.Tasks;
using Data;
using Data.Enum;

namespace Services
{
    public class TimerService
    {
        private readonly float _duration;

        private bool _isTimerFinish = true;
        
        public TimerService(ValuesSettings settings)
        {
            _duration = settings.LoadingDuration;
        }

        public event Action<WaitingType> TimerFinished;

        public bool IsTimerFinish => _isTimerFinish;

        public async void StartTimerAsync()
        {
            _isTimerFinish = false;

            await UniTask.Delay(TimeSpan.FromSeconds(_duration));

            _isTimerFinish = true;
            TimerFinished?.Invoke(WaitingType.Timer);
        }
    }
}
