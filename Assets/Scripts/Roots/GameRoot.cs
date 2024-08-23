using Core;
using Services.WindowService;
using UniRx;
using Zenject;

namespace Roots
{
    public class GameRoot : DisposableBehaviour<Unit>
    {
        private IWindowService _windowService;
        private WindowResolver _windowResolver;

        [Inject]
        private void Construct(IWindowService windowService, WindowResolver windowResolver)
        {
            _windowService = windowService;
            _windowResolver = windowResolver;
        }
        
        protected override void OnInit()
        {
            base.OnInit();
            
            OpenGreetingWindow();
        }

        //For prototype window
        private void OpenGreetingWindow()
        {
            var greetingsWindow = _windowResolver.GetGreetingsWindow();
            
            _windowService.Open(greetingsWindow, false);
        }

    }
}