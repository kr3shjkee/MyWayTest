using System;
using Cysharp.Threading.Tasks;
using Data;
using Data.Dto;
using Data.Enum;
using UnityEngine;
using UnityEngine.Networking;

namespace Services
{
    public class LoadingService
    {
        private readonly UrlSettings _urlSettings;

        private bool _isLoadingFinish;
        private MainScreenDto _dto;
        public LoadingService(UrlSettings urlSettings)
        {
            _urlSettings = urlSettings;
        }
        public event Action ShowLoadScreen;
        public event Action HideLoadScreen;
        public event Action<WaitingType> LoadingFinished;

        public bool IsLoadingFinish => _isLoadingFinish;

        public async void StartLoad()
        {
            _isLoadingFinish = false;
            _dto = new MainScreenDto();
            ShowLoadScreen?.Invoke();
            CheckSaveData();
            await LoadJsons();
            _isLoadingFinish = true;
            LoadingFinished?.Invoke(WaitingType.Loading);
        }
        
        public MainScreenDto GetDto()
        {
            return _dto;
        }

        public void InvokeHideLoadScreen()
        {
            HideLoadScreen?.Invoke();
        }

        private void CheckSaveData()
        {
            
        }

        private async UniTask LoadJsons()
        {
            JsonStringDto stringDto = await GetJson<JsonStringDto>(_urlSettings.JsonStringUrl);
            _dto.Text = stringDto.Text;

            JsonIntDto intDto = await GetJson<JsonIntDto>(_urlSettings.JsonIntUrl);
            _dto.Counter = intDto.StartValue;
        }
        
        private async UniTask<TValue> GetJson<TValue>(string url) where TValue : IDto
        {
            TValue value = default;

            UnityWebRequest request = UnityWebRequest.Get(url);
            await request.SendWebRequest();
        
            if (request.isHttpError || request.isNetworkError)
            {
                
            }
            else
            {
                Debug.Log("Successfully download Json");

                string json = request.downloadHandler.text;

                if (string.IsNullOrEmpty(json))
                {
                    
                }

                value = JsonUtility.FromJson<TValue>(json);
            }

            return value;
        }
    }
}
