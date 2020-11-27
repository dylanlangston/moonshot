using Raylib_cs;
using static Raylib_cs.Raylib;
using static Raylib_cs.Color;
using System;
using System.Numerics;

namespace moonshot.Screens
{
    class eraseTopTenSettings : screen
    {
        public override string Name {
            get { return "Erase Top Ten Settings"; }
        }
        public override void Display()
        {
            ClearBackground(Colors.space);
            starscape();
            DisplayTopTen();
        }
        private static string selection = String.Empty;
        private static void DisplayTopTen() {

            Raylib.DrawText("Erase Top Ten List", (Raylib.GetScreenWidth()-("Erase Top Ten List".Length*17))/2, 45, 30, WHITE);

            Raylib.DrawLineV(new Vector2(0, 92), new Vector2(Raylib.GetScreenWidth(), 92), BLUE);
            Raylib.DrawLineV(new Vector2(0, 93), new Vector2(Raylib.GetScreenWidth(), 93), BLUE);
            Raylib.DrawLineV(new Vector2(0, 94), new Vector2(Raylib.GetScreenWidth(), 94), BLUE);

            Raylib.DrawText("If you erase the current Top Ten list, the\nnames and scores will be replaced by those on\nthe original list.", 30, 200, 30, WHITE);
            
            Raylib.DrawText("Do you want to do this? " + selection + "_", 30, 350, 30, WHITE);

            int keypress = Raylib.GetKeyPressed();
            if (Raylib.IsKeyPressed(KeyboardKey.KEY_BACKSPACE)) {
                keypress = 9000;
            } else if (Raylib.IsKeyPressed(KeyboardKey.KEY_ENTER)) {
                switch (selection) {
                    case "y":
                        MainWindow.settings.topTen = new TopTen();
                        MainWindow.settings.SaveSettings();
                        MainWindow.settings.currentScreen = "settings";
                        break;
                    case "n":
                        MainWindow.settings.currentScreen = "settings";
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
        }
    }
}
