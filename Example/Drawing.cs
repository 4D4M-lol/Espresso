using Espresso; // Import Espresso namespace.
using Espresso.EspInterface; // Import Espresso.EspInterface for EsCanvas and EsTriangle.
using Espresso.EspStyling; // Import Espresso.EspStyling for EsColor3

namespace Example
{
    public static class Drawing
    {
        public static void Create()
        {
            EsWindow window = new("Drawing Window", new(800, 800)); // Create a window and sets the title to "Drawing Window" and the size to 800x800.
            
            window.Initialize(); // Initialize the window.

            window.Fill = new EsColor3(EsColors.SlateBlue); // Set the window background color to SlateBlue (0x6970061).
            
            EsCanvas canvas = new(window); // Create a new canvas for drawing and set the parent to the window.

            canvas.Size = new(0.5f, 0.5f); // Sets the canvas width and height to 50% of the parent width.
            canvas.Position = new(0.25f, 0.25f); // Center the canvas on the window (1 - Size / 2).
            canvas.BackgroundColor = new EsColor3(EsColors.MediumSlateBlue); // Set the canvas background color to MediumSlateBlue (0x8087790).
            canvas.ScaleRule = EsScaleRule.Stretch; // Set the canvas scale rule to Stretch, so all the drawing in it got stretched when the canvas size changed.

            EsTriangle triangle = new(
                EsTriangleType.Equilateral, new(200, 200), new(100, 100), fill: new EsColor3(EsColors.DarkSlateBlue)
            ); // Create a new equilateral triangle drawing with the size of 200x200 and a DarkSlateBlue (0x4734347) color in the center of the canvas.

            canvas.Draw(triangle); // Draw the triangle on the canvas.
            window.Run(); // Execute the main loop.
        }
    }
}