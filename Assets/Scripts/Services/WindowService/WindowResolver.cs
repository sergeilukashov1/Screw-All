using Windows;

namespace Services.WindowService
{
    //Resolver for getting models of windows 
    public class WindowResolver
    {
        public GreetingsWindow.Model GetGreetingsWindow()
        {
            return new GreetingsWindow.Model();
        }
    }
}