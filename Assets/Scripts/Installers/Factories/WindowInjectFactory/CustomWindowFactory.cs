using Services.WindowService;
using UnityEngine;
using Zenject;

namespace Installers.Factories.WindowInjectFactory
{
    public class CustomWindowFactory : IFactory<WindowBase, Transform, WindowBase>
    {
        private readonly DiContainer _container;
        
        public CustomWindowFactory(DiContainer container)
        {
            _container = container;
        }
        
        public WindowBase Create(WindowBase param1, Transform param2)
        {
            return _container.InstantiatePrefabForComponent<WindowBase>(param1, param2);
        }
    }
}