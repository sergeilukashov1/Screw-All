using Installers.Factories.WindowInjectFactory;
using Services.WindowService;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class ProjectInstaller : MonoInstaller
    {
        [SerializeField] private WindowsService _windowsService;
        
        public override void InstallBindings()
        {
            BindServices();
            BindFactories();
        }

        private void BindServices()
        {
            Container
                .BindInterfacesAndSelfTo<WindowsService>()
                .FromComponentInNewPrefab(_windowsService)
                .AsSingle();
            
            Container
                .Bind<WindowResolver>()
                .AsSingle();
        }

        private void BindFactories()
        {
            Container
                .BindFactory<WindowBase, Transform, WindowBase, WindowPlaceholderFactory>()
                .FromFactory<CustomWindowFactory>();
        }
    }
}