using Raylib_cs;
using static Raylib_cs.Raylib;
using static Raylib_cs.Color;
using System;
using System.Numerics;
using System.IO;

namespace moonshot.Screens
{
    class LeavingMattsStore : screen
    {
        public override string Name {
            get { return "Leaving Matts Store"; }
        }
        static int loopCount = 0;
        public override void Display()
        {
            ClearBackground(Colors.space);
            starscape();
            Salesman();
            if (loopCount > 5) {
                Confirmation();
            } else {
                loopCount++;
            }
        }
        private static void Confirmation(){
            Raylib.DrawText("Well then, you're ready to start.\nGood luck! You have a long and\ndifficult journey ahead of you.", Raylib.GetScreenWidth()/3, (Raylib.GetScreenHeight()/5*2), 30, WHITE);
            if (PressSPACEBAR()) {
                MainWindow.settings.currentScreen = "Leaving Cape Kennedy";
                loopCount = 0;
            }
        }
        // Salesman
        internal static Texture2D salesmanTexture = new Texture2D();
        internal static void Salesman()
        {
            if (salesmanTexture.height == 0) {
                Image img = LoadImage(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Images/salesman.png"));
                salesmanTexture = LoadTextureFromImage(img);
                UnloadImage(img);
            }
            DrawTextureEx(salesmanTexture, new Vector2(0, 0), 0f, 1f, WHITE);
        }
    }
}
