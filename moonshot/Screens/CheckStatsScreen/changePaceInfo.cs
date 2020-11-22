using Raylib_cs;
using static Raylib_cs.Raylib;
using static Raylib_cs.Color;
using System;
using System.Numerics;
using System.Windows.Input;

namespace moonshot.Screens
{
    class ChangePaceInfo : screen
    {
        public override string Name {
            get { return "Change Pace Info"; }
        }
        static int loopCount = 0;
        public override void Display()
        {
            ClearBackground(Colors.space);
            starscape();
            if (loopCount > 5) {
                Confirmation();
            } else {
                loopCount++;
            }
        }
        private static void Confirmation(){
            Raylib.DrawText(
@"Steady - You travel about 8 hours a day, taking
frequent rests. You take care not to get too
tired.

Strenuous - You travel about 12 hours a day.
You stop to rest only when necessary. You finish
each day feeling very tired.

Grueling - You travel about 16 hours a day. You
almost never stop to rest. You do not get enough
sleep at night. You finish each day feeling
absolutely exhausted, and your health suffers. 
"
, 15, 10, 30, WHITE);
            Raylib.DrawLine(15, 40, 100, 40, Color.SKYBLUE);
            Raylib.DrawLine(15, 41, 100, 41, Color.SKYBLUE);

            Raylib.DrawLine(15, 220, 175, 220, Color.SKYBLUE);
            Raylib.DrawLine(15, 221, 175, 221, Color.SKYBLUE);

            Raylib.DrawLine(15, 400, 120, 400, Color.SKYBLUE);
            Raylib.DrawLine(15, 401, 120, 401, Color.SKYBLUE);
            if (PressSPACEBAR()) {
                MainWindow.settings.currentScreen = "Change Pace";
                loopCount = 0;
            }
        }
    }
}
