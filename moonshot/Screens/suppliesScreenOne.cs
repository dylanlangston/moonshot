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
            Menuline(Raylib.GetScreenWidth() / 128, Raylib.GetScreenHeight()/12);
            Menuline(Raylib.GetScreenWidth() / 128, Raylib.GetScreenHeight() / 24 * 20 );
            Confirmation();
        }
        private static void Confirmation(){
            Raylib.DrawText("Before leaving Indepedence...", Raylib.GetScreenWidth()/32*7, (Raylib.GetScreenHeight()/2)-30, 30, WHITE);
            if (PressSPACEBAR()) {
                //MainWindow.settings.currentScreen = "welcome";
            }
        }
    }
}
