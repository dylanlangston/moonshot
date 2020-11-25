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
    class scoringInfoThree : screen
    {
        public override string Name {
            get { return "scoring info three"; }
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
            Raylib.DrawText("On Arriving at Peary", (Raylib.GetScreenWidth()-("On Arriving at Peary".Length*17))/2, 35, 30, WHITE);
            Menuline(Raylib.GetScreenWidth() / 128, Raylib.GetScreenHeight()/12);
            Menuline(Raylib.GetScreenWidth() / 128, Raylib.GetScreenHeight() / 24 * 20 );
        }
        private static void Confirmation(){
            if (PressSPACEBAR()) {
                loopCount = 0;
                MainWindow.settings.currentScreen = "welcome";
            }
        }
    }
}
