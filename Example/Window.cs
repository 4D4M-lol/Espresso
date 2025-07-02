using Espresso; // Import Espresso namespace.

namespace Example
{
    public static class Window
    {
        public static void Main(string[] args)
        {
            EsWindow window = new("Example Window"); // Create a window and sets the title to "Example Window".
            
            window.Run(); // Execute the main loop.
        }
    }
}