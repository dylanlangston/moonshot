﻿using Raylib_cs;
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
            Raylib.DrawLineV(new Vector2(220, 25), new Vector2(Raylib.GetScreenWidth()-20, 25), RED);
            Raylib.DrawLineV(new Vector2(220, 26), new Vector2(Raylib.GetScreenWidth()-20, 26), RED);
            Raylib.DrawLineV(new Vector2(220, 27), new Vector2(Raylib.GetScreenWidth()-20, 27), RED);
            Raylib.DrawLineV(new Vector2(220, 28), new Vector2(Raylib.GetScreenWidth()-20, 28), RED);
            Raylib.DrawLineV(new Vector2(220, 29), new Vector2(Raylib.GetScreenWidth()-20, 29), RED);
            Raylib.DrawLineV(new Vector2(220, 30), new Vector2(Raylib.GetScreenWidth()-20, 30), RED);
            Raylib.DrawLineV(new Vector2(220, 31), new Vector2(Raylib.GetScreenWidth()-20, 31), RED);
            Raylib.DrawText("Matt's Commissary", Raylib.GetScreenWidth()/20*9, Raylib.GetScreenHeight()/12 - 10, 30, WHITE);
            Raylib.DrawText("Cape Kennedy, Florida", Raylib.GetScreenWidth()/80*33, Raylib.GetScreenHeight()/12 + 20, 30, WHITE);
            string launchDate = PlayerType.GetLaunchDate(MainWindow.settings.userStats.playerType).ToString("MMMM dd, yyyy");
            Raylib.DrawText(launchDate, (Raylib.GetScreenWidth()-(launchDate.Length*20)), Raylib.GetScreenHeight()/12 + 70, 30, WHITE);
            Raylib.DrawLineV(new Vector2(220, 160), new Vector2(Raylib.GetScreenWidth()-20, 160), RED);
            Raylib.DrawLineV(new Vector2(220, 161), new Vector2(Raylib.GetScreenWidth()-20, 161), RED);
            Raylib.DrawLineV(new Vector2(220, 162), new Vector2(Raylib.GetScreenWidth()-20, 162), RED);
            Raylib.DrawLineV(new Vector2(220, 163), new Vector2(Raylib.GetScreenWidth()-20, 163), RED);
            Raylib.DrawLineV(new Vector2(220, 164), new Vector2(Raylib.GetScreenWidth()-20, 164), RED);
            Raylib.DrawLineV(new Vector2(220, 165), new Vector2(Raylib.GetScreenWidth()-20, 165), RED);
            Raylib.DrawLineV(new Vector2(220, 166), new Vector2(Raylib.GetScreenWidth()-20, 166), RED);

            Raylib.DrawText("1.  Oxygen", Raylib.GetScreenWidth()/3, Raylib.GetScreenHeight()/24*7, 30, WHITE);
            Raylib.DrawText("2. Fuel", Raylib.GetScreenWidth()/3, Raylib.GetScreenHeight()/24*7+35, 30, WHITE);
            Raylib.DrawText("3. Food", Raylib.GetScreenWidth()/3, Raylib.GetScreenHeight()/24*7+70, 30, WHITE);
            Raylib.DrawText("4. Boxes", Raylib.GetScreenWidth()/3, Raylib.GetScreenHeight()/24*7+105, 30, WHITE);
            Raylib.DrawText("5. Space Parts", Raylib.GetScreenWidth()/3, Raylib.GetScreenHeight()/24*7+140, 30, WHITE);

            Raylib.DrawLineV(new Vector2(220, 355), new Vector2(Raylib.GetScreenWidth()-20, 355), RED);
            Raylib.DrawLineV(new Vector2(220, 356), new Vector2(Raylib.GetScreenWidth()-20, 356), RED);
            Raylib.DrawLineV(new Vector2(220, 357), new Vector2(Raylib.GetScreenWidth()-20, 357), RED);
            Raylib.DrawLineV(new Vector2(220, 358), new Vector2(Raylib.GetScreenWidth()-20, 358), RED);
            Raylib.DrawLineV(new Vector2(220, 359), new Vector2(Raylib.GetScreenWidth()-20, 359), RED);
            Raylib.DrawLineV(new Vector2(220, 360), new Vector2(Raylib.GetScreenWidth()-20, 360), RED);
            Raylib.DrawLineV(new Vector2(220, 361), new Vector2(Raylib.GetScreenWidth()-20, 361), RED);

            Raylib.DrawText("Total Bill:", Raylib.GetScreenWidth()/2, Raylib.GetScreenHeight()/96*61, 30, WHITE);

            //MainWindow.settings.userStats.inventory.
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
