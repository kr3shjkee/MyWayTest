using System;
using Data.Dto;

namespace Services
{
    public class MainScreenService
    {
        public event Action<IDto> ShowMainScreen;

        public void InvokeShowMainScreen(IDto dto)
        {
            ShowMainScreen?.Invoke(dto);
        }
    }
}
