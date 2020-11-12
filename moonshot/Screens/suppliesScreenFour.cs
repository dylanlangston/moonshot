using Raylib_cs;
using static Raylib_cs.Raylib;
using static Raylib_cs.Color;
using System;
using System.Numerics;
using System.IO;

namespace moonshot.Screens
{
    class suppliesScreenFour : screen
    {
        public override string Name {
            get { return "Supplies Screen Four"; }
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
            Raylib.DrawText("Hello, I'm Matt. So you're going to the\nMoon! I can fix you up with what you\nneed:", Raylib.GetScreenWidth()/32*7, (Raylib.GetScreenHeight()/8), 30, WHITE);
            Raylib.DrawText("- Plenty of food for the trip", Raylib.GetScreenWidth()/18*5, Raylib.GetScreenHeight()/16*7, 30, WHITE);
            Raylib.DrawText("- Boxes to collect the rocks", Raylib.GetScreenWidth()/18*5, (Raylib.GetScreenHeight()/16*7)+65, 30, WHITE);
            Raylib.DrawText("- Spare parts for your ship", Raylib.GetScreenWidth()/18*5, (Raylib.GetScreenHeight()/16*7)+125, 30, WHITE);
            if (PressSPACEBAR()) {
                MainWindow.settings.currentScreen = "Matts Store";
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
