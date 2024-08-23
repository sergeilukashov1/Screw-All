namespace Core.StateManagement
{
    public interface IGameState
    {
        void Init(string sceneName);
        void Deinit();
    }

    public abstract class GameStateBase<T> : IGameState where T : struct
    {
        void IGameState.Init(string sceneName)
        {
            Init(sceneName);
        }

        void IGameState.Deinit()
        {
            Deinit();
        }

        protected abstract void Init(string sceneName);
        protected abstract void Deinit();
    }
}