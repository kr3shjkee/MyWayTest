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
        private readonly ErrorScreenService _errorScreenService;

        public GameManager(LoadingService loadingService, 
            TimerService timerService, 
            MainScreenService mainScreenService,
            ErrorScreenService errorScreenService)
        {
            _loadingService = loadingService;
            _timerService = timerService;
            _mainScreenService = mainScreenService;
            _errorScreenService = errorScreenService;
            
            _loadingService.LoadingFinished += CheckTimer;
            _loadingService.LoadWithError += HandleLoadError;
            _timerService.TimerFinished += CheckTimer;
            _errorScreenService.RetryLoad += Initialize;
            _mainScreenService.UpdateContent += Initialize;
        }
        
        public void Dispose()
        {
            _loadingService.LoadingFinished -= CheckTimer;
            _loadingService.LoadWithError -= HandleLoadError;
            _timerService.TimerFinished -= CheckTimer;
            _errorScreenService.RetryLoad -= Initialize;
            _mainScreenService.UpdateContent -= Initialize;
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

        private void HandleLoadError()
        {
            _errorScreenService.InvokeShowMainScreen();
        }
    }
}
