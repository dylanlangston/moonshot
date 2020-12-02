using Raylib_cs;
using static Raylib_cs.Raylib;
using static Raylib_cs.Color;
using System;
using System.Numerics;
using System.Windows.Input;
using System.IO;
using System.Collections.Generic;

namespace moonshot.Screens
{
    partial class mainTrail : screen
    {
        public override string Name {
            get { return "Main Trail"; }
        }
        private static int annimationCounter = 15;
        private static int foodCounter = 0;
        internal static bool StartAnimation = false;
        public static void MainTrailMainDisplay()
        {
            ClearBackground(Colors.space);
            starscape();
            if (StartAnimation)
                annimationCounter++;
            switch (annimationCounter/15) {
                case (1):
                    MainTrail1();
                    break;
                case (2):
                    MainTrail2();
                    break;
                case (3):
                    MainTrail3();
                    break;
                case (4):
                    MainTrail4();
                    break;
                case (5):
                    MainTrail4();
                    break;
                case (6):
                    MainTrail3();
                    break;
                case (7):
                    MainTrail2();
                    break;
                case (8):
                    MainTrail1();
                    break;
                default:
                    MainTrail1();
                    annimationCounter = 15;
                    break;
            }
            WhiteBackground();
            Date();
            Fuel();
            Health();
            Food();
            NextLandmark();
            LastLandmark();
            DisplayIcon();
        }
        public override void Display()
        {
            MainTrailMainDisplay();

            //Confirmation();

            if (!MainWindow.settings.userStats.ShipWorking){
                ShipBroke();
            }
            else if (StartAnimation == false && MainWindow.settings.userStats.milesTraveled == 0 && String.IsNullOrEmpty(tempPopUpMessage))
            {
                LargePopUp(popUpMessages[MainWindow.settings.userStats.currentLocation].Item1, popUpMessages[MainWindow.settings.userStats.currentLocation].Item2, popUpMessages[MainWindow.settings.userStats.currentLocation].Item3);
            }
            else if (StartAnimation == false && String.IsNullOrEmpty(tempPopUpMessage))
            {
                if (Raylib.IsKeyReleased(KeyboardKey.KEY_ENTER))
                    StartAnimation = true;
            } else
            {
                if (!String.IsNullOrEmpty(tempPopUpMessage))
                {
                    LargePopUp(tempPopUpMessage, tempPromptBool, tempNextScreen);
                }
                else {
                    PressEnterToSizeUp();
                    Tuple<int, int> foodAndFuel = GetFoodAndFuelMod();
                    CheckHealth();
                    CheckOxygen();
                    CalamityChance();
                    CheckForDeadPlayers();
                    Travel(foodAndFuel.Item1);
                    foodCounter++;
                    if (foodCounter > 400) {
                        foodCounter = 0;
                        UseFood(foodAndFuel.Item2);
                    }
                }
            }
                
        }
        private static void Confirmation(){
            if (PressSPACEBAR()) {
                //MainWindow.settings.currentScreen = "Supplies Screen Two";
            }
        }

        private static void WhiteBackground()
        {
            DrawRectangle(0, 300, Raylib.GetScreenWidth(), 260, new Color(255,255,255,200));
        }
        public static List<(string, bool, string)> popUpMessages = new List<(string, bool, string)>() { 
            ("From the Landing Site it is 209\nMiles to the Mare Tranquillitatis.", false, String.Empty),
            ("You are now at Mare\nTranquillitatis. Would you like to\nlook around? ", true, "Mare Tranquillitatis"),
            ("       You are now at MTP.\n          (collapsed pit of\n       Mare Tranquillitatis)\nWould you like to look around? ", true, "MTP"),
            ("You are now at Mare Serenitatis.\nWould you like to look around? ", true, "Mare Serenitatis"),
            ("You are now at Posidonius. Would\nyou like to look around? ", true, "Posidonius"),
            ("You are now at Montes Taurus.\nWould you like to look around? ", true, "Montes Taurus"),
            ("You are now at Atlas & Hercules.\nWould you like to look around? ", true, "Atlas and Hercules"),
            ("You are now at Mare Frigoris.\nWould you like to look around? ", true, "Mare Frigoris"),
            ("You are now at Anaxagoras. Would\nyou like to look around? ", true, "Anaxagoras"),
            ("        You are now at Peary!", false, "Peary")
        };

        public static void ResetPopups()
        {
            popUpMessages = new List<(string, bool, string)>() { 
            ("From the Landing Site it is 209\nMiles to the Mare Tranquillitatis.", false, String.Empty),
            ("You are now at Mare\nTranquillitatis. Would you like to\nlook around? ", true, "Mare Tranquillitatis"),
            ("       You are now at MTP.\n          (collapsed pit of\n       Mare Tranquillitatis)\nWould you like to look around? ", true, "MTP"),
            ("You are now at Mare Serenitatis.\nWould you like to look around? ", true, "Mare Serenitatis"),
            ("You are now at Posidonius. Would\nyou like to look around? ", true, "Posidonius"),
            ("You are now at Montes Taurus.\nWould you like to look around? ", true, "Montes Taurus"),
            ("You are now at Atlas & Hercules.\nWould you like to look around? ", true, "Atlas and Hercules"),
            ("You are now at Mare Frigoris.\nWould you like to look around? ", true, "Mare Frigoris"),
            ("You are now at Anaxagoras. Would\nyou like to look around? ", true, "Anaxagoras"),
            ("        You are now at Peary!", false, "Peary")
        };

        }



        private static string tempPopUpMessage = String.Empty;
        private static bool tempPromptBool = false;
        private static string tempNextScreen = String.Empty;
        private static void DisplayNewPopUp(string newTempPopUpMessage, bool newTempPromptBool = false, string newTempNextScreen = "")
        {
            StartAnimation = false;
            tempPopUpMessage = newTempPopUpMessage;
            tempPromptBool = newTempPromptBool;
            tempNextScreen = newTempNextScreen;
        }

        private static string selectionLargePopUp = String.Empty;
        internal static void LargePopUp(string message = "", bool prompt = false, string nextScreen = "Check Stats")
        {
            string[] messageArray = message.Split("\n");
            Raylib.DrawRectangleRounded(new Rectangle(Raylib.GetScreenWidth()/8, Raylib.GetScreenHeight()/3, Raylib.GetScreenWidth()/8*6, 40+(messageArray.Length*30)), 0.25f, 10, WHITE);
            Raylib.DrawRectangle(Raylib.GetScreenWidth()/8+10, Raylib.GetScreenHeight()/3+10, Raylib.GetScreenWidth()/8*6-20, 20+(messageArray.Length*30), BLACK);
            for (int i = 0;i < messageArray.Length;i++)
            {
                Raylib.DrawText(messageArray[i] + (prompt && messageArray.Length-1 == i ?  selectionLargePopUp + "_" : ""), Raylib.GetScreenWidth()/8+50, Raylib.GetScreenHeight()/3+20+(30*i), 30, WHITE);
            }
            if (prompt) {
            int keypress = Raylib.GetKeyPressed();
            if (Raylib.IsKeyPressed(KeyboardKey.KEY_BACKSPACE)) {
                keypress = 9000;
            } else if (Raylib.IsKeyPressed(KeyboardKey.KEY_ENTER)) {
                switch (selectionLargePopUp) {
                    case "y":
                        MainWindow.settings.currentScreen = nextScreen;
                        break;
                    case "n":
                        MainWindow.settings.userStats.milesTraveled++;
                        break;
                    default:
                        break;
                }
                selectionLargePopUp = "";
            }
            switch (keypress){
                case 'y':
                    selectionLargePopUp = "y";
                    break;
                case 'n':
                    selectionLargePopUp = "n";
                    break;
                case 9000:
                    selectionLargePopUp = String.Empty;
                    break;
                default:
                    break;
            }
            } else {
            if (PressSPACEBAR()) {
                if (nextScreen != String.Empty)
                    MainWindow.settings.currentScreen = nextScreen;
                else 
                    StartAnimation = true;
                tempPopUpMessage = String.Empty;
            }
            }
        }
        private static void PressEnterToSizeUp()
        {
            Raylib.DrawRectangle(0, 300, Raylib.GetScreenWidth(), 35, Color.BLACK);
            Raylib.DrawText("Press ENTER to size up the situation", 110, 303, 30, WHITE);
            if (Raylib.IsKeyReleased(KeyboardKey.KEY_ENTER))
            {
                StartAnimation = false;
                MainWindow.settings.currentScreen = "Check Stats";
            }
        }
        private static void Date()
        {
            Raylib.DrawText("Date :", Raylib.GetScreenWidth()/2-100, Raylib.GetScreenHeight()/2+40, 30, BLACK);
            Raylib.DrawText(MainWindow.settings.userStats.currentTime.ToString("MMMM dd, yyyy"), Raylib.GetScreenWidth()/2, Raylib.GetScreenHeight()/2+40, 30, BLACK);
        }

        private static void Fuel()
        {
            Raylib.DrawText("Fuel :", Raylib.GetScreenWidth()/2-95, Raylib.GetScreenHeight()/2+70, 30, BLACK);
            Raylib.DrawText(MainWindow.settings.userStats.inventory.Items.Find(s => s.id == 102).value.ToString() + " gallons", Raylib.GetScreenWidth()/2, Raylib.GetScreenHeight()/2+70, 30, BLACK);
        }

        private static void Health()
        {
            Raylib.DrawText("Health :", Raylib.GetScreenWidth()/2-128, Raylib.GetScreenHeight()/2+100, 30, BLACK);
            Raylib.DrawText(MainWindow.settings.userStats.status, Raylib.GetScreenWidth()/2, Raylib.GetScreenHeight()/2+100, 30, BLACK);
        }

        private static void Food()
        {
            Raylib.DrawText("Food :", Raylib.GetScreenWidth()/2-104, Raylib.GetScreenHeight()/2+130, 30, BLACK);
            Raylib.DrawText(MainWindow.settings.userStats.inventory.Items.Find(s => s.id == 103).value.ToString() + " pounds", Raylib.GetScreenWidth()/2, Raylib.GetScreenHeight()/2+130, 30, BLACK);
        }

        private static void NextLandmark()
        {
            int currentLocation = MainWindow.settings.userStats.currentLocation;
            int milesTraveled = MainWindow.settings.userStats.milesTraveled;
            int NextLandmarkDistance = ((int)(LocationsAndDistances[currentLocation].Item3)) - milesTraveled;
            Raylib.DrawText("Next Landmark :", Raylib.GetScreenWidth()/2-257, Raylib.GetScreenHeight()/2+160, 30, BLACK);
            Raylib.DrawText(NextLandmarkDistance.ToString() + " miles", Raylib.GetScreenWidth()/2, Raylib.GetScreenHeight()/2+160, 30, BLACK);
        }

        private static void LastLandmark()
        {
            int currentLocation = MainWindow.settings.userStats.currentLocation;
            int milesTraveled = MainWindow.settings.userStats.milesTraveled;
            int totalMilesTraveled = GetMilesTraveled(currentLocation) + milesTraveled;
            Raylib.DrawText("Miles Traveled :", Raylib.GetScreenWidth()/2-258, Raylib.GetScreenHeight()/2+190, 30, BLACK);
            Raylib.DrawText(totalMilesTraveled.ToString() + " miles", Raylib.GetScreenWidth()/2, Raylib.GetScreenHeight()/2+190, 30, BLACK);
        }

        private static Texture2D mainTrail1Texture = new Texture2D();
        private static void MainTrail1()
        {
            if (mainTrail1Texture.height == 0) {
                Image img = LoadImage(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Images/mainTrail1.png"));
                mainTrail1Texture = LoadTextureFromImage(img);
                UnloadImage(img);
            }
            DrawTextureEx(mainTrail1Texture, new Vector2(0, 0), 0f, 1f, WHITE);
        }
        private static Texture2D mainTrail2Texture = new Texture2D();
        private static void MainTrail2()
        {
            if (mainTrail2Texture.height == 0) {
                Image img = LoadImage(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Images/mainTrail2.png"));
                mainTrail2Texture = LoadTextureFromImage(img);
                UnloadImage(img);
            }
            DrawTextureEx(mainTrail2Texture, new Vector2(0, 0), 0f, 1f, WHITE);
        }
        private static Texture2D mainTrail3Texture = new Texture2D();
        private static void MainTrail3()
        {
            if (mainTrail3Texture.height == 0) {
                Image img = LoadImage(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Images/mainTrail3.png"));
                mainTrail3Texture = LoadTextureFromImage(img);
                UnloadImage(img);
            }
            DrawTextureEx(mainTrail3Texture, new Vector2(0, 0), 0f, 1f, WHITE);
        }
        private static Texture2D mainTrail4Texture = new Texture2D();
        private static void MainTrail4()
        {
            if (mainTrail4Texture.height == 0) {
                Image img = LoadImage(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Images/mainTrail4.png"));
                mainTrail4Texture = LoadTextureFromImage(img);
                UnloadImage(img);
            }
            DrawTextureEx(mainTrail4Texture, new Vector2(0, 0), 0f, 1f, WHITE);
        }
    }
}
