using Raylib_cs;
using static Raylib_cs.Raylib;
using static Raylib_cs.Color;
using System;
using System.Numerics;
using System.Windows.Input;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace moonshot.Screens
{
    partial class mainTrail : screen
    {
        private static void DisplayIcon()
        {
            int currentLocationMod = 1;
            int currentLocation = MainWindow.settings.userStats.currentLocation;
            int milesTraveled = MainWindow.settings.userStats.milesTraveled;
            double milesPercent = (double)milesTraveled/((double)LocationsAndDistances[currentLocation].Item3);
            if (milesPercent == 0 && !StartAnimation && String.IsNullOrEmpty(tempPopUpMessage))
            {
                milesPercent = 1;
                currentLocationMod = 0;
            }
            switch (currentLocation+currentLocationMod)
            {
                case (2):
                    mtpIcon(175 + (int)(250*milesPercent), 75);
                    break;
                case (4):
                    posidoniusIcon(175 + (int)(250*milesPercent), 75);
                    break;
                default:
                    mareTranIcon(175 + (int)(250*milesPercent), 75);
                    break;
            }
        }
        private static Texture2D mareTranIconTexture = new Texture2D();
        private static void mareTranIcon(int x, int y)
        {
            if (mareTranIconTexture.height == 0) {
                Image img = LoadImage(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Images/mareTranIcon.png"));
                mareTranIconTexture = LoadTextureFromImage(img);
                UnloadImage(img);
            }
            DrawTextureEx(mareTranIconTexture, new Vector2(x, y), 0f, 0.75f, WHITE);
        }
        private static Texture2D mtpIconTexture = new Texture2D();
        private static void mtpIcon(int x, int y)
        {
            if (mtpIconTexture.height == 0) {
                Image img = LoadImage(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Images/mtpIcon.png"));
                mtpIconTexture = LoadTextureFromImage(img);
                UnloadImage(img);
            }
            DrawTextureEx(mtpIconTexture, new Vector2(x, y), 0f, 0.75f, WHITE);
        }
        private static Texture2D posidoniusIconTexture = new Texture2D();
        private static void posidoniusIcon(int x, int y)
        {
            if (posidoniusIconTexture.height == 0) {
                Image img = LoadImage(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Images/posidoniusIcon.png"));
                posidoniusIconTexture = LoadTextureFromImage(img);
                UnloadImage(img);
            }
            DrawTextureEx(posidoniusIconTexture, new Vector2(x, y), 0f, 0.75f, WHITE);
        }
    }
}