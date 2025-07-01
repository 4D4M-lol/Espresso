using Espresso;
using Espresso.EspInterface;
using Espresso.EspMath;

namespace Example
{
    public static class Window
    {
        public static void Main(string[] args)
        {
            EsWindow window = new(null, new(800, 800));
            EsFrame frame = new(window);
            
            window.Run();
        }
    }
}