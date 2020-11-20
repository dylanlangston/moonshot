using Raylib_cs;
using static Raylib_cs.Raylib;
using static Raylib_cs.Color;
using System;
using System.Numerics;
using System.Windows.Input;

namespace moonshot.Screens
{
    class mainTrail : screen
    {
        public override string Name {
            get { return "Main Trail"; }
        }
        public override void Display()
        {
            ClearBackground(Colors.space);
            starscape();
            Confirmation();
        }
        private static void Confirmation(){
            if (PressSPACEBAR()) {
                //MainWindow.settings.currentScreen = "Supplies Screen Two";
            }
        }
    }
}
