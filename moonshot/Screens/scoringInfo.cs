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
    class scoringInfo : screen
    {
        public override string Name {
            get { return "Scoring Info"; }
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


            Raylib.DrawText("Your most important resource is the people you\nhave with you. You receive points for each\nmember of your party who arrives safely; you\nreceive more points if they arrive in good health!", 30, 110, 30, WHITE);

            Raylib.DrawRectangle(100, 285, 175, 65, WHITE);
            Raylib.DrawText("Health of", 115, 290, 30, BLACK);
            Raylib.DrawText("Crew", 150, 320, 30, BLACK);

            Raylib.DrawText("good", 110, 360, 30, WHITE);
            Raylib.DrawText("fair", 110, 390, 30, WHITE);
            Raylib.DrawText("poor", 110, 420, 30, WHITE);
            Raylib.DrawText("very poor", 110, 450, 30, WHITE);

            Raylib.DrawRectangle(460, 285, 180, 65, WHITE);
            Raylib.DrawText("Points per", 470, 290, 30, BLACK);
            Raylib.DrawText("Person", 495, 320, 30, BLACK);

            Raylib.DrawText("500", 520, 360, 30, WHITE);
            Raylib.DrawText("400", 520, 390, 30, WHITE);
            Raylib.DrawText("300", 520, 420, 30, WHITE);
            Raylib.DrawText("200", 520, 450, 30, WHITE);

        }
        private static void Confirmation(){
            if (PressSPACEBAR()) {
                MainWindow.settings.currentScreen = "scoring info two";
                loopCount = 0;
            }
        }
    }
}
