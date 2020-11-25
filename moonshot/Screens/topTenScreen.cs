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
        private static string selection = String.Empty;
        private static void DisplayTopTen() {
            Raylib.DrawLineV(new Vector2(0, 27), new Vector2(Raylib.GetScreenWidth(), 27), BLUE);
            Raylib.DrawLineV(new Vector2(0, 28), new Vector2(Raylib.GetScreenWidth(), 28), BLUE);
            Raylib.DrawLineV(new Vector2(0, 29), new Vector2(Raylib.GetScreenWidth(), 29), BLUE);
            Raylib.DrawText("The Moon Top Ten", (Raylib.GetScreenWidth()-("The Moon Top Ten".Length*17))/2, 45, 30, WHITE);
            Raylib.DrawLineV(new Vector2(0, 92), new Vector2(Raylib.GetScreenWidth(), 92), BLUE);
            Raylib.DrawLineV(new Vector2(0, 93), new Vector2(Raylib.GetScreenWidth(), 93), BLUE);
            Raylib.DrawLineV(new Vector2(0, 94), new Vector2(Raylib.GetScreenWidth(), 94), BLUE);

            Raylib.DrawText("Name", 100, 110, 30, WHITE);
            Raylib.DrawText("Points", 390, 110, 30, WHITE);
            Raylib.DrawText("Rating", 600, 110, 30, WHITE);

            Raylib.DrawText("Commander", 575, 160, 30, WHITE);
            Raylib.DrawText("Command Pilot", 575, 190, 30, WHITE);
            Raylib.DrawText("Command Pilot", 575, 220, 30, WHITE);
            Raylib.DrawText("Pilot", 575, 250, 30, WHITE);
            Raylib.DrawText("Pilot", 575, 280, 30, WHITE);
            Raylib.DrawText("Pilot", 575, 310, 30, WHITE);
            Raylib.DrawText("Pilot", 575, 340, 30, WHITE);
            Raylib.DrawText("Pilot", 575, 370, 30, WHITE);
            Raylib.DrawText("Pilot", 575, 400, 30, WHITE);
            Raylib.DrawText("Pilot", 575, 430, 30, WHITE);

            for(int i = 0;i < MainWindow.settings.topTen.Leaders.Count;i++)
            {
                Raylib.DrawText(MainWindow.settings.topTen.Leaders[i].Item1, 30, 160 + (i * 30), 30, WHITE);
                Raylib.DrawText(MainWindow.settings.topTen.Leaders[i].Item2.ToString(), 400, 160 + (i * 30), 30, WHITE);
            }

            Raylib.DrawRectangleRounded(new Rectangle(Raylib.GetScreenWidth()/8, Raylib.GetScreenHeight()/10*8, Raylib.GetScreenWidth()/8*6, 100), 0.25f, 10, WHITE);
            Raylib.DrawRectangle(Raylib.GetScreenWidth()/8+10, Raylib.GetScreenHeight()/10*8+10, Raylib.GetScreenWidth()/8*6-20, 80, BLACK);

            Raylib.DrawText("Would you like to see how points are", Raylib.GetScreenWidth()/8+20, Raylib.GetScreenHeight()/10*8+20, 30, WHITE);
            Raylib.DrawText("earned? " + selection + "_", Raylib.GetScreenWidth()/8+20, Raylib.GetScreenHeight()/10*8+50, 30, WHITE);

            int keypress = Raylib.GetKeyPressed();
            if (Raylib.IsKeyPressed(KeyboardKey.KEY_BACKSPACE)) {
                keypress = 9000;
            } else if (Raylib.IsKeyPressed(KeyboardKey.KEY_ENTER)) {
                switch (selection) {
                    case "y":
                        MainWindow.settings.currentScreen = "Scoring Info";
                        break;
                    case "n":
                        MainWindow.settings.currentScreen = "welcome";
                        break;
                    default:
                        break;
                }
                selection = "";
            }
            switch (keypress){
                case 121:
                    selection = "y";
                    break;
                case 110:
                    selection = "n";
                    break;
                case 9000:
                    selection = String.Empty;
                    break;
                default:
                    break;
            }
        }
    }
}
