using Raylib_cs;
using static Raylib_cs.Raylib;
using static Raylib_cs.Color;
using System;
using System.Numerics;
using System.Windows.Input;

namespace moonshot.Screens
{
    class saveScreen : screen
    {
        public override string Name {
            get { return "Save"; }
        }
        public override void Display()
        {
            ClearBackground(Colors.space);
            starscape();
            Menuline(Raylib.GetScreenWidth() / 128, Raylib.GetScreenHeight()/12);
            Menuline(Raylib.GetScreenWidth() / 128, Raylib.GetScreenHeight() / 24 * 20 );
            if (save) { SavedConfirmation(); return; }
            Menu();
        }
        private static bool save = false;
        internal static string selection = String.Empty;
        internal static void Menu() {
            Raylib.DrawText("Leaving MOONSHOT", Raylib.GetScreenWidth()/96*31, 10, 30, WHITE);
            int keypress = Raylib.GetKeyPressed();
            if (Raylib.IsKeyPressed(KeyboardKey.KEY_BACKSPACE)) {
                keypress = 9000;
            } else if (Raylib.IsKeyPressed(KeyboardKey.KEY_ENTER)) {
                switch (selection) {
                    case "y":
                        MainWindow.settings.currentScreen = MainWindow.currentScreenTempStore;
                        MainWindow.settings.SaveSettings();
                        MainWindow.settings.currentScreen = "save";
                        save = true;
                        break;
                    case "n":
                        MainWindow.settings.currentScreen = MainWindow.currentScreenTempStore;
                        MainWindow.currentScreenTempStore = "";
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
            Raylib.DrawText("Do you want to save your game? " + selection + "_", Raylib.GetScreenWidth()/8, (Raylib.GetScreenHeight()/2)-30, 30, WHITE);
        }

        private static void SavedConfirmation(){
            Raylib.DrawText("Your game has been saved.", Raylib.GetScreenWidth()/32*7, (Raylib.GetScreenHeight()/2)-30, 30, WHITE);
            if (PressSPACEBAR()) {
                MainWindow.settings.currentScreen = "welcome";
                MainWindow.settings.savedProgress = true;
                save = false;
            }
        }
    }
}
