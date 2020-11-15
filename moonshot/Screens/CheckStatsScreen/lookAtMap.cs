using Raylib_cs;
using static Raylib_cs.Raylib;
using static Raylib_cs.Color;
using System;
using System.Numerics;
using System.Collections.Generic;
using System.IO;

namespace moonshot.Screens
{
    class LookAtMap : screen
    {
        public override string Name {
            get { return "Look at Map"; }
        }
        
        public override void Display()
        {
            ClearBackground(Colors.space);
            starscape();
            Map();
            MapProgress(MainWindow.settings.userStats.currentLocation);
            Confirmation();
        }
        private static void Confirmation(){
            if (PressSPACEBAR()) {
                MainWindow.settings.currentScreen = "Check Stats";
            }
        }
        private new static bool PressSPACEBAR() {
            Raylib.DrawText("Press SPACE BAR to continue", 30, Raylib.GetScreenHeight()-25, 20, WHITE);
            if (Raylib.IsKeyReleased(KeyboardKey.KEY_SPACE)) {
                return true;
            }
            return false;
        }
        private static void MapProgress(int Progress)
        {
            for (int i = 1; i <= Progress-1; i++) {
                switch (i){
                    case 1:
                        Raylib.DrawLine(292, 513, 318, 495, WHITE);
                        Raylib.DrawLine(292, 514, 318, 496, WHITE);
                        break;
                    case 2:
                        Raylib.DrawLine(318, 495, 366, 466, WHITE);
                        Raylib.DrawLine(318, 496, 366, 467, WHITE);
                        break;
                    case 3:
                        Raylib.DrawLine(366, 466, 328, 416, WHITE);
                        Raylib.DrawLine(366, 467, 328, 417, WHITE);
                        break;
                    case 4:
                        Raylib.DrawLine(328, 416, 330, 336, WHITE);
                        Raylib.DrawLine(329, 417, 331, 337, WHITE);
                        break;
                    case 5:
                        Raylib.DrawLine(330, 336, 363, 281, WHITE);
                        Raylib.DrawLine(331, 337, 364, 282, WHITE);
                        break;
                    case 6:
                        Raylib.DrawLine(363, 281, 408, 217, WHITE);
                        Raylib.DrawLine(364, 282, 409, 218, WHITE);
                        break;
                    case 7:
                        Raylib.DrawLine(408, 217, 390, 158, WHITE);
                        Raylib.DrawLine(409, 218, 391, 159, WHITE);
                        break;
                    case 8:
                        Raylib.DrawLine(390, 158, 383, 92, WHITE);
                        Raylib.DrawLine(391, 159, 384, 93, WHITE);
                        break;
                    case 9:
                        Raylib.DrawLine(383, 92, 397, 33, WHITE);
                        Raylib.DrawLine(384, 93, 398, 34, WHITE);
                        break;
                    default:
                        break;
                }
            }
        }
        // Map
        private static Texture2D mapTexture = new Texture2D();
        private static void Map()
        {
            if (mapTexture.height == 0) {
                Image img = LoadImage(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Images/mapofmoon.png"));
                mapTexture = LoadTextureFromImage(img);
                UnloadImage(img);
            }
            DrawTextureEx(mapTexture, new Vector2(0, 0), 0f, 1f, WHITE);
        }
    }
}
