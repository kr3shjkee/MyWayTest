using System;

namespace Services
{
    public class ErrorScreenService
    {
        public event Action ShowErrorScreen;

        public void InvokeShowMainScreen()
        {
            ShowErrorScreen?.Invoke();
        }
    }
}
