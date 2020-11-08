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

        private static string currentScreenTempStore = "";
        public static int Init(bool debugging = true, bool resetProgress = false)
        {
            // Initialization
            //--------------------------------------------------------------------------------------

            // Count the amount of cycles to track when to run GC.Collect 
            int cleanupCounter = 0;

            // Count the amount of times tab was pressed in a certain period of time.
            int tabCounter = 0;

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

            // Reset CurrentScreen if flag has been specified
            if (resetProgress) {
                settings.currentScreen = "welcome";
            }

            //--------------------------------------------------------------------------------------
            // Main game loop
            while (!WindowShouldClose())    // Detect window close button
            {
                // Check if tab key is pressed and if so launch save menu
                tabCounter = SavetoMenu(tabCounter, cleanupCounter);

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
                bool foundScreen = false;
                foreach (screen scrn in settings.screensList) {
                    if (scrn.Name.ToLower() == settings.currentScreen.ToLower()) {
                        ((screen)scrn).Display(); // Display screen
                        foundScreen = true;
                    }
                }
                if (!foundScreen) {settings.currentScreen = "welcome";}
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

            // Save settings
            settings.SaveSettings();

            // De-Initialization
            //--------------------------------------------------------------------------------------
            CloseWindow();        // Close window and OpenGL context
            //--------------------------------------------------------------------------------------

            return 0;
        }

        private static int SavetoMenu(int tabCounter, int cleanupCounter) {
            // Check if tab key is pressed within a few seconds. If so display save screen.
            if (Raylib.IsKeyPressed(KeyboardKey.KEY_TAB)) {
                tabCounter++;
                if (tabCounter > 1) {
                    if (string.IsNullOrEmpty(currentScreenTempStore)) {
                        currentScreenTempStore = settings.currentScreen;
                        settings.currentScreen = "welcome";
                    } else {
                        settings.currentScreen = currentScreenTempStore;
                        currentScreenTempStore = "";
                        tabCounter = 0;
                    }
                }
            } else if (tabCounter > 0) {
                if (cleanupCounter % 100 == 0) {
                    tabCounter = 0;
                }
            }
            return tabCounter;
        }
    }
}