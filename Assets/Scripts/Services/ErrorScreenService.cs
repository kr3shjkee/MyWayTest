using System;

namespace Services
{
    public class ErrorScreenService
    {
        public event Action ShowErrorScreen;
        public event Action RetryLoad;

        public void InvokeShowMainScreen()
        {
            ShowErrorScreen?.Invoke();
        }

        public void InvokeRetryLoad()
        {
            RetryLoad?.Invoke();
        }
    }
}
