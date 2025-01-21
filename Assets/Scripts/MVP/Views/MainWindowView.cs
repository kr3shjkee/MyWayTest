using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace MVP.Views
{
    public class MainWindowView : BaseView
    {
        [field:SerializeField] public TMP_Text Text { get; private set; }
        [field:SerializeField] public TMP_Text Counter { get; private set; }
        [field:SerializeField] public Button UpButton { get; private set; }
        [field:SerializeField] public Button UpdateButton { get; private set; }

        public event Action SaveCounter;

        private void OnApplicationPause(bool pauseStatus)
        {
            if (pauseStatus)
            {
                SaveCounter?.Invoke();
            }
        }

        private void OnApplicationQuit()
        {
            SaveCounter?.Invoke();
        }
    }
}
