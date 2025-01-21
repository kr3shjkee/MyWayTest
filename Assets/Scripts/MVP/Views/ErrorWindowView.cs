
using UnityEngine;
using UnityEngine.UI;

namespace MVP.Views
{
    public class ErrorWindowView : BaseView
    {
        [field:SerializeField] public Button CloseButton { get; private set; }
        [field:SerializeField] public Button RetryButton { get; private set; }
    }
}
