using Espresso; // Import Espresso namespace.

namespace Example
{
    public static class Window
    {
        public static void Create()
        {
            EsWindow window = new("Example Window"); // Create a window and set the title to "Example Window".
            
            window.Initialize(); // Initialize window.
            window.Run(); // Execute the main loop.
        }
    }
}