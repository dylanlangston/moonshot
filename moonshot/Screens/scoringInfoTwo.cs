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
    class scoringInfoTwo : screen
    {
        public override string Name {
            get { return "Scoring Info Two"; }
        }
        static int loopCount = 0;
        public override void Display()
        {
            ClearBackground(Colors.space);
            starscape();
            ScoreInfo();
            if (loopCount > 5) {
                Confirmation();
            } else {
                loopCount++;
            }
        }
        private static string selection = String.Empty;
        private static void ScoreInfo() {
            Raylib.DrawLineV(new Vector2(0, 27), new Vector2(Raylib.GetScreenWidth(), 27), BLUE);
            Raylib.DrawLineV(new Vector2(0, 28), new Vector2(Raylib.GetScreenWidth(), 28), BLUE);
            Raylib.DrawLineV(new Vector2(0, 29), new Vector2(Raylib.GetScreenWidth(), 29), BLUE);
            Raylib.DrawText("On Arriving at Peary", (Raylib.GetScreenWidth()-("On Arriving at Peary".Length*17))/2, 45, 30, WHITE);
            Raylib.DrawLineV(new Vector2(0, 92), new Vector2(Raylib.GetScreenWidth(), 92), BLUE);
            Raylib.DrawLineV(new Vector2(0, 93), new Vector2(Raylib.GetScreenWidth(), 93), BLUE);
            Raylib.DrawLineV(new Vector2(0, 94), new Vector2(Raylib.GetScreenWidth(), 94), BLUE);

            Raylib.DrawText(
@"Mission control likes it when you return with a
surplus of items. They will reward you with
points for each item you bring safely back.", 30, 110, 30, WHITE);

            Raylib.DrawRectangle(100, 245, 235, 65, WHITE);
            Raylib.DrawText("Resources of", 115, 250, 30, BLACK);
            Raylib.DrawText("Crew", 180, 280, 30, BLACK);

            Raylib.DrawText("ship", 110, 330, 30, WHITE);
            Raylib.DrawText("oxygen", 110, 360, 30, WHITE);
            Raylib.DrawText("spare ship part", 110, 390, 30, WHITE);
            Raylib.DrawText("rock boxes", 110, 420, 30, WHITE);
            Raylib.DrawText("fuel (each 10 gallons)", 110, 450, 30, WHITE);
            Raylib.DrawText("food (each 20 pounds)", 110, 480, 30, WHITE);
            Raylib.DrawText("cash (each 5 dollars)", 110, 510, 30, WHITE);

            Raylib.DrawRectangle(460, 245, 180, 65, WHITE);
            Raylib.DrawText("Points per", 470, 250, 30, BLACK);
            Raylib.DrawText("Item", 515, 280, 30, BLACK);

            Raylib.DrawText("50", 510, 330, 30, WHITE);
            Raylib.DrawText("4", 510, 360, 30, WHITE);
            Raylib.DrawText("2", 510, 390, 30, WHITE);
            Raylib.DrawText("2", 510, 420, 30, WHITE);
            Raylib.DrawText("1", 510, 450, 30, WHITE);
            Raylib.DrawText("1", 510, 480, 30, WHITE);
            Raylib.DrawText("1", 510, 510, 30, WHITE);
        }
        private static void Confirmation(){
            if (PressSPACEBAR()) {
                loopCount = 0;
                MainWindow.settings.currentScreen = "scoring info three";
            }
        }
    }
}
