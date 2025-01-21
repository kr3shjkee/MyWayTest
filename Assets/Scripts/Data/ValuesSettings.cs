using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = "ValuesSettings", menuName = "Settings/Create Values Settings")]
    public class ValuesSettings : ScriptableObject
    {
        [field:SerializeField] public float LoadingDuration { get; private set; }
    }
}
