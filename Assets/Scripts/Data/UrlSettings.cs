using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = "UrlSettings", menuName = "Settings/Create URL Settings")]
    public class UrlSettings : ScriptableObject
    {
        [field:SerializeField] public string JsonStringUrl { get; private set; }
        [field:SerializeField] public string JsonIntUrl { get; private set; }
        [field:SerializeField] public string AssetBundleUrl { get; private set; }
    }
}
