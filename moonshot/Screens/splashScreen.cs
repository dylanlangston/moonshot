using Raylib_cs;
using static Raylib_cs.Raylib;
using static Raylib_cs.Color;
using System;
using System.Numerics;
using System.Windows.Input;
using System.IO;

namespace moonshot.Screens
{
    class splashScreen : screen
    {
        public override string Name {
            get { return "SplashScreen"; }
        }
        public override void Display()
        {
            ClearBackground(WHITE);
            SplashScreen();
        }
        // Splash Screen
        internal static Texture2D SplashScreenTexture = new Texture2D();
        internal static void SplashScreen()
        {
            if (SplashScreenTexture.height == 0)
            {
                Image splashscreen = LoadImage(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Images/splashscreen.png"));
                SplashScreenTexture = LoadTextureFromImage(splashscreen);
                UnloadImage(splashscreen);
            }
            DrawTexture(SplashScreenTexture, 0, 0, WHITE);
        }
    }
}
