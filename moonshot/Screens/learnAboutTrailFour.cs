using Raylib_cs;
using static Raylib_cs.Raylib;
using static Raylib_cs.Color;
using System;
using System.Numerics;
using System.Collections.Generic;
using System.IO;

namespace moonshot.Screens
{
    class learnAboutTrailFour : screen
    {
        public override string Name {
            get { return "Learn About Trail Four"; }
        }
        private static int loopCount = 0;
        public override void Display()
        {
            ClearBackground(Colors.space);
            starscape();
            Moon();
            Menuline(Raylib.GetScreenWidth() / 128, Raylib.GetScreenHeight() / 6);
            Menuline(Raylib.GetScreenWidth() / 128, Raylib.GetScreenHeight() / 24 * 20 );
            MoonshotLogo();
            Message();
            if (loopCount > 5) {
                Confirmation();
            } else {
                loopCount++;
            }
        }
        private static void Confirmation(){
            if (PressSPACEBAR()) {
                MainWindow.settings.currentScreen = "Learn About Trail Five";
                loopCount = 0;
            }
        }
        private static void Message()
        {
            Raylib.DrawText(
@"If for some reason you don't survive -- your
rocket gets destroyed, or you run out of
provisions, or you die of oxygen deprivation --
don't give up! Try again... and again... until
your name is up with the others on
The Moon Top Ten.", 30, 250, 30, WHITE);
        }
        // Moon
        private static Texture2D moonTexture = new Texture2D();
        private static void Moon()
        {
            if (moonTexture.height == 0) {
                Image moon = LoadImage(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Images/moon.png"));
                moonTexture = LoadTextureFromImage(moon);
                UnloadImage(moon);
            }
            DrawTextureEx(moonTexture, new Vector2(Raylib.GetScreenHeight()/4*3, 0), 0f, 0.10f, WHITE);
        }
    }
}
