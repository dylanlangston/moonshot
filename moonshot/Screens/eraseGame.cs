using Raylib_cs;
using static Raylib_cs.Raylib;
using static Raylib_cs.Color;
using System;
using System.Numerics;
using System.Windows.Input;

namespace moonshot.Screens
{
    class eraseGame : screen
    {
        public override string Name {
            get { return "Erase Game"; }
        }
        public override void Display()
        {
            ClearBackground(Colors.space);
            starscape();
            Confirmation();
        }
        private static string selection = String.Empty;
        private static void Confirmation(){
            Raylib.DrawText("Erase saved games", (Raylib.GetScreenWidth()-("Erase saved games".Length * 15))/2, 30, 30, WHITE);
            Raylib.DrawLineV(new Vector2(0, 92), new Vector2(Raylib.GetScreenWidth(), 92), BLUE);
            Raylib.DrawLineV(new Vector2(0, 93), new Vector2(Raylib.GetScreenWidth(), 93), BLUE);
            Raylib.DrawLineV(new Vector2(0, 94), new Vector2(Raylib.GetScreenWidth(), 94), BLUE);

            int keypress = Raylib.GetKeyPressed();
            if (Raylib.IsKeyPressed(KeyboardKey.KEY_BACKSPACE)) {
                keypress = 9000;
            } else if (Raylib.IsKeyPressed(KeyboardKey.KEY_ENTER)) {
                switch (selection) {
                    case "y":
                        MainWindow.currentScreenTempStore = "";
                        MainWindow.settings.savedProgress = false;
                        MainWindow.settings.userStats = new UserStats();
                        MainWindow.settings.SaveSettings();
                        MainWindow.settings.currentScreen = "Settings";
                        break;
                    case "n":
                        MainWindow.settings.currentScreen = "Settings";
                        break;
                    default:
                        break;
                }
                selection = "";
            }
            switch (keypress){
                case 'y':
                    selection = "y";
                    break;
                case 'n':
                    selection = "n";
                    break;
                case 9000:
                    selection = String.Empty;
                    break;
                default:
                    break;
            }

            Raylib.DrawText("Are you sure you want to erase the\nsaved game? " + selection + "_", 80, Raylib.GetScreenHeight()/2, 30, WHITE);
        }
    }
}
