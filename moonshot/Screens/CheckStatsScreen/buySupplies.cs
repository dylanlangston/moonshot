using Raylib_cs;
using static Raylib_cs.Raylib;
using static Raylib_cs.Color;
using System;
using System.Numerics;
using System.Collections.Generic;
using System.IO;

namespace moonshot.Screens
{
    class BuySupplies : screen
    {
        public override string Name {
            get { return "Buy Supplies"; }
        }
        private static string selection = String.Empty;
        private static bool selectionMade = false;
        private static bool cantAffort = false;
        public override void Display()
        {
            ClearBackground(Colors.space);
            starscape();
            UserStats stats = MainWindow.settings.userStats;
            LocationAndTime(MainWindow.settings.Locations[stats.currentLocation].Item1, stats.currentTime);
            YouMayBuy();
            if (selectionMade)
            {
                if (!cantAffort)
                    HowMany();
                else
                    CantAffort();
            }
        }
        private void LocationAndTime(string Location, DateTime time)
        {
            Raylib.DrawRectangle(Raylib.GetScreenWidth()/8, 0, Raylib.GetScreenWidth()/8*6, 75, WHITE);
            Raylib.DrawText(Location, (Raylib.GetScreenWidth()-(Location.Length * 16))/2, 5, 30, BLACK);
            Raylib.DrawText(time.ToString("MMMM dd, yyyy"), (Raylib.GetScreenWidth()-(time.ToString("MMMM dd, yyyy").Length * 16))/2, 40, 30, BLACK);
        }
        private void YouMayBuy()
        {
            Raylib.DrawText("You may buy:", 10, 120, 30, WHITE);
            Raylib.DrawText("1.  Oxygen Tank", 100, 175, 30, WHITE);
            Raylib.DrawText("2. Fuel", 100, 210, 30, WHITE);
            Raylib.DrawText("3. Food", 100, 245, 30, WHITE);
            Raylib.DrawText("4. Boxes", 100, 280, 30, WHITE);
            Raylib.DrawText("5. Ship Parts", 100, 315, 30, WHITE);
            Raylib.DrawText("6. Leave store", 100, 350, 30, WHITE);

            Raylib.DrawText("$15.00 per tank", 506, 175, 30, WHITE);
            Raylib.DrawText("  $1.00 per gallon", 500, 210, 30, WHITE);
            Raylib.DrawText("  $1.00 per pound", 500, 245, 30, WHITE);
            Raylib.DrawText(" $5.00 per box", 505, 280, 30, WHITE);
            Raylib.DrawText("$20.00 per kit", 500, 315, 30, WHITE);

            Raylib.DrawText("You have $" + MainWindow.settings.userStats.Money + ".00 to spend.", 10, 400, 30, WHITE);

            Raylib.DrawText("Which number? " + selection + (selectionMade ? String.Empty : "_"), 10, 430, 30, WHITE);
            if (!selectionMade) {
            int keypress = Raylib.GetKeyPressed();
            if (Raylib.IsKeyPressed(KeyboardKey.KEY_BACKSPACE)) {
                keypress = 9000;
            } else if (Raylib.IsKeyPressed(KeyboardKey.KEY_ENTER)) {
                switch (selection) {
                    case "":
                        break;
                    case "6":
                        MainWindow.settings.currentScreen = "Check Stats";
                        selection = "";
                        break;
                    default:
                        selectionMade = true;
                        break;
                }
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
                case 9000:
                    selection = String.Empty;
                    break;
                default:
                    break;
            }
            }
        }

        private static string _howManySelection = String.Empty;
        private static string howManySelection { 
            get { return _howManySelection; } 
            set { 
                int tempInt = 0; 
                switch (selection){
                    case "1":
                        Int32.TryParse(value, out tempInt);
                        if (tempInt < 17) 
                            if (value.Length < 3)
                                _howManySelection = value;
                        break;
                    case "2":
                        Int32.TryParse(value, out tempInt);
                        if (tempInt < 181) 
                            if (value.Length < 4)
                                _howManySelection = value;
                                break;
                    case "3":
                        Int32.TryParse(value, out tempInt);
                        if (tempInt < 401) 
                            if (value.Length < 4)
                                _howManySelection = value;
                        break;
                    case "4":
                        Int32.TryParse(value, out tempInt);
                        if (tempInt < 200) 
                            if (value.Length < 4)
                                _howManySelection = value;
                        break;
                    case "5":
                        Int32.TryParse(value, out tempInt);
                        if (tempInt < 6) 
                            if (value.Length < 2)
                                _howManySelection = value;
                        break;
                    default:
                        break;
                }
            }
        }

        private static void HowMany()
        {
            string message = String.Empty;
            switch (selection){
                case "1":
                    message = "How many oxygen tanks?";
                    break;
                case "2":
                    message = "How many gallons of fuel?";
                    break;
                case "3":
                    message = "How many pounds of food?";
                    break;
                case "4":
                    message = "How many boxes?";
                    break;
                case "5":
                    message = "How many ship part kits?";
                    break;
                default:
                    break;
            }

            int keypress = Raylib.GetKeyPressed();
            if (Raylib.IsKeyPressed(KeyboardKey.KEY_BACKSPACE)) {
                keypress = 9000;
            } else if (Raylib.IsKeyPressed(KeyboardKey.KEY_ENTER)) {
                if (!String.IsNullOrEmpty(howManySelection))
                {
                    int tempInt = 0; 
                    int price = 0;
                    switch (selection){
                        case "1":
                            Int32.TryParse(howManySelection, out tempInt);
                            price = (tempInt * 15);
                            if (price <= MainWindow.settings.userStats.Money)
                            {
                                MainWindow.settings.userStats.inventory.AddItem(101, tempInt);
                                MainWindow.settings.userStats.Money -= price;
                                howManySelection = String.Empty;
                                selection = String.Empty;
                                selectionMade = false;
                            } 
                            else 
                            {
                                cantAffort = true;
                            }
                            break;
                        case "2":
                            Int32.TryParse(howManySelection, out tempInt);
                            price = (tempInt);
                            if (price <= MainWindow.settings.userStats.Money)
                            {
                                MainWindow.settings.userStats.inventory.AddItem(102, tempInt);
                                MainWindow.settings.userStats.Money -= price;
                                howManySelection = String.Empty;
                                selection = String.Empty;
                                selectionMade = false;
                            }
                            else 
                            {
                                cantAffort = true;
                            }
                            break;
                        case "3":
                            Int32.TryParse(howManySelection, out tempInt);
                            price = (tempInt);
                            if (price <= MainWindow.settings.userStats.Money)
                            {
                                MainWindow.settings.userStats.inventory.AddItem(103, tempInt);
                                MainWindow.settings.userStats.Money -= price;
                                howManySelection = String.Empty;
                                selection = String.Empty;
                                selectionMade = false;
                            }
                            else 
                            {
                                cantAffort = true;
                            }
                            break;
                        case "4":
                            Int32.TryParse(howManySelection, out tempInt);
                            price = (tempInt * 5);
                            if (price <= MainWindow.settings.userStats.Money)
                            {
                                MainWindow.settings.userStats.inventory.AddItem(104, tempInt);
                                MainWindow.settings.userStats.Money -= price;
                                howManySelection = String.Empty;
                                selection = String.Empty;
                                selectionMade = false;
                            }
                            else 
                            {
                                cantAffort = true;
                            }
                            break;
                        case "5":
                            Int32.TryParse(howManySelection, out tempInt);
                            price = (tempInt * 20);
                            if (price <= MainWindow.settings.userStats.Money)
                            {
                                MainWindow.settings.userStats.inventory.AddItem(105, tempInt);
                                MainWindow.settings.userStats.Money -= price;
                                howManySelection = String.Empty;
                                selection = String.Empty;
                                selectionMade = false;
                            }
                            else 
                            {
                                cantAffort = true;
                            }
                            break;
                        default:
                            break;
                    }
                }
            }
            switch (keypress){
                case '1':
                    howManySelection += "1";
                    break;
                case '2':
                    howManySelection += "2";
                    break;
                case '3':
                    howManySelection += "3";
                    break;
                case '4':
                    howManySelection += "4";
                    break;
                case '5':
                    howManySelection += "5";
                    break;
                case '6':
                    howManySelection += "6";
                    break;
                case '7':
                    howManySelection += "7";
                    break;
                case '8':
                    howManySelection += "8";
                    break;
                case '9':
                    howManySelection += "9";
                    break;
                case '0':
                    howManySelection += "0";
                    break;
                case 9000:
                    try {howManySelection = howManySelection.Remove(howManySelection.Length-1, 1);} catch {}
                    break;
                default:
                    break;
            }

            Raylib.DrawText(message + " " + howManySelection + "_", 10, 460, 30, WHITE);
        }

        private static void CantAffort()
        {
            Raylib.DrawText("You cannot afford that many.", 10, 480, 30, WHITE);
            if (PressSPACEBAR()) {
                howManySelection = String.Empty;
                selection = String.Empty;
                cantAffort = false;
                selectionMade = false;
            }
        }
    }
}
