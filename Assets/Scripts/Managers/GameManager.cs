using System;
using Zenject;

namespace Managers
{
    public class GameManager : IInitializable
    {
        public event Action InvokeStartLoad;
        
        public void Initialize()
        {
            InvokeStartLoad?.Invoke();
        }
    }
}
