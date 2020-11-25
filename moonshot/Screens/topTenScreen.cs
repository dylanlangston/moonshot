using Raylib_cs;
using static Raylib_cs.Raylib;
using static Raylib_cs.Color;
using System;
using System.Numerics;
using System.Windows.Input;
using System.IO;
using System.Linq;
using System.Threading;
using System.Diagnostics;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using static System.Environment;

namespace moonshot.Screens
{
    class topTenScreen : screen
    {
        public override string Name {
            get { return "Top Ten"; }
        }
        public override void Display()
        {
            ClearBackground(Colors.space);
            starscape();
            DisplayTopTen();
        }
        internal static void DisplayTopTen() {
            Raylib.DrawLineV(new Vector2(0, 27), new Vector2(Raylib.GetScreenWidth(), 27), BLUE);
            Raylib.DrawLineV(new Vector2(0, 28), new Vector2(Raylib.GetScreenWidth(), 28), BLUE);
            Raylib.DrawLineV(new Vector2(0, 29), new Vector2(Raylib.GetScreenWidth(), 29), BLUE);
            Raylib.DrawText("The Moon Top Ten", (Raylib.GetScreenWidth()-("The Moon Top Ten".Length*17))/2, 45, 30, WHITE);
            Raylib.DrawLineV(new Vector2(0, 92), new Vector2(Raylib.GetScreenWidth(), 92), BLUE);
            Raylib.DrawLineV(new Vector2(0, 93), new Vector2(Raylib.GetScreenWidth(), 93), BLUE);
            Raylib.DrawLineV(new Vector2(0, 94), new Vector2(Raylib.GetScreenWidth(), 94), BLUE);

            Raylib.DrawText("Name", 100, 110, 30, WHITE);
            Raylib.DrawText("Points", 390, 110, 30, WHITE);

            for(int i = 0;i < MainWindow.settings.topTen.Leaders.Count;i++)
            {
                Raylib.DrawText(MainWindow.settings.topTen.Leaders[i].Item1, 30, 160 + (i * 30), 30, WHITE);
                Raylib.DrawText(MainWindow.settings.topTen.Leaders[i].Item2.ToString(), 400, 160 + (i * 30), 30, WHITE);
            }
        }
    }
}
