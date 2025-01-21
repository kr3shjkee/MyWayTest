using Data.Dto;
using MVP.Views;
using Services;

namespace MVP.Presenters
{
    public class MainWindowPresenter : BasePresenter
    {
        private readonly MainWindowView _view;
        private readonly MainScreenService _service;
        public MainWindowPresenter(MainWindowView view, MainScreenService service)
        {
            _view = view;
            _service = service;

            _service.ShowMainScreen += HandleDto;
        }
        protected override void OnDispose()
        {
            _service.ShowMainScreen -= HandleDto;
        }

        private void HandleDto(IDto dto)
        {
            if (dto is MainScreenDto mainScreenDto)
            {
                _view.Counter.text = mainScreenDto.Counter;
                _view.Text.text = mainScreenDto.Text;
                _view.UpButton.image.sprite = mainScreenDto.ButtonSprite;
                
                _view.Show();
            }
        }
    }
}
