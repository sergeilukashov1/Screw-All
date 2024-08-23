using System;
using Core.StateManagement;
using Roots;
using Support;
using UniRx;

namespace States
{
    public class GameState : GameStateBase<Unit>
    {
        private readonly CompositeDisposable _rootDisposable = new();
        
        private void OnSceneLoaded()
        {
            InitControllers()
                .AddTo(_rootDisposable);
        }
        
        private IDisposable InitControllers()
        {
            var subscriptions = new CompositeDisposable();
        
            var gameRoot = SceneExtensions.LoadSceneRoot<GameRoot>();

            gameRoot
                .Init(new Unit())
                .AddTo(subscriptions);

            return subscriptions;
        }
        
        protected override void Init(string sceneName)
        {
            SceneExtensions.LoadScene(sceneName)
                .SafeSubscribe(_ => OnSceneLoaded())
                .AddTo(_rootDisposable);
        }

        protected override void Deinit()
        {
            _rootDisposable.Clear();
        }
    }
}