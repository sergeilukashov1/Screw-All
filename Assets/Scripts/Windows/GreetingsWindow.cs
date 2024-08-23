using Services.WindowService;
using UnityEngine;

namespace Windows
{
    public class GreetingsWindow : WindowBase<GreetingsWindow.Model>
    {
        public class Model
        {
        
        }

        protected override void OnOpen()
        {
            Debug.LogError("Greetings it's prototype window");
        }
    }
}