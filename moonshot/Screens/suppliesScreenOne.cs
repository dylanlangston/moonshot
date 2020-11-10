using Raylib_cs;
using static Raylib_cs.Raylib;
using static Raylib_cs.Color;
using System;
using System.Numerics;
using System.Windows.Input;

namespace moonshot.Screens
{
    class suppliesScreenOne : screen
    {
        public override string Name {
            get { return "Supplies Screen One"; }
        }
        public override void Display()
        {
            ClearBackground(Colors.space);
            starscape();
            Confirmation();
        }
        private static void Confirmation(){
            //Raylib.DrawText("Your game has been saved.", Raylib.GetScreenWidth()/32*7, (Raylib.GetScreenHeight()/2)-30, 30, WHITE);
            if (PressSPACEBAR()) {
                MainWindow.settings.currentScreen = "welcome";
            }
        }
    }
}
