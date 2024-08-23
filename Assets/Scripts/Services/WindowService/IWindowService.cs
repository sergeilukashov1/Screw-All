namespace Services.WindowService
{
    public interface IWindowService
    {
        public void Open(object model, bool isDisplacing = true);
        public void Close();
    }
}