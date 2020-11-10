using Raylib_cs;
using static Raylib_cs.Raylib;
using static Raylib_cs.Color;
using System;
using System.Numerics;
using System.Windows.Input;

namespace moonshot.Screens
{
    class loadScreen : screen
    {
        public override string Name {
            get { return "Load"; }
        }
        public override void Display()
        {
            ClearBackground(Colors.space);
            starscape();
            Menuline(Raylib.GetScreenWidth() / 128, Raylib.GetScreenHeight() / 6);
            Menuline(Raylib.GetScreenWidth() / 128, Raylib.GetScreenHeight() / 24 * 20 );
            MoonshotLogo();
            Menu();
        }
        internal static string selection = String.Empty;
        internal static void Menu() {
            int keypress = Raylib.GetKeyPressed();
            if (Raylib.IsKeyPressed(KeyboardKey.KEY_BACKSPACE)) {
                keypress = 9000;
            } else if (Raylib.IsKeyPressed(KeyboardKey.KEY_ENTER)) {
                switch (selection) {
                    case "y":
                        MainWindow.settings.currentScreen = MainWindow.currentScreenTempStore;
                        MainWindow.currentScreenTempStore = "";
                        MainWindow.settings.savedProgress = false;
                        selection = "";
                        break;
                    case "n":
                        MainWindow.settings.currentScreen = "Character Selection";
                        MainWindow.currentScreenTempStore = "";
                        MainWindow.settings.savedProgress = false;
                        break;
                    default:
                        break;
                }
                selection = "";
            }
            switch (keypress){
                case 121:
                    selection = "y";
                    break;
                case 110:
                    selection = "n";
                    break;
                case 9000:
                    selection = String.Empty;
                    break;
                default:
                    break;
            }
            Raylib.DrawText("Would you like to continue a\nsaved game? " + selection + "_", Raylib.GetScreenWidth()/8, (Raylib.GetScreenHeight()/2)-30, 30, WHITE);
        }
    }
}
