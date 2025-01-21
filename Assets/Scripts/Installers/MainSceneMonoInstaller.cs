using Managers;
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
            BindManagers();
        }
        
        private void BindServices()
        {
            Container.Bind<LoadingService>().AsSingle();
            Container.Bind<TimerService>().AsSingle();
            Container.Bind<MainScreenService>().AsSingle();
            Container.Bind<ErrorScreenService>().AsSingle();
        }
        
        private void BindPresenters()
        {
            Container.BindInterfacesAndSelfTo<LoadingWindowPresenter>().AsSingle();
            Container.BindInterfacesAndSelfTo<MainWindowPresenter>().AsSingle();
            Container.BindInterfacesAndSelfTo<ErrorWindowPresenter>().AsSingle();
        }

        private void BindViews()
        {
            Container.BindInstance(_loadingWindowView);
            Container.BindInstance(_mainWindowView);
            Container.BindInstance(_errorWindowView);
        }

        private void BindManagers()
        {
            Container.BindInterfacesAndSelfTo<GameManager>().AsSingle();
        }
    }
}