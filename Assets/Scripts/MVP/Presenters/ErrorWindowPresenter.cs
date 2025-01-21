using MVP.Views;
using Services;
using UnityEngine;

namespace MVP.Presenters
{
    public class ErrorWindowPresenter : BasePresenter
    {
        private readonly ErrorWindowView _view;
        private readonly ErrorScreenService _service;

        public ErrorWindowPresenter(ErrorWindowView view, ErrorScreenService service)
        {
            _view = view;
            _service = service;
            _service.ShowErrorScreen += _view.Show;
            _view.CloseButton.onClick.AddListener(CloseApplication);
            _view.RetryButton.onClick.AddListener(RetryLoad);
        }
        protected override void OnDispose()
        {
            _service.ShowErrorScreen -= _view.Show;
            _view.CloseButton.onClick.RemoveListener(CloseApplication);
            _view.RetryButton.onClick.RemoveListener(RetryLoad);
        }

        private void CloseApplication()
        {
            Application.Quit();
        }

        private void RetryLoad()
        {
            _view.Hide();
            _service.InvokeRetryLoad();
        }
    }
}

