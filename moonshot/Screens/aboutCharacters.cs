using Raylib_cs;
using static Raylib_cs.Raylib;
using static Raylib_cs.Color;
using System;
using System.Numerics;
using System.Windows.Input;

namespace moonshot.Screens
{
    class aboutCharacters : screen
    {
        public override string Name {
            get { return "About Characters"; }
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
            Raylib.DrawText("Traveling to the Moon isn't easy! But if\nyou're in the Apollo 11 mission, you'll have\nmore money and less rocks to collect than\nthe Apollo 12 or 14 missions.", Raylib.GetScreenWidth()/10, (Raylib.GetScreenHeight()/5), 30, WHITE);
            Raylib.DrawText("However, the harder you have to try, the\nmore points you deserve! Therefore, the\nApollo 14 mission earns the most points and\nthe Apollo 11 mission has the least.", Raylib.GetScreenWidth()/10, (Raylib.GetScreenHeight()/2)+20, 30, WHITE);
            if (PressSPACEBAR()) {
                MainWindow.settings.currentScreen = "Character Selection";
            }
        }
    }
}
