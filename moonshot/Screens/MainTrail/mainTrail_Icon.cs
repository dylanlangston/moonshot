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
                case (0):
                    landingIcon(175 + (int)(300*milesPercent), 75);
                    break;
                case (1):
                    mareTranIcon(175 + (int)(250*milesPercent), 75);
                    break;
                case (2):
                    mtpIcon(175 + (int)(250*milesPercent), 75);
                    break;
                case (3):
                    mareSerenIcon(175 + (int)(250*milesPercent), 75);
                    break;
                case (4):
                    posidoniusIcon(175 + (int)(250*milesPercent), 75);
                    break;
                case (5):
                    taurusIcon(175 + (int)(250*milesPercent), 75);
                    break;
                case (6):
                    aandhIcon(175 + (int)(250*milesPercent), 75);
                    break;
                case (7):
                    mareFrigIcon(175 + (int)(250*milesPercent), 75);
                    break;
                case (9):
                    pearyIcon(175 + (int)(250*milesPercent), 75);
                    break;
                default:
                    anaxagorasIcon(175 + (int)(250*milesPercent), 75);
                    break;
            }
        }
        private static Texture2D landingIconTexture = new Texture2D();
        private static void landingIcon(int x, int y)
        {
            if (landingIconTexture.height == 0) {
                Image img = LoadImage(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Images/landingIcon.png"));
                landingIconTexture = LoadTextureFromImage(img);
                UnloadImage(img);
            }
            DrawTextureEx(landingIconTexture, new Vector2(x, y), 0f, 0.5f, WHITE);
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
        private static Texture2D mareSerenIconTexture = new Texture2D();
        private static void mareSerenIcon(int x, int y)
        {
            if (mareSerenIconTexture.height == 0) {
                Image img = LoadImage(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Images/mareSerenIcon.png"));
                mareSerenIconTexture = LoadTextureFromImage(img);
                UnloadImage(img);
            }
            DrawTextureEx(mareSerenIconTexture, new Vector2(x, y), 0f, 0.75f, WHITE);
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
        private static Texture2D taurusIconTexture = new Texture2D();
        private static void taurusIcon(int x, int y)
        {
            if (taurusIconTexture.height == 0) {
                Image img = LoadImage(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Images/taurusIcon.png"));
                taurusIconTexture = LoadTextureFromImage(img);
                UnloadImage(img);
            }
            DrawTextureEx(taurusIconTexture, new Vector2(x, y), 0f, 0.75f, WHITE);
        }
        private static Texture2D aandhIconTexture = new Texture2D();
        private static void aandhIcon(int x, int y)
        {
            if (aandhIconTexture.height == 0) {
                Image img = LoadImage(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Images/ahIcon.png"));
                aandhIconTexture = LoadTextureFromImage(img);
                UnloadImage(img);
            }
            DrawTextureEx(aandhIconTexture, new Vector2(x, y), 0f, 0.75f, WHITE);
        }
        private static Texture2D mareFrigIconTexture = new Texture2D();
        private static void mareFrigIcon(int x, int y)
        {
            if (mareFrigIconTexture.height == 0) {
                Image img = LoadImage(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Images/mareFrigIcon.png"));
                mareFrigIconTexture = LoadTextureFromImage(img);
                UnloadImage(img);
            }
            DrawTextureEx(mareFrigIconTexture, new Vector2(x, y), 0f, 0.75f, WHITE);
        }
        private static Texture2D anaxagorasIconTexture = new Texture2D();
        private static void anaxagorasIcon(int x, int y)
        {
            if (anaxagorasIconTexture.height == 0) {
                Image img = LoadImage(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Images/anaxagorasIcon.png"));
                anaxagorasIconTexture = LoadTextureFromImage(img);
                UnloadImage(img);
            }
            DrawTextureEx(anaxagorasIconTexture, new Vector2(x, y), 0f, 0.75f, WHITE);
        }
        private static Texture2D pearyIconTexture = new Texture2D();
        private static void pearyIcon(int x, int y)
        {
            if (pearyIconTexture.height == 0) {
                Image img = LoadImage(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Images/pearyIcon.png"));
                pearyIconTexture = LoadTextureFromImage(img);
                UnloadImage(img);
            }
            DrawTextureEx(pearyIconTexture, new Vector2(x, y), 0f, 0.75f, WHITE);
        }
    }
}