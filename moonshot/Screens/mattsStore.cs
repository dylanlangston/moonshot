using Raylib_cs;
using static Raylib_cs.Raylib;
using static Raylib_cs.Color;
using System;
using System.Numerics;
using System.IO;

namespace moonshot.Screens
{
    class mattsStore : screen
    {
        public override string Name {
            get { return "Matts Store"; }
        }
        public override void Display()
        {
            ClearBackground(Colors.space);
            starscape();
            Salesman();
            Store();
        }
        private static void Store()
        {
            Raylib.DrawLineV(new Vector2(220, 30), new Vector2(Raylib.GetScreenWidth()-20, 30), RED);
            Raylib.DrawLineV(new Vector2(220, 31), new Vector2(Raylib.GetScreenWidth()-20, 31), RED);
            Raylib.DrawLineV(new Vector2(220, 32), new Vector2(Raylib.GetScreenWidth()-20, 32), RED);
            Raylib.DrawLineV(new Vector2(220, 33), new Vector2(Raylib.GetScreenWidth()-20, 33), RED);
            Raylib.DrawLineV(new Vector2(220, 34), new Vector2(Raylib.GetScreenWidth()-20, 34), RED);
            Raylib.DrawLineV(new Vector2(220, 35), new Vector2(Raylib.GetScreenWidth()-20, 35), RED);
            Raylib.DrawLineV(new Vector2(220, 36), new Vector2(Raylib.GetScreenWidth()-20, 36), RED);
            Raylib.DrawText("Matt's Commissary", Raylib.GetScreenWidth()/20*9, Raylib.GetScreenHeight()/12, 30, WHITE);
            Raylib.DrawText("Cape Kennedy, Florida", Raylib.GetScreenWidth()/80*33, Raylib.GetScreenHeight()/12 + 30, 30, WHITE);
            Raylib.DrawText(PlayerType.GetLaunchDate(MainWindow.settings.userStats.playerType).ToString("MMMM dd, yyyy"), Raylib.GetScreenWidth()/3*2, Raylib.GetScreenHeight()/12 + 80, 30, WHITE);
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
