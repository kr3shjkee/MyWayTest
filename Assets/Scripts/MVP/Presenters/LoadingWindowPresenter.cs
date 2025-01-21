using MVP.Views;
using Services;

namespace MVP.Presenters
{
    public class LoadingWindowPresenter : BasePresenter
    {
        private readonly LoadingWindowView _view;
        private readonly LoadingService _service;

        public LoadingWindowPresenter(LoadingWindowView view, LoadingService service)
        {
            _view = view;
            _service = service;
            _service.ShowLoadScreen += _view.Show;
            _service.HideLoadScreen += _view.Hide;
        }


        protected override void OnDispose()
        {
            _service.ShowLoadScreen -= _view.Show;
            _service.HideLoadScreen -= _view.Hide;
        }
    }
}
