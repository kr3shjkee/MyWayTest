using System;
using Data.Dto;
using MVP.Views;
using Services;
using UnityEngine;

namespace MVP.Presenters
{
    public class MainWindowPresenter : BasePresenter
    {
        private readonly MainWindowView _view;
        private readonly MainScreenService _mainScreenService;
        private readonly SaveLoadService _saveLoadService;

        private int _counter;
        public MainWindowPresenter(MainWindowView view, 
            MainScreenService mainScreenService, 
            SaveLoadService saveLoadService)
        {
            _view = view;
            _mainScreenService = mainScreenService;
            _saveLoadService = saveLoadService;

            _mainScreenService.ShowMainScreen += HandleDto;
            _view.SaveCounter += SaveCounter;
            _view.UpdateButton.onClick.AddListener(UpdateContent);
            _view.UpButton.onClick.AddListener(UpCounter);
            _view.DeleteButton.onClick.AddListener(DeleteSaveAndQuit);
        }
        protected override void OnDispose()
        {
            _mainScreenService.ShowMainScreen -= HandleDto;
            _view.SaveCounter -= SaveCounter;
            _view.UpdateButton.onClick.RemoveListener(UpdateContent);
            _view.UpButton.onClick.RemoveListener(UpCounter);
            _view.DeleteButton.onClick.RemoveListener(DeleteSaveAndQuit);
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
            _mainScreenService.InvokeUpdateContent();
            _view.Hide();
        }

        private void SaveCounter()
        {
            if (_counter != 0)
            {
                _saveLoadService.SaveData(_counter);
            }
        }

        private void DeleteSaveAndQuit()
        {
            _saveLoadService.DeleteData();
            Application.Quit();
            
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#endif
            
        }
    }
}
