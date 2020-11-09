using Raylib_cs;
using static Raylib_cs.Raylib;
using static Raylib_cs.Color;
using System;
using System.Numerics;
using System.Windows.Input;
using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Diagnostics;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using static System.Environment;

namespace moonshot.Screens
{
    class settingsScreen : screen
    {
        static System.Reflection.Assembly assembly = System.Reflection.Assembly.GetExecutingAssembly();
        static System.Diagnostics.FileVersionInfo fvi = System.Diagnostics.FileVersionInfo.GetVersionInfo(assembly.Location);
        static string version = fvi.FileVersion;
        public override string Name {
            get { return "Settings"; }
        }
        public override void Display()
        {
            ClearBackground(Colors.space);
            starscape();
            Menu();
        }
        internal static string selection = String.Empty;
        internal static void Menu() {
            Raylib.DrawText("M O O N S H O T", Raylib.GetScreenWidth()/48*17, 30, 30, WHITE);
            Raylib.DrawText("Version "+version, Raylib.GetScreenWidth()/96*37, 60, 30, WHITE);
            Raylib.DrawLineV(new Vector2(0, 92), new Vector2(Raylib.GetScreenWidth(), 92), BLUE);
            Raylib.DrawLineV(new Vector2(0, 93), new Vector2(Raylib.GetScreenWidth(), 93), BLUE);
            Raylib.DrawLineV(new Vector2(0, 94), new Vector2(Raylib.GetScreenWidth(), 94), BLUE);
            Raylib.DrawText("Management Options", Raylib.GetScreenWidth()/96*31, 95, 30, WHITE);

            Raylib.DrawText("You may:", Raylib.GetScreenWidth()/8, Raylib.GetScreenHeight()/4, 30, WHITE);
            Raylib.DrawText("1.  See the current Top Ten list", Raylib.GetScreenWidth()/7, Raylib.GetScreenHeight()/3, 30, WHITE);
            Raylib.DrawText("2. See the original Top Ten list", Raylib.GetScreenWidth()/7, Raylib.GetScreenHeight()/3 + 35, 30, WHITE);
            Raylib.DrawText("3. Erase the current Top Ten list", Raylib.GetScreenWidth()/7, Raylib.GetScreenHeight()/3 + 70, 30, WHITE);
            Raylib.DrawText("4. Erase the tombstone messages", Raylib.GetScreenWidth()/7, Raylib.GetScreenHeight()/3 + 105, 30, WHITE);
            Raylib.DrawText("5. Erase the saved game", Raylib.GetScreenWidth()/7, Raylib.GetScreenHeight()/3 + 140, 30, WHITE);
            Raylib.DrawText("6. Full Screen " + (Raylib.IsWindowFullscreen() ? "Off" : "On") + "   (press escape)", Raylib.GetScreenWidth()/7, Raylib.GetScreenHeight()/3 + 175, 30, WHITE);
            Raylib.DrawText("7. Return to the main menu", Raylib.GetScreenWidth()/7, Raylib.GetScreenHeight()/3 + 210, 30, WHITE);


            int keypress = Raylib.GetKeyPressed();
            if (Raylib.IsKeyPressed(KeyboardKey.KEY_BACKSPACE)) {
                keypress = 9000;
            } else if (Raylib.IsKeyPressed(KeyboardKey.KEY_ENTER)) {
                switch (selection) {
                    case "6":
                        Raylib.ToggleFullscreen();
                        // Show/Hide cursor depending on if full screen or not.
                        if (Raylib.IsCursorHidden())
                        {
                            Raylib.ShowCursor();
                        }
                        else
                        {
                            Raylib.HideCursor();
                        }
                        // Update setting
                        MainWindow.settings.StartFullscreen = Raylib.IsWindowFullscreen();
                        break;
                    case "7":
                        MainWindow.settings.currentScreen = "welcome";
                        break;
                    default:
                        break;
                }
                selection = "";
            }
            switch (keypress){
                case 49:
                    selection = "1";
                    break;
                case 50:
                    selection = "2";
                    break;
                case 51:
                    selection = "3";
                    break;
                case 52:
                    selection = "4";
                    break;
                case 53:
                    selection = "5";
                    break;
                case 54:
                    selection = "6";
                    break;
                case 55:
                    selection = "7";
                    break;
                case 9000:
                    selection = String.Empty;
                    break;
                default:
                    break;
            }
            Raylib.DrawText("What is your choice? " + selection + "_", Raylib.GetScreenWidth()/8, Raylib.GetScreenHeight()/5*4, 30, WHITE);
        }
    }
}
