using Data;
using UnityEngine;
using Zenject;

namespace Installers
{
    [CreateAssetMenu(fileName = "ProjectSettingsInstaller", menuName = "Installers/Create Project Settings Installer")]
    public class ProjectSettingsInstaller : ScriptableObjectInstaller<ProjectSettingsInstaller>
    {
        [field: SerializeField] private UrlSettings _urlSettings;
        [field: SerializeField] private ValuesSettings _valuesSettings;
        
        public override void InstallBindings()
        {
            Container.BindInstance(_urlSettings);
            Container.BindInstance(_valuesSettings);
        }
    }
}