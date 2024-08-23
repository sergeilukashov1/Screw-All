using System;
using Core.StateManagement;
using States;
using Support;
using UniRx;
using UnityEngine;

namespace Roots
{
    public class MainRoot : MonoBehaviour
    {
        private GameMachine _gameMachine;
    
        private readonly CompositeDisposable _rootDisposable = new();
        
        private const string StateGameSceneName = "GameRoot";
    
        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
            StartRoot();
        }

        private void StartRoot()
        {
            Observable.ReturnUnit()
                .ContinueWith(_ => InitStates())
                .ContinueWith(_ => LoadStage())
                .EmptySubscribe()
                .AddTo(_rootDisposable);
        }

        //Registration of states
        private IObservable<Unit> InitStates()
        {
            _gameMachine = new GameMachine();
        
            _gameMachine
                .Init()
                .AddTo(_rootDisposable);
            
            _gameMachine.AddState(new GameState());
            
            return Observable.ReturnUnit();
        }
    
        //Change state transition
        private IObservable<Unit> LoadStage()
        {
            _gameMachine.ChangeState<GameState>(StateGameSceneName);
            return Observable.ReturnUnit();
        }
    
        private void OnDestroy()
        {
            _rootDisposable.Clear();
        }
    }
}