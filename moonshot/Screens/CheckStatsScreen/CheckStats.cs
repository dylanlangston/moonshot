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
        private static bool resting = false;
        public override void Display()
        {
            ClearBackground(Colors.space);
            starscape();
            UserStats stats = MainWindow.settings.userStats;
            LocationAndTime(MainWindow.settings.Locations[stats.currentLocation+1].Item1, stats.currentTime);
            ShowBasicStats(stats.status, stats.pace, stats.rations);
            bool AtLandmark = MainWindow.settings.userStats.milesTraveled == 0;
            YouMay(AtLandmark);
            WhatIsYourChoice(resting, AtLandmark);
            if (resting)
                Rest();
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
            else 
                Raylib.DrawText("8. Collect Rocks", 100, 470, 30, WHITE);
        }
        private static string selection = String.Empty;
        private void WhatIsYourChoice(bool resting, bool BuySupplies = true)
        {
            if (!resting)
            {
            int keypress = Raylib.GetKeyPressed();
            if (Raylib.IsKeyPressed(KeyboardKey.KEY_BACKSPACE)) {
                keypress = 9000;
            } else if (Raylib.IsKeyPressed(KeyboardKey.KEY_ENTER)) {
                switch (selection) {
                    case "1":
                        MainWindow.settings.currentScreen = "Main Trail";
                        break;
                    case "2":
                        MainWindow.settings.currentScreen = "Check Supplies";
                        break;
                    case "3":
                        MainWindow.settings.currentScreen = "Look at Map";
                        break;
                    case "4":
                        MainWindow.settings.currentScreen = "Change Pace";
                        break;
                    case "5":
                        MainWindow.settings.currentScreen = "Change Food Ration";
                        break;
                    case "6":
                        CheckStats.resting = true;
                        break;
                    case "8":
                        if (BuySupplies)
                            MainWindow.settings.currentScreen = "Buy Supplies";
                        else 
                            MainWindow.settings.currentScreen = "Collect Rocks Game";
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
                    selection = "8";
                    break;
                case 9000:
                    selection = String.Empty;
                    break;
                default:
                    break;
            }
            }
            else 
            { selection = "6"; }
            Raylib.DrawText("What is your choice? " + selection + "_", 10, Raylib.GetScreenHeight()-40, 30, WHITE);
        }

        private static string _timeToRest = String.Empty;
        private static string timeToRest { 
            get { return _timeToRest; } 
            set { 
                int tempInt = 0; 
                Int32.TryParse(value, out tempInt);
                if (tempInt < 13) 
                    if (value.Length < 3)
                        _timeToRest = value;
                } 
            }
        private static void Rest()
        {
            Raylib.DrawRectangleRounded(new Rectangle(Raylib.GetScreenWidth()/8, Raylib.GetScreenHeight()/3, Raylib.GetScreenWidth()/8*6, 100), 0.25f, 10, WHITE);
            Raylib.DrawRectangle(Raylib.GetScreenWidth()/8+10, Raylib.GetScreenHeight()/3+10, Raylib.GetScreenWidth()/8*6-20, 80, BLACK);
            Raylib.DrawText("How many days would you like to", Raylib.GetScreenWidth()/8+50, Raylib.GetScreenHeight()/3+15, 30, WHITE);
            Raylib.DrawText("rest? " + timeToRest + "_", Raylib.GetScreenWidth()/8+50, Raylib.GetScreenHeight()/3+45, 30, WHITE);
            int keypress = Raylib.GetKeyPressed();
            if (Raylib.IsKeyPressed(KeyboardKey.KEY_BACKSPACE)) {
                keypress = 9000;
            } else if (Raylib.IsKeyPressed(KeyboardKey.KEY_ENTER)) {
                if (!String.IsNullOrEmpty(timeToRest))
                {
                    int tempInt = 0; 
                    Int32.TryParse(timeToRest, out tempInt);
                    MainWindow.settings.userStats.currentTime = MainWindow.settings.userStats.currentTime.AddDays((double)tempInt);
                    for (int i = 0; i < tempInt; i++)
                    {
                        Tuple<int, int> foodAndFuel = mainTrail.GetFoodAndFuelMod();
                        mainTrail.UseFood(foodAndFuel.Item2);
                        mainTrail.CheckHealth();
                    }
                    resting = false;
                    timeToRest = String.Empty;
                    selection = String.Empty;
                }
            }
            switch (keypress){
                case '1':
                    timeToRest += "1";
                    break;
                case '2':
                    timeToRest += "2";
                    break;
                case '3':
                    timeToRest += "3";
                    break;
                case '4':
                    timeToRest += "4";
                    break;
                case '5':
                    timeToRest += "5";
                    break;
                case '6':
                    timeToRest += "6";
                    break;
                case '7':
                    timeToRest += "7";
                    break;
                case '8':
                    timeToRest += "8";
                    break;
                case '9':
                    timeToRest += "9";
                    break;
                case '0':
                    timeToRest += "0";
                    break;
                case 9000:
                    try {timeToRest = timeToRest.Remove(timeToRest.Length-1, 1);} catch {}
                    break;
                default:
                    break;
            }
        }
    }
}
