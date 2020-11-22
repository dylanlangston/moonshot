using Raylib_cs;
using static Raylib_cs.Raylib;
using static Raylib_cs.Color;
using System;
using System.Numerics;
using System.Collections.Generic;
using System.IO;

namespace moonshot.Screens
{
    class ChangePace : screen
    {
        public override string Name {
            get { return "Change Pace"; }
        }
        
        public override void Display()
        {
            ClearBackground(Colors.space);
            starscape();
            Menuline(Raylib.GetScreenWidth() / 128, Raylib.GetScreenHeight()/12);
            Menuline(Raylib.GetScreenWidth() / 128, Raylib.GetScreenHeight() / 24 * 20 );
            CurrentPace(MainWindow.settings.userStats.pace);
            ChoosePace();
        }
        private void CurrentPace(string pace)
        {
            Raylib.DrawText("Change pace", (Raylib.GetScreenWidth()-(190))/2, Raylib.GetScreenHeight()/5, 30, WHITE);
            string paceFull = "(currently \"" + pace + "\")";
            Raylib.DrawText(paceFull, (Raylib.GetScreenWidth()-(paceFull.Length * 16))/2, Raylib.GetScreenHeight()/5+35, 30, WHITE);
            Raylib.DrawText("The pace at which you travel can\nchange. Your choices are:", Raylib.GetScreenWidth()/8, Raylib.GetScreenHeight()/3, 30, WHITE);
            Raylib.DrawText("1.  A Steady pace", Raylib.GetScreenWidth()/6, Raylib.GetScreenHeight()/2, 30, WHITE);
            Raylib.DrawText("2. A Strenuous pace", Raylib.GetScreenWidth()/6, Raylib.GetScreenHeight()/2 + 30, 30, WHITE);
            Raylib.DrawText("3. A Grueling pace", Raylib.GetScreenWidth()/6, Raylib.GetScreenHeight()/2 + 60, 30, WHITE);
            Raylib.DrawText("4. Find out what these different", Raylib.GetScreenWidth()/6, Raylib.GetScreenHeight()/2 + 90, 30, WHITE);
            Raylib.DrawText("paces mean", Raylib.GetScreenWidth()/6+ 35, Raylib.GetScreenHeight()/2 + 115, 30, WHITE);
        }
        private static string selection = String.Empty;
        private void ChoosePace()
        {
            Raylib.DrawText("What is your choice? " + selection + "_", Raylib.GetScreenWidth()/8, Raylib.GetScreenHeight()/2 + 170, 30, WHITE);
            int keypress = Raylib.GetKeyPressed();
            if (Raylib.IsKeyPressed(KeyboardKey.KEY_BACKSPACE)) {
                keypress = 9000;
            } else if (Raylib.IsKeyPressed(KeyboardKey.KEY_ENTER)) {
                switch (selection) {
                    case "1":
                        MainWindow.settings.userStats.pace = PlayerPace.steady;
                        MainWindow.settings.currentScreen = "Check Stats";
                        break;
                    case "2":
                        MainWindow.settings.userStats.pace = PlayerPace.strenuous;
                        MainWindow.settings.currentScreen = "Check Stats";
                        break;
                    case "3":
                        MainWindow.settings.userStats.pace = PlayerPace.grueling;
                        MainWindow.settings.currentScreen = "Check Stats";
                        break;
                    case "4":
                        MainWindow.settings.currentScreen = "Change Pace Info";
                        break;
                    default:
                        break;
                }
                selection = "";
            }
            switch (keypress){
                case '1':
                    selection = "1";
                    break;
                case '2':
                    selection = "2";
                    break;
                case '3':
                    selection = "3";
                    break;
                case '4':
                    selection = "4";
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
