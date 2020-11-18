using Raylib_cs;
using static Raylib_cs.Raylib;
using static Raylib_cs.Color;
using System;
using System.Numerics;
using System.Collections.Generic;
using System.IO;

namespace moonshot.Screens
{
    class CheckSupplies : screen
    {
        public override string Name {
            get { return "Check Supplies"; }
        }
        
        public override void Display()
        {
            ClearBackground(Colors.space);
            starscape();
            Boxes();
            Food();
            Fuel();
            OxygenTanks();
            ShipParts();
            Supplies();
            Confirmation();
        }
        private static void Supplies(){
            Raylib.DrawText("Your Supplies", Raylib.GetScreenWidth()/10*4, 5, 30, WHITE);
            Raylib.DrawText("Oxygen Tank", Raylib.GetScreenWidth()/3, 75, 30, WHITE);
            Raylib.DrawText("Fuel", Raylib.GetScreenWidth()/3, 110, 30, WHITE);
            Raylib.DrawText("Food", Raylib.GetScreenWidth()/3, 145, 30, WHITE);
            Raylib.DrawText("Boxes", Raylib.GetScreenWidth()/3, 180, 30, WHITE);
            Raylib.DrawText("Spare Ship Parts", Raylib.GetScreenWidth()/3, 215, 30, WHITE);
            Raylib.DrawText("Money Left", Raylib.GetScreenWidth()/3, 250, 30, WHITE);

            Raylib.DrawText(MainWindow.settings.userStats.inventory.Items.Find(x => x.id == 101).value.ToString(), Raylib.GetScreenWidth()/10*8, 75, 30, WHITE);
            Raylib.DrawText(MainWindow.settings.userStats.inventory.Items.Find(x => x.id == 102).value.ToString(), Raylib.GetScreenWidth()/10*8, 110, 30, WHITE);
            Raylib.DrawText(MainWindow.settings.userStats.inventory.Items.Find(x => x.id == 103).value.ToString(), Raylib.GetScreenWidth()/10*8, 145, 30, WHITE);
            Raylib.DrawText(MainWindow.settings.userStats.inventory.Items.Find(x => x.id == 104).value.ToString(), Raylib.GetScreenWidth()/10*8, 180, 30, WHITE);
            Raylib.DrawText(MainWindow.settings.userStats.inventory.Items.Find(x => x.id == 105).value.ToString(), Raylib.GetScreenWidth()/10*8, 215, 30, WHITE);
            string money = "$" + MainWindow.settings.userStats.Money+".00";
            Raylib.DrawText(money, (Raylib.GetScreenWidth()/10*8)-((money.Length-4)*16), 250, 30, WHITE);
        }
        private static void Confirmation(){
            if (PressSPACEBAR()) {
                MainWindow.settings.currentScreen = "Check Stats";
            }
        }

        // Boxes
        internal static Texture2D boxesTexture = new Texture2D();
        internal static void Boxes()
        {
            if (boxesTexture.height == 0) {
                Image img = LoadImage(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Images/rock box.png"));
                boxesTexture = LoadTextureFromImage(img);
                UnloadImage(img);
            }
            DrawTextureEx(boxesTexture, new Vector2(Raylib.GetScreenWidth()/2-100, Raylib.GetScreenHeight()-280), 0f, 1f, WHITE);
        }
        // Food
        internal static Texture2D foodTexture = new Texture2D();
        internal static void Food()
        {
            if (foodTexture.height == 0) {
                Image img = LoadImage(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Images/food.png"));
                foodTexture = LoadTextureFromImage(img);
                UnloadImage(img);
            }
            DrawTextureEx(foodTexture, new Vector2(0, 50), 0f, 1f, WHITE);
        }
        // Fuel
        internal static Texture2D fuelTexture = new Texture2D();
        internal static void Fuel()
        {
            if (fuelTexture.height == 0) {
                Image img = LoadImage(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Images/fuel.png"));
                fuelTexture = LoadTextureFromImage(img);
                UnloadImage(img);
            }
            DrawTextureEx(fuelTexture, new Vector2(Raylib.GetScreenWidth()-230, Raylib.GetScreenHeight()-210), 0f, 1f, WHITE);
        }
        // Oxygen Tanks
        internal static Texture2D oxygenTanksTexture = new Texture2D();
        internal static void OxygenTanks()
        {
            if (oxygenTanksTexture.height == 0) {
                Image img = LoadImage(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Images/oxygentanks.png"));
                oxygenTanksTexture = LoadTextureFromImage(img);
                UnloadImage(img);
            }
            DrawTextureEx(oxygenTanksTexture, new Vector2(0, Raylib.GetScreenHeight()-200), 0f, 1f, WHITE);
        }
        // Ship Parts
        internal static Texture2D shipPartsTexture = new Texture2D();
        internal static void ShipParts()
        {
            if (shipPartsTexture.height == 0) {
                Image img = LoadImage(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Images/spareParts.png"));
                shipPartsTexture = LoadTextureFromImage(img);
                UnloadImage(img);
            }
            DrawTextureEx(shipPartsTexture, new Vector2(15, 210), 0f, 1f, WHITE);
        }
    }
}
