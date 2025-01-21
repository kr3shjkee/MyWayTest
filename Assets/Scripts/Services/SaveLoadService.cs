using System;
using System.IO;
using Cysharp.Threading.Tasks;
using Data.Dto;
using UnityEngine;
using Zenject;

namespace Services
{
    public class SaveLoadService : IInitializable
    {
        private int _counter;
        private string _filePath;
        public int Counter => _counter;
        
        public void Initialize()
        {
            _filePath = Application.persistentDataPath + "/save.json";
        }
        
        public async UniTask<bool> TryLoadDataAsync()
        {
            if (!File.Exists(_filePath))
            {
                return false;
            }
            
            JsonIntDto dto = JsonUtility.FromJson<JsonIntDto>(await File.ReadAllTextAsync(_filePath));

            if (dto == null)
            {
                return false;
            }
                
            _counter = Int32.Parse(dto.StartValue);
            return true;
        }

        public void SaveData(int counter)
        {
            JsonIntDto dto = new JsonIntDto();
            dto.StartValue = counter.ToString();
            
            File.WriteAllText(_filePath, JsonUtility.ToJson(dto));
        }
    }
}