using Raylib_cs;
using static Raylib_cs.Raylib;
using static Raylib_cs.Color;
using moonshot.Screens;
using System;

namespace moonshot
{
    public class MainWindow
    {
        // Load Settings
        internal static Settings settings= new Settings();
        public static int Init(bool debugging = true)
        {
            // Initialization
            //--------------------------------------------------------------------------------------

            // Count the amount of cycles to track when to run GC.Collect 
            int cleanupCounter = 0;

            if (debugging) {
                Raylib.SetTraceLogLevel(TraceLogType.LOG_ALL);
            } else {
                Raylib.SetTraceLogLevel(TraceLogType.LOG_NONE);
            }

            // Create Window
            // 800x600 is the games internal resolution. 
            InitWindow(800, 600, "M O O N S H O T");

            // Switch to Full Screen if setting is set.
            if (!Raylib.IsWindowFullscreen() && settings.StartFullscreen)
            {
                Raylib.ToggleFullscreen();
                Raylib.HideCursor();
            }

            // Set the Exit Key to something invalid. To disable it...
            Raylib.SetExitKey(KeyboardKey.KEY_LEFT_CONTROL | KeyboardKey.KEY_RIGHT_CONTROL);

            // Set FPS Target
            SetTargetFPS(60);

            //--------------------------------------------------------------------------------------
            // Main game loop
            while (!WindowShouldClose())    // Detect window close button
            {
                // Toggle FullScreen on Escape key
                if (Raylib.IsKeyPressed(KeyboardKey.KEY_ESCAPE)) {
                    Raylib.ToggleFullscreen();
                    // Show/Hide cursor depending on if full screen or not.
                    if (Raylib.IsCursorHidden()) {
                        Raylib.ShowCursor();
                    } else {
                        Raylib.HideCursor();
                    }
                    // Update setting
                    settings.StartFullscreen = Raylib.IsWindowFullscreen();
                }
                // Draw
                //----------------------------------------------------------------------------------
                BeginDrawing();

                // Go through each screen and check if screen name matches the currentScreen. 
                // This is how we track the game state. Not sure if this is how other's would create games but it works.
                foreach (screen scrn in settings.screensList) {
                    if (scrn.Name.ToLower() == settings.currentScreen.ToLower()) {
                        ((screen)scrn).Display(); // Display screen
                    }
                }

                EndDrawing();

                // Count loops
                cleanupCounter++;

                // .NET Garbage collection very 10000 cycles
                // Probably not needed but why not.
                if (cleanupCounter > 10000) {
                    GC.Collect();
                    GC.WaitForPendingFinalizers();
                    cleanupCounter = 0;
                }

                // Exit if Running is false;
                if (!settings.Running) { break; }
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