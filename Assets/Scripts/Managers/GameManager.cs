using System;
using Data.Dto;
using Data.Enum;
using Services;
using Zenject;

namespace Managers
{
    public class GameManager : IInitializable, IDisposable
    {
        private readonly LoadingService _loadingService;
        private readonly TimerService _timerService;
        private readonly MainScreenService _mainScreenService;

        public GameManager(LoadingService loadingService, 
            TimerService timerService, 
            MainScreenService mainScreenService)
        {
            _loadingService = loadingService;
            _timerService = timerService;
            _mainScreenService = mainScreenService;
            
            _loadingService.LoadingFinished += CheckTimer;
            _timerService.TimerFinished += CheckTimer;
        }
        
        public void Dispose()
        {
            _loadingService.LoadingFinished -= CheckTimer;
            _timerService.TimerFinished -= CheckTimer;
        }
        
        public void Initialize()
        {
            _loadingService.StartLoad();
            _timerService.StartTimer();
        }

        private void CheckTimer(WaitingType type)
        {
            if (type == WaitingType.Loading && _timerService.IsTimerFinish ||
                type == WaitingType.Timer && _loadingService.IsLoadingFinish)
            {
                MainScreenDto dto = _loadingService.GetDto();
                _mainScreenService.InvokeShowMainScreen(dto);
                _loadingService.InvokeHideLoadScreen();
            }
        }
    }
}
