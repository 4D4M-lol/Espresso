using Espresso; // Import Espresso namespace.
using Espresso.EspStyling; // Import Espresso.EspStyling namespace for EsColor3.

namespace Example
{
    public static class Rainbow
    {
        private static EsWindow window;
        private static double speed = 1;
        
        public static void Create()
        {
            window = new("Rainbow Window"); // Create a window and sets the title to "Rainbow Window".
            
            window.Initialize(); // Initialize window.

            window.Fill = EsColor3.Red; // Set the window background color to red (0xFF0000).
            
            window.OnRender.Connect("ChangeColor", OnRender); // Connect the OnRender function so that it would be called when the window is done rendering.
            window.Run(); // Execute the main loop.
        }

        private static void OnRender(double _)
        {
            // This function got called everytime the window done rendering.

            double now = speed * (double)window.Ticks / 1000; // Convert the ticks from ms to s.
            byte red = (byte)(128 + 127 * Math.Sin(now)); // Calculate red.
            byte green = (byte)(128 + 127 * Math.Sin(now + Double.Pi * 2 / 3)); // Calculate green.
            byte blue = (byte)(128 + 127 * Math.Sin(now + Double.Pi * 4 / 3)); // Calculate blue.
            window.Fill = new EsColor3(red, green, blue); // Changed the window background color.
        }
    }
}