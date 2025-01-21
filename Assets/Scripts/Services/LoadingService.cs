using System;
using System.Threading;
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
        private CancellationTokenSource _cts;
        public LoadingService(UrlSettings urlSettings)
        {
            _urlSettings = urlSettings;
        }
        public event Action ShowLoadScreen;
        public event Action HideLoadScreen;
        public event Action LoadWithError;
        public event Action<WaitingType> LoadingFinished;

        public bool IsLoadingFinish => _isLoadingFinish;

        public async void StartLoad()
        {
            _isLoadingFinish = false;
            _dto = new MainScreenDto();
            ShowLoadScreen?.Invoke();
            CheckSaveData();
            
            if (await TryLoadJsons())
            {
                _isLoadingFinish = true;
                LoadingFinished?.Invoke(WaitingType.Loading);
            }
            else
            {
                LoadWithError?.Invoke();
            }
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

        private async UniTask<bool> TryLoadJsons()
        {
            try
            {
                _cts = new CancellationTokenSource();
                JsonStringDto stringDto = await GetJson<JsonStringDto>(_urlSettings.JsonStringUrl, _cts.Token);
                _cts.Dispose();
                _cts = null;
                _dto.Text = stringDto.Text;
            }
            catch (Exception e)
            {
                return false;
            }
            
            try
            {
                _cts = new CancellationTokenSource();
                JsonIntDto intDto = await GetJson<JsonIntDto>(_urlSettings.JsonIntUrl, _cts.Token);
                _cts.Dispose();
                _cts = null;
                _dto.Counter = intDto.StartValue;
            }
            catch (Exception e)
            {
                return false;
            }

            return true;
        }
        
        private async UniTask<TValue> GetJson<TValue>(string url, CancellationToken token) where TValue : IDto
        {
            TValue value;

            UnityWebRequest request = UnityWebRequest.Get(url);
            await request
                .SendWebRequest()
                .WithCancellation(token);
        
            if (request.isHttpError || request.isNetworkError)
            {
                throw new Exception("Http or network problem");
            }
            
            Debug.Log($"Successfully download Json ({typeof(TValue)})");

            string json = request.downloadHandler.text;

            if (string.IsNullOrEmpty(json))
            {
                throw new Exception("Json is Null or Empty");  
            }

            value = JsonUtility.FromJson<TValue>(json);

            return value;
        }
    }
}
