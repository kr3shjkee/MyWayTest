using System;
using Data.Dto;
using MVP.Views;
using Services;

namespace MVP.Presenters
{
    public class MainWindowPresenter : BasePresenter
    {
        private readonly MainWindowView _view;
        private readonly MainScreenService _service;

        private int _counter;
        public MainWindowPresenter(MainWindowView view, MainScreenService service)
        {
            _view = view;
            _service = service;

            _service.ShowMainScreen += HandleDto;
            _view.UpdateButton.onClick.AddListener(UpdateContent);
            _view.UpButton.onClick.AddListener(UpCounter);
        }
        protected override void OnDispose()
        {
            _service.ShowMainScreen -= HandleDto;
            _view.UpdateButton.onClick.RemoveListener(UpdateContent);
            _view.UpButton.onClick.RemoveListener(UpCounter);
        }

        private void HandleDto(IDto dto)
        {
            if (dto is MainScreenDto mainScreenDto)
            {
                _counter = Int32.Parse(mainScreenDto.Counter);
                _view.Counter.text = _counter.ToString();
                _view.Text.text = mainScreenDto.Text;
                _view.UpButton.image.sprite = mainScreenDto.ButtonSprite;
                
                _view.Show();
            }
        }

        private void UpCounter()
        {
            _counter++;
            _view.Counter.text = _counter.ToString();
        }

        private void UpdateContent()
        {
            _service.InvokeUpdateContent();
            _view.Hide();
        }
    }
}
