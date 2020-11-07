using Raylib_cs;
using static Raylib_cs.Raylib;
using static Raylib_cs.Color;
using System;

namespace moonshot.Screens
{
    class welcome : screen
    {
        public override void Display()
        {
            starscape();
            DrawText("Welcome to Moonshot!" + Settings.DefaultWidth, 220, 200, 40, MAROON);
        }
        private static void starscape()
        {
            int Width = GetScreenWidth();
            int Height = GetScreenHeight();

            for (int w = 0; w < Width; w=w+10)
            {
                for (int h = 0; h < Height; h=h+10)
                {
                    Raylib.DrawCircle(h, w, 1, WHITE);
                }
            }
        }
    }
}
