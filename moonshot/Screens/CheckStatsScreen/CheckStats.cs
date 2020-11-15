using Raylib_cs;
using static Raylib_cs.Raylib;
using static Raylib_cs.Color;
using System;
using System.Numerics;
using System.Collections.Generic;
using System.IO;

namespace moonshot.Screens
{
    class CheckStats : screen
    {
        public override string Name {
            get { return "Check Stats"; }
        }
        
        public override void Display()
        {
            ClearBackground(Colors.space);
            starscape();
            UserStats stats = MainWindow.settings.userStats;
            LocationAndTime(MainWindow.settings.Locations[stats.currentLocation].Item1, stats.currentTime);
            ShowBasicStats(stats.status, stats.pace, stats.rations);
            YouMay();
            WhatIsYourChoice();
        }
        private void LocationAndTime(string Location, DateTime time)
        {
            Raylib.DrawText(Location, (Raylib.GetScreenWidth()-(Location.Length * 16))/2, 5, 30, WHITE);
            Raylib.DrawText(time.ToString("MMMM dd, yyyy"), (Raylib.GetScreenWidth()-(time.ToString("MMMM dd, yyyy").Length * 16))/2, 40, 30, WHITE);
        }
        private void ShowBasicStats(string health, string pace, string ration)
        {
            Raylib.DrawRectangle(0, 75, Raylib.GetScreenWidth(), 90, WHITE);
            Raylib.DrawText("Health:    " + health, 30, 80, 30, BLACK);
            Raylib.DrawText("Pace:    " + pace, 30, 105, 30, BLACK);
            Raylib.DrawText("Rations:    " + ration, 30, 130, 30, BLACK);
        }
        private void YouMay(bool BuySupplies = true)
        {
            Raylib.DrawText("You may:", 10, 170, 30, WHITE);
            Raylib.DrawText("1.  Continue", 100, 225, 30, WHITE);
            Raylib.DrawText("2. Check supplies", 100, 260, 30, WHITE);
            Raylib.DrawText("3. Look at map", 100, 295, 30, WHITE);
            Raylib.DrawText("4. Change pace", 100, 330, 30, WHITE);
            Raylib.DrawText("5. Change food ration", 100, 365, 30, WHITE);
            Raylib.DrawText("6. Stop to rest", 100, 400, 30, WHITE);
            Raylib.DrawText("7. Talk with mission control", 100, 435, 30, WHITE);
            if (BuySupplies)
                Raylib.DrawText("8. Buy supplies", 100, 470, 30, WHITE);
        }
        private static string selection = String.Empty;
        private void WhatIsYourChoice(bool BuySupplies = true)
        {
            int keypress = Raylib.GetKeyPressed();
            if (Raylib.IsKeyPressed(KeyboardKey.KEY_BACKSPACE)) {
                keypress = 9000;
            } else if (Raylib.IsKeyPressed(KeyboardKey.KEY_ENTER)) {
                switch (selection) {
                    case "3":
                        MainWindow.settings.currentScreen = "Look at Map";
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
                case '5':
                    selection = "5";
                    break;
                case '6':
                    selection = "6";
                    break;
                case '7':
                    selection = "7";
                    break;
                case '8':
                    if (BuySupplies)
                        selection = "8";
                    break;
                case 9000:
                    selection = String.Empty;
                    break;
                default:
                    break;
            }
            Raylib.DrawText("What is your choice? " + selection + "_", 10, Raylib.GetScreenHeight()-40, 30, WHITE);
        }
    }
}
