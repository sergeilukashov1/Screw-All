using System;
using System.Collections.Generic;
using Core;
using Installers.Factories.WindowInjectFactory;
using UniRx;
using UnityEngine;
using Zenject;

namespace Services.WindowService
{
    public class WindowsService : DisposableBehaviour<WindowsService.Model>, IWindowService
    {
        public class Model
        {
        }
        
        [SerializeField] private List<WindowBase> _prefabs;
        
        private WindowPlaceholderFactory _factory;

        private const int EnvironmentOrder = 1;
        private readonly List<WindowBase> _instances = new();
        private readonly Stack<WindowBase> _windowsStack = new();

        [Inject]
        private void Construct(WindowPlaceholderFactory customWindowFactory)
        {
            _factory = customWindowFactory;
        }

        public IObservable<Unit> ObserveWindowOpen<T>() =>
            CurrentWindow.Where(window => window != null && window.GetType() == typeof(T))
                .Select(_ => _)
                .First()
                .AsUnitObservable();

        public IObservable<Unit> ObserveWindowClose<T>() =>
            CurrentWindow.Pairwise()
                .Where(pair => pair.Previous != null && pair.Previous.GetType() == typeof(T))
                .Select(_ => _)
                .First()
                .AsUnitObservable();

        public IObservable<Unit> ObserveWindowFullClose<T>()
        {
            return _windowClosedObservable.Where(closingWindowType => closingWindowType == typeof(T))
                .First()
                .AsUnitObservable();
        }

        public IReadOnlyReactiveProperty<WindowBase> CurrentWindow => _currentWindow;

        private readonly ReactiveCommand<Type> _windowClosedObservable = new();
        private readonly ReactiveProperty<WindowBase> _currentWindow = new();

        private readonly CompositeDisposable _loadingDisposable = new();

        public static void Quit()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }
        
        public void Open(object model, bool isDisplacing = true)
        {
            if (isDisplacing)
            {
                while (_windowsStack.Count > 0)
                {
                    var closingWindow = _windowsStack.Pop();

                    closingWindow.Close();

                    _windowClosedObservable.Execute(closingWindow.GetType());
                }
            }

            var currentWindow = GetOrCreateWindow(model.GetType());

            currentWindow.SetOrder(_windowsStack.Count + EnvironmentOrder);

            currentWindow.Open(model);

            _windowsStack.Push(currentWindow);

            OnWindowsQueueChanged();
        }

        public void Close()
        {
            if (_windowsStack.Count > 0)
            {
                var currentWindow = _windowsStack.Pop();

                currentWindow.Close();

                _windowClosedObservable.Execute(currentWindow.GetType());

                OnWindowsQueueChanged();
            }
        }

        protected override void OnInit()
        {
            Disposable.Create(OnDisposed).AddTo(Disposables);
        }

        private WindowBase GetOrCreateWindow(Type modelType)
        {
            var instance = _instances.Find(instanceArg => instanceArg.ModelType == modelType);

            if (instance == null)
            {
                instance = CreateWindowInstance(modelType);
            }

            return instance;
        }

        private WindowBase CreateWindowInstance(Type modelType)
        {
            var prefab = _prefabs.Find(windowInstanceArg => windowInstanceArg.ModelType == modelType);

            var instance = _factory.Create(prefab, transform);

            _instances.Add(instance);

            return instance;
        }

        private void OnDisposed()
        {
            _loadingDisposable.Clear();

            while (_windowsStack.Count > 0)
            {
                Close();
            }

            foreach (var instance in _instances)
            {
                GameObject.Destroy(instance.gameObject);
            }

            _instances.Clear();
        }

        private void OnWindowsQueueChanged()
        {
            _currentWindow.Value = _windowsStack.Count > 0 ? _windowsStack.Peek() : null;
        }
    }
}