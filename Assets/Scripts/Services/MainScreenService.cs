using System;
using Data.Dto;

namespace Services
{
    public class MainScreenService
    {
        public event Action<IDto> ShowMainScreen;
        public event Action UpdateContent;

        public void InvokeShowMainScreen(IDto dto)
        {
            ShowMainScreen?.Invoke(dto);
        }

        public void InvokeUpdateContent()
        {
            UpdateContent?.Invoke();
        }
    }
}
