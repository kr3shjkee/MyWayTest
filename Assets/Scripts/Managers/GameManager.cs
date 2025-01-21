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
        private readonly SaveLoadService _saveLoadService;

        public GameManager(LoadingService loadingService, 
            TimerService timerService, 
            MainScreenService mainScreenService,
            ErrorScreenService errorScreenService,
            SaveLoadService saveLoadService)
        {
            _loadingService = loadingService;
            _timerService = timerService;
            _mainScreenService = mainScreenService;
            _errorScreenService = errorScreenService;
            _saveLoadService = saveLoadService;
            
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
            _loadingService.StartLoadAsync();
            _timerService.StartTimerAsync();
        }

        private async void CheckTimer(WaitingType type)
        {
            if (type == WaitingType.Loading && _timerService.IsTimerFinish ||
                type == WaitingType.Timer && _loadingService.IsLoadingFinish)
            {
                MainScreenDto dto = _loadingService.GetDto();
                if (await _saveLoadService.TryLoadDataAsync())
                {
                    dto.Counter = _saveLoadService.Counter.ToString();
                }
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
