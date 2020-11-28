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
    partial class collectRocksComplete : collectRocksMiniGameScreen
    {
        public override string Name {
            get { return "Complete"; }
        }
        private static int loopCount = 0;
        public override void Display()
        {
            ClearBackground(Colors.space);
            screen.starscape();

            Message();

            Boxes();

            if (loopCount > 5) {
                Confirmation();
            } else {
                loopCount++;
            }
        }
        private static void Message()
        {
            double usedSpace = MainWindow.settings.userStats.inventory.Items.Find(S => S.id == 104).capacity;
            double freeSpace = MainWindow.settings.userStats.inventory.Items.Find(S => S.id == 104).value - usedSpace;
            double howManyFilled = (((double)collectRocks.rocksCollected)/5) + usedSpace;
            int howMuchTotal = MainWindow.settings.userStats.inventory.Items.Find(S => S.id == 104).value * 5;
            if (collectRocks.rocksCollected == 0)
                DrawText("You were unable to collect any rocks.", 110, 200, 30, WHITE);
            else if (howManyFilled <= (freeSpace*5))
            {
                DrawText("From the rocks you collected you were\nable to fill " + (((double)collectRocks.rocksCollected)/5).ToString("0.0") + " " + ((collectRocks.rocksCollected/5) == 1 ? "box" : "boxes") + ".", 110, 200, 30, WHITE);
            }
            else if (howManyFilled > freeSpace && freeSpace > 0)
            {
                DrawText("From the rocks you collected you were\nable to fill " + (((double)collectRocks.rocksCollected)/5).ToString("0.0") + " " + ((collectRocks.rocksCollected/5) == 1 ? "box" : "boxes") + ". However you only\nhave " + freeSpace.ToString("0.0") + " " + (freeSpace == 1 ? "box" : "boxes") + " remaining." , 110, 200, 30, WHITE);
            }
            else 
            {
                DrawText("From the rocks you collected you were\nable to fill " + (((double)collectRocks.rocksCollected)/5).ToString("0.0") + " boxes. However you have\nno remaining boxes." , 110, 200, 30, WHITE);
            }
        }

        private static void Confirmation(){
            if (screen.PressSPACEBAR()) {
                double usedSpace = MainWindow.settings.userStats.inventory.Items.Find(S => S.id == 104).capacity;
                double freeSpace = MainWindow.settings.userStats.inventory.Items.Find(S => S.id == 104).value - usedSpace;
                double howManyFilled = (((double)collectRocks.rocksCollected)/5) + usedSpace;
                if (howManyFilled > MainWindow.settings.userStats.inventory.Items.Find(S => S.id == 104).value) { howManyFilled = MainWindow.settings.userStats.inventory.Items.Find(S => S.id == 104).value; }
                int howMuchTotal = MainWindow.settings.userStats.inventory.Items.Find(S => S.id == 104).value * 5;
                if (collectRocks.rocksCollected == 0)
                {}
                else if (howManyFilled <= (freeSpace*5))
                {
                    MainWindow.settings.userStats.inventory.Items.Find(S => S.id == 104).capacity = howManyFilled;
                }
                else if (howManyFilled > freeSpace && freeSpace > 0)
                {
                    MainWindow.settings.userStats.inventory.Items.Find(S => S.id == 104).capacity = howManyFilled;
                }

                MainWindow.settings.userStats.currentTime = MainWindow.settings.userStats.currentTime.AddHours(6);
                mainTrail.UseFood(1);
                mainTrail.CheckHealth();

                collectRocks.rocksCollected = 0;
                collectRocks.CollectRocksState = "Controls";
                MainWindow.settings.currentScreen = "Check Stats";
                loopCount = 0;
            }
        }
         // Boxes
        private static Texture2D boxesTexture = new Texture2D();
        private static void Boxes()
        {
            if (boxesTexture.height == 0) {
                Image img = LoadImage(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Images/rock box.png"));
                boxesTexture = LoadTextureFromImage(img);
                UnloadImage(img);
            }
            DrawTextureEx(boxesTexture, new Vector2(50, Raylib.GetScreenHeight()-200), 0f, 1f, WHITE);
        }
    }
}
