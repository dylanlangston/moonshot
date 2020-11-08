using Raylib_cs;
using static Raylib_cs.Raylib;
using static Raylib_cs.Color;
using moonshot.Screens;
using System.Collections.Generic;

namespace moonshot
{
    public class MainWindow
    {
        public static bool Running = true;
        public static string currentScreen = "welcome";
        private static List<screen> screensList = new List<screen>(){ 
            new welcome(),
            new chooseCharacter()
        };
        public static int Init()
        {
            // Initialization
            //--------------------------------------------------------------------------------------

            // Full Screen
            InitWindow(800, 600, "moonshot");
            if (!Raylib.IsWindowFullscreen() && Settings.StartFullscreen)
            {
                Raylib.ToggleFullscreen();
                Raylib.HideCursor();
            }
            Raylib.SetExitKey(KeyboardKey.KEY_LEFT_CONTROL | KeyboardKey.KEY_RIGHT_CONTROL);
            // Set FPS Target
            SetTargetFPS(60);
            //--------------------------------------------------------------------------------------
            // Main game loop
            while (!WindowShouldClose())    // Detect window close button or ESC key
            {
                // Toggle FullScreen
                if (Raylib.IsKeyPressed(KeyboardKey.KEY_ESCAPE)) {
                    Raylib.ToggleFullscreen();
                    if (Raylib.IsCursorHidden()) {
                        Raylib.ShowCursor();
                    } else {
                        Raylib.HideCursor();
                    }
                }
                // Draw
                //----------------------------------------------------------------------------------
                BeginDrawing();

                foreach (screen scrn in screensList) {
                    if (scrn.Name.ToLower() == currentScreen.ToLower()) {
                        ((screen)scrn).Display();
                    }
                }

                EndDrawing();
                //----------------------------------------------------------------------------------
                if (!Running) { break; }
            }

            // De-Initialization
            //--------------------------------------------------------------------------------------
            CloseWindow();        // Close window and OpenGL context
            //--------------------------------------------------------------------------------------

            return 0;
        }
    }
}