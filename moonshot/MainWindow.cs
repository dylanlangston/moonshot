using Raylib_cs;
using static Raylib_cs.Raylib;
using static Raylib_cs.Color;
using moonshot.Screens;

namespace moonshot
{
    public class MainWindow
    {
        public static int Init()
        {
            // Initialization
            //--------------------------------------------------------------------------------------

            // Full Screen
            InitWindow(Settings.DefaultWidth, Settings.DefaultHeight, "moonshot");
            if (!Raylib.IsWindowFullscreen() && Settings.StartFullscreen)
            {
                //Raylib.ToggleFullscreen();
            }

            // Set FPS Target
            SetTargetFPS(60);
            //--------------------------------------------------------------------------------------

            // Main game loop
            while (!WindowShouldClose())    // Detect window close button or ESC key
            {
                // Update
                //----------------------------------------------------------------------------------
                // TODO: Update your variables here
                //----------------------------------------------------------------------------------

                // Draw
                //----------------------------------------------------------------------------------
                BeginDrawing();

                Raylib.DisableCursor();
                new welcome();

                EndDrawing();
                //----------------------------------------------------------------------------------
            }

            // De-Initialization
            //--------------------------------------------------------------------------------------
            CloseWindow();        // Close window and OpenGL context
            //--------------------------------------------------------------------------------------

            return 0;
        }
    }
}