using Raylib_cs;
using static Raylib_cs.Raylib;
using static Raylib_cs.Color;
using System;
using System.Numerics;
using System.IO;

namespace moonshot.Screens
{
    class mattsStoreShipParts : screen
    {
        public override string Name {
            get { return "Matts Store Ship Parts"; }
        }
        public override void Display()
        {
            ClearBackground(Colors.space);
            starscape();
            Salesman();
            Title();
            if (PressSPACEBAR()) {
                MainWindow.settings.currentScreen = "Matts Store";
            }
        }
        private static void Title()
        {
            Raylib.DrawLineV(new Vector2(220, 25), new Vector2(Raylib.GetScreenWidth()-20, 25), RED);
            Raylib.DrawLineV(new Vector2(220, 26), new Vector2(Raylib.GetScreenWidth()-20, 26), RED);
            Raylib.DrawLineV(new Vector2(220, 27), new Vector2(Raylib.GetScreenWidth()-20, 27), RED);
            Raylib.DrawLineV(new Vector2(220, 28), new Vector2(Raylib.GetScreenWidth()-20, 28), RED);
            Raylib.DrawLineV(new Vector2(220, 29), new Vector2(Raylib.GetScreenWidth()-20, 29), RED);
            Raylib.DrawLineV(new Vector2(220, 30), new Vector2(Raylib.GetScreenWidth()-20, 30), RED);
            Raylib.DrawLineV(new Vector2(220, 31), new Vector2(Raylib.GetScreenWidth()-20, 31), RED);
            Raylib.DrawText("Matt's Commissary", Raylib.GetScreenWidth()/20*9, Raylib.GetScreenHeight()/12 - 10, 30, WHITE);
            Raylib.DrawText("Cape Kennedy, Florida", Raylib.GetScreenWidth()/80*33, Raylib.GetScreenHeight()/12 + 20, 30, WHITE);
            Raylib.DrawLineV(new Vector2(220, 110), new Vector2(Raylib.GetScreenWidth()-20, 110), RED);
            Raylib.DrawLineV(new Vector2(220, 111), new Vector2(Raylib.GetScreenWidth()-20, 111), RED);
            Raylib.DrawLineV(new Vector2(220, 112), new Vector2(Raylib.GetScreenWidth()-20, 112), RED);
            Raylib.DrawLineV(new Vector2(220, 113), new Vector2(Raylib.GetScreenWidth()-20, 113), RED);
            Raylib.DrawLineV(new Vector2(220, 114), new Vector2(Raylib.GetScreenWidth()-20, 114), RED);
            Raylib.DrawLineV(new Vector2(220, 115), new Vector2(Raylib.GetScreenWidth()-20, 115), RED);
            Raylib.DrawLineV(new Vector2(220, 116), new Vector2(Raylib.GetScreenWidth()-20, 116), RED);
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
