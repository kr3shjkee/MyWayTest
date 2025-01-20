using System;

namespace MVP
{
    public abstract class BasePresenter : IDisposable
    {
        public void Dispose() => OnDispose();

        protected abstract void OnDispose();
    }
}
