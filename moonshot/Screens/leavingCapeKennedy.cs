using Raylib_cs;
using static Raylib_cs.Raylib;
using static Raylib_cs.Color;
using System;
using System.Numerics;
using System.Windows.Input;
using System.IO;

namespace moonshot.Screens
{
    class leavingCapeKennedy : screen
    {
        public override string Name {
            get { return "Leaving Cape Kennedy"; }
        }
        public override void Display()
        {
            ClearBackground(Colors.space);
            starscape();
            Spacecenter();
            //Confirmation();
        }
        private static void Confirmation(){
            if (PressSPACEBAR()) {
                MainWindow.settings.currentScreen = "Welcome";
            }
        }

        // space center
        internal static Sound sound = LoadSound("Music/POL-moon-castle-short.wav");
        internal static Texture2D spacecenterTexture = new Texture2D();
        internal static void Spacecenter()
        {
            if (spacecenterTexture.height == 0) {
                Image img = LoadImage(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Images/spaceCenter.png"));
                spacecenterTexture = LoadTextureFromImage(img);
                UnloadImage(img);
            }
            DrawTextureEx(spacecenterTexture, new Vector2(0, 0), 0f, 1f, WHITE);
            if (!Raylib.IsSoundPlaying(sound)) 
                Raylib.PlaySound(sound);
        }
    }
}
