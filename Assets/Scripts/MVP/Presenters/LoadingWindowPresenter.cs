using Cysharp.Threading.Tasks;
using Data;
using MVP.Views;
using Services;

namespace MVP.Presenters
{
    public class LoadingWindowPresenter : BasePresenter
    {
        private readonly LoadingWindowView _view;
        private readonly LoadingService _service;
        private readonly ValuesSettings _valuesSettings;

        public LoadingWindowPresenter(LoadingWindowView view, 
            LoadingService service,
            ValuesSettings settings)
        {
            _view = view;
            _service = service;
            _valuesSettings = settings;
            
            _service.ShowLoadScreen += OnShow;
            _service.HideLoadScreen += OnHide;
        }


        protected override void OnDispose()
        {
            _service.ShowLoadScreen -= OnShow;
            _service.HideLoadScreen -= OnHide;
        }

        private void OnShow()
        {
            _view
                .Animations
                .StartAnimation(_valuesSettings.LoadingDuration)
                .Forget();
            
            _view.Show();
        }

        private void OnHide()
        {
            _view.Hide();
            _view.Animations.FinishAnimation();
        }
    }
}
