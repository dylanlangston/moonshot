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

        }
        private static void Confirmation(){
            if (PressSPACEBAR()) {
                loopCount = 0;
                MainWindow.settings.currentScreen = "scoring info three";
            }
        }
    }
}
