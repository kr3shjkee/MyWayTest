using System;
using Managers;

namespace Services
{
    public class LoadingService : IDisposable
    {
        private readonly GameManager _gameManager;

        public LoadingService(GameManager gameManager)
        {
            _gameManager = gameManager;
            _gameManager.InvokeStartLoad += InvokeShowLoadScreen;
        }

        public event Action ShowLoadScreen;
        
        public void Dispose()
        {
            _gameManager.InvokeStartLoad -= InvokeShowLoadScreen;
        }

        private void InvokeShowLoadScreen()
        {
            ShowLoadScreen?.Invoke();
        }

        
    }
}
