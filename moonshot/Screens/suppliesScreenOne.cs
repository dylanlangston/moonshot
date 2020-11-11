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
            Raylib.DrawText("Before leaving Cape Kennedy you should\nget equipment and supplies. You have " + MainWindow.settings.userStats.Money + "\nin credits, but you don't have to use it\nall now.", Raylib.GetScreenWidth()/10, (Raylib.GetScreenHeight()/3), 30, WHITE);
            if (PressSPACEBAR()) {
                MainWindow.settings.currentScreen = "Supplies Screen Two";
            }
        }
    }
}
