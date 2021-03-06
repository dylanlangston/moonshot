﻿using Raylib_cs;
using static Raylib_cs.Raylib;
using static Raylib_cs.Color;
using moonshot.Screens;
using System;
using System.Threading;
using System.Collections.Generic;
using System.Windows;
using System.IO;

namespace moonshot
{
    public class MainWindow
    {
        
        // Custom logging function
        private static void LogCustom(TraceLogType msgType, string text, IntPtr arg) 
        {
            using (System.IO.StreamWriter file =
                new System.IO.StreamWriter(Path.Combine(Program.AppDataFolder(), @"moonshot-raylib.log"), true))
            {
                file.WriteLine(text);
            }
        }
        
        // Load Settings
        internal static Settings settings= new Settings();

        internal static string currentScreenTempStore = "";
        public static int Init(bool debugging = true, bool resetProgress = false, bool raylibLogging = false, bool displayFPS = false)
        {
            // Initialization
            //--------------------------------------------------------------------------------------
            bool firstRun = true;

            // Count the amount of cycles to track when to run GC.Collect 
            int cleanupCounter = 0;

            // Count the amount of times tab was pressed in a certain period of time.
            int tabCounter = 0;

            if (debugging) {
                Raylib.SetTraceLogLevel(TraceLogType.LOG_ALL);    
            } else {
                Raylib.SetTraceLogLevel(TraceLogType.LOG_ERROR);
            }

            // Custom Logging
            if (raylibLogging) {
                Raylib.SetTraceLogLevel(TraceLogType.LOG_ALL);
                Raylib.SetTraceLogCallback(LogCustom);
            }

            // Reset to defaults
            if (resetProgress) {
                settings = new Settings(true);
            }


            // Create Window
            // 800x600 is the games internal resolution. 
            InitWindow(800, 600, "M O O N S H O T");


            Image icon = LoadImage(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Images/moon.png"));
            Raylib.ImageResize(ref icon, 100, 100);
            Raylib.SetWindowIcon(icon);


            // Enable Sound
            InitAudioDevice();

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

            // Set currentScreenTempStore to the currentScreen if savedProgress is true
            if (settings.savedProgress) {
                currentScreenTempStore = settings.currentScreen;
            }

            //--------------------------------------------------------------------------------------
            // Main game loop
            while (!WindowShouldClose())    // Detect window close button
            {
                if (displayFPS)
                    Raylib.DrawFPS(0,0);

                // Check if tab key is pressed and if so launch save menu
                if (!settings.nonGameScreens.Contains(settings.currentScreen.ToLower())) 
                    tabCounter = SavetoMenu(tabCounter, cleanupCounter);

                // Toggle FullScreen on Escape key
                ToggleFS();

                // Draw
                //----------------------------------------------------------------------------------
                BeginDrawing();

                // Splash Screen on initial launch
                if (firstRun)
                {
                    (new splashScreen()).Display();
                    if (cleanupCounter > 150)
                    {
                        firstRun = false;
                    }
                } else if (Raylib.GetScreenWidth() > 800) {
                    Raylib.ClearBackground(BLACK);
                    Raylib.DrawText("Screen resize not working!\nPlease change resolution to 800x600 manually.", 30, 30, 30, WHITE);
                } else
                {
                    // Go through each screen and check if screen name matches the currentScreen. 
                    // This is how we track the game state. Not sure if this is how other's would create games but it works.
                    bool foundScreen = false;
                    foreach (screen scrn in settings.screensList)
                    {
                        if (settings.savedProgress == true ) {
                            if (settings.nonGameScreens.Contains(scrn.Name.ToLower()) && settings.nonGameScreens.Contains(settings.currentScreen.ToLower()))
                            {
                                if (settings.currentScreen.ToLower() == "avoid meteor game")
                                {}
                                else if (settings.currentScreen.ToLower() == "collect rocks game")
                                {}
                                else if (settings.currentScreen.ToLower() == scrn.Name.ToLower()) {
                                    ((screen)scrn).Display(); // Display screen
                                    foundScreen = true;
                                }
                            } 
                        } else {
                            if (scrn.Name.ToLower() == settings.currentScreen.ToLower())
                            {
                                ((screen)scrn).Display(); // Display screen
                                foundScreen = true;
                            }
                        }
                    }
                    if (!foundScreen) { settings.currentScreen = "welcome"; }
                }
                EndDrawing();

                // Count loops
                cleanupCounter++;

                // .NET Garbage collection every 10000 cycles
                // Probably not needed but why not?
                if (cleanupCounter > 10000) {
                    GC.Collect();
                    GC.WaitForPendingFinalizers();
                    cleanupCounter = 0;
                }

                // Exit if Running is false;
                if (!settings.Running) { break; }
                //----------------------------------------------------------------------------------

                //Console.WriteLine(settings.userStats.ToString());
            }

            // Save settings
            if (settings.nonGameScreens.Contains(settings.currentScreen.ToLower())) {
                if (!string.IsNullOrEmpty(currentScreenTempStore)) {
                    settings.currentScreen = currentScreenTempStore;
                }
                else if (settings.currentScreen.ToLower() == "avoid meteor game")
                {}
                else if (settings.currentScreen.ToLower() == "collect rocks game")
                {}
                else if (settings.currentScreen.ToLower() == "repair ship")
                    settings.currentScreen = "main trail";
                else
                    settings.currentScreen = "";         
            }
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
                if ((tabCounter > 1) && string.IsNullOrEmpty(currentScreenTempStore)) {
                    currentScreenTempStore = settings.currentScreen;
                    settings.currentScreen = "save";
                    tabCounter = 0;
                }
            } else if (tabCounter > 0) {
                if (cleanupCounter % 100 == 0) {
                    tabCounter = 0;
                }
            }
            return tabCounter;
        }

        private static void ToggleFS()
        {
            // Toggle FullScreen on Escape key
            if (Raylib.IsKeyPressed(KeyboardKey.KEY_ESCAPE))
            {
                Raylib.ToggleFullscreen();
                Raylib.SetWindowSize(800,600);
                // Show/Hide cursor depending on if full screen or not.
                if (Raylib.IsCursorHidden())
                {
                    Raylib.ShowCursor();
                }
                else
                {
                    Raylib.HideCursor();
                }
                // Update setting
                settings.StartFullscreen = Raylib.IsWindowFullscreen();
            }
        }
    }
}