using MVP.Presenters;
using MVP.Views;
using Services;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class MainSceneMonoInstaller : MonoInstaller
    {
        [field: SerializeField] private LoadingWindowView _loadingWindowView;
        [field: SerializeField] private MainWindowView _mainWindowView;
        [field: SerializeField] private ErrorWindowView _errorWindowView;
        public override void InstallBindings()
        {
            BindViews();
            BindServices();
            BindPresenters();
        }
        
        private void BindServices()
        {
            Container.BindInterfacesAndSelfTo<LoadingService>().AsSingle();
        }
        
        private void BindPresenters()
        {
            Container.BindInterfacesAndSelfTo<LoadingWindowPresenter>().AsSingle();
            Container.BindInterfacesAndSelfTo<MainWindowBasePresenter>().AsSingle();
            Container.BindInterfacesAndSelfTo<ErrorWindowBasePresenter>().AsSingle();
        }

        private void BindViews()
        {
            Container.BindInstance(_loadingWindowView);
            Container.BindInstance(_mainWindowView);
            Container.BindInstance(_errorWindowView);
        }
    }
}