using Raylib_cs;
using static Raylib_cs.Raylib;
using static Raylib_cs.Color;
using System;
using System.Numerics;
using System.IO;
using System.Windows.Input;

namespace moonshot.Screens
{
    class suppliesScreenThree : screen
    {
        public override string Name {
            get { return "Supplies Screen Three"; }
        }
        static int loopCount = 0;
        public override void Display()
        {
            ClearBackground(Colors.space);
            starscape();
            Salesman();
            if (loopCount > 10) {
                Confirmation();
            } else {
                loopCount++;
            }
        }
        private static void Confirmation(){
            Raylib.DrawText("Hello, I'm Matt. So you're going to the\nMoon! I can fix you up with what you\nneed:", Raylib.GetScreenWidth()/32*7, (Raylib.GetScreenHeight()/8), 30, WHITE);
            Raylib.DrawText("- Oxygen tanks to provide you with\n  air", Raylib.GetScreenWidth()/18*5, Raylib.GetScreenHeight()/2, 30, WHITE);
            Raylib.DrawText("- Fuel to get you there and back ", Raylib.GetScreenWidth()/18*5, (Raylib.GetScreenHeight()/2)+95, 30, WHITE);
            if (PressSPACEBAR()) {
                MainWindow.settings.currentScreen = "Supplies Screen Four";
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
