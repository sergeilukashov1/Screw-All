using System;
using System.Collections.Generic;
using UniRx;

namespace Core.StateManagement
{
    public class GameMachine : DisposableClass
    {
        private readonly Dictionary<Type, IGameState> _states = new();
        private IGameState _currentState;

        protected override void OnInit()
        {
            base.OnInit();

            Disposable
                .Create(OnDisposed)
                .AddTo(Disposables);
        }

        public void AddState(IGameState state)
        {
            _states.Add(state.GetType(), state);
        }

        public void ChangeState<T>(string sceneName)
        {
            _currentState?.Deinit();
            _currentState = _states[typeof(T)];
            _currentState.Init(sceneName);
        }

        private void OnDisposed()
        {
            _currentState?.Deinit();
        }
    }
}