using Raylib_cs;
using static Raylib_cs.Raylib;
using static Raylib_cs.Color;
using System;
using System.Numerics;
using System.IO;

namespace moonshot.Screens
{
    class tombstone : screen
    {
        public override string Name {
            get { return "tombstone"; }
        }
        private static bool firstRun = true;
        private static bool spaceBarPressed = false;
        private static int epitaphState = 0;
        public override void Display()
        {
            ClearBackground(Colors.space);
            starscape();
            Moon();
            stone();
        }
        private static void stone(){
            Tombstone();
            if (spaceBarPressed)
            {
                switch (epitaphState)
                {
                    case (1):
                        WhatOnTombstone();
                        break;
                    case (2):
                        MakeChanges();
                        break;
                    case (10):
                        AllCrewMembersDied();
                        break;
                    default:
                        WriteEpitaph();
                        break;
                }
                
            } else {
            if (PressSPACEBAR()) {
                spaceBarPressed = true;
            }
            }
        }
        private static string epitaphSection = String.Empty;
        private static void WriteEpitaph()
        {
            Raylib.DrawRectangle(0, 550, Raylib.GetScreenWidth(), 50, BLACK);
            
            int keypress = Raylib.GetKeyPressed();
            if (Raylib.IsKeyPressed(KeyboardKey.KEY_BACKSPACE)) {
                keypress = 9000;
            } else if (Raylib.IsKeyPressed(KeyboardKey.KEY_ENTER)) {
                switch (epitaphSection) {
                    case "y":
                        epitaphState = 1;
                        break;
                    case "n":
                        epitaphState = 10;
                        break;
                    default:
                        break;
                }
                epitaphSection = "";
            }
            switch (keypress){
                case 'y':
                    epitaphSection = "y";
                    break;
                case 'n':
                    epitaphSection = "n";
                    break;
                case 9000:
                    epitaphSection = String.Empty;
                    break;
                default:
                    break;
            }

            Raylib.DrawText("Would you like to write an epitaph? " + epitaphSection + "_", 30, 555, 30, WHITE);
        }

        private static string selection = String.Empty;
        private static int backSpaceLoop = 0;
        private static void WhatOnTombstone()
        {

            Raylib.DrawRectangle(0, 530, Raylib.GetScreenWidth(), 70, BLACK);
            Raylib.DrawText("What would you like on the tombstone?", 90, 535, 30, WHITE);
            int keypress = Raylib.GetKeyPressed();

            if (Raylib.IsKeyDown(KeyboardKey.KEY_BACKSPACE)) {
                if (Raylib.IsKeyPressed(KeyboardKey.KEY_BACKSPACE)) {
                    keypress = 9000;
                    backSpaceLoop = 0;
                } else {
                    backSpaceLoop++;
                    if (backSpaceLoop > 6) {
                        keypress = keypress == 0 ? 9000 : keypress;
                        backSpaceLoop = 0;
                    }
                }
            } else if (Raylib.IsKeyPressed(KeyboardKey.KEY_ENTER)) {
                switch (selection) {
                    default:
                        if (!string.IsNullOrEmpty(selection)) 
                            tombstoneEpitaph = selection;
                            epitaphState = 2;
                        break;
                    }
            }
            switch (keypress){
                case 0:
                    break;
                case 9000:
                    if (selection.Length > 0)
                        selection = selection.Remove(selection.Length-1, 1);
                    break;
                case '<':
                    break;
                case '>':
                    break;
                default:
                    if (selection.Length < 40)
                        selection += ((Char)keypress);
                    break;
            }
            Raylib.DrawText(selection + "_", 30, 565, 30, WHITE);
        }
        private static void MakeChanges()
        {
            Raylib.DrawRectangle(0, 550, Raylib.GetScreenWidth(), 50, BLACK);
            
            int keypress = Raylib.GetKeyPressed();
            if (Raylib.IsKeyPressed(KeyboardKey.KEY_BACKSPACE)) {
                keypress = 9000;
            } else if (Raylib.IsKeyPressed(KeyboardKey.KEY_ENTER)) {
                switch (epitaphSection) {
                    case "y":
                        epitaphState = 1;
                        break;
                    case "n":
                        epitaphState = 10;
                        break;
                    default:
                        break;
                }
                epitaphSection = "";
            }
            switch (keypress){
                case 'y':
                    epitaphSection = "y";
                    break;
                case 'n':
                    epitaphSection = "n";
                    break;
                case 9000:
                    epitaphSection = String.Empty;
                    break;
                default:
                    break;
            }

            Raylib.DrawText("Would you like to make changes? " + epitaphSection + "_", 30, 555, 30, WHITE);
        }
        private static void AllCrewMembersDied()
        {
            Raylib.SetMasterVolume(1f);
            if (!Raylib.IsSoundPlaying(sound))
                Raylib.PlaySound(sound);
            Raylib.DrawRectangle(0, 530, Raylib.GetScreenWidth(), 70, BLACK);
            Raylib.DrawText("All of the people in your crew have died.", 90, 535, 30, WHITE);
            if (PressSPACEBAR()) {
                firstRun = true;
                spaceBarPressed = false;
                epitaphState = 0;
                if (Raylib.IsSoundPlaying(sound))
                    Raylib.StopSound(sound);
                MainWindow.settings.currentScreen = "welcome";
            }
        }

        private static Sound sound = LoadSound("Music/SELFMADE-space-taps.wav");
        private static string tombstoneEpitaph = String.Empty;
        // tombstone
        private static Texture2D tombstoneTexture = new Texture2D();
        private static void Tombstone()
        {
            Raylib.SetMasterVolume(1f);
            if (tombstoneTexture.height == 0) {
                Image img = LoadImage(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Images/tombstone.png"));
                tombstoneTexture = LoadTextureFromImage(img);
                UnloadImage(img);
            }
            DrawTextureEx(tombstoneTexture, new Vector2(0, 0), 0f, 1f, WHITE);

            Raylib.DrawText("Here lies", 330, 150, 30, BLACK);
            Raylib.DrawText(MainWindow.settings.userStats.crew.Party[MainWindow.settings.userStats.crew.Party.Count-1].name, (Raylib.GetScreenWidth() - (MainWindow.settings.userStats.crew.Party[MainWindow.settings.userStats.crew.Party.Count-1].name.Length * 16))/2, 180, 30, BLACK);
            

            string epitaphPart1 = String.Empty;
            string epitaphPart2 = String.Empty;
            if (tombstoneEpitaph.Length > 0)
            {
                epitaphPart1 = tombstoneEpitaph.Substring(0, tombstoneEpitaph.Length > 20 ? 20 : tombstoneEpitaph.Length);
                if (tombstoneEpitaph.Length > 20)
                    epitaphPart2 = tombstoneEpitaph.Substring(20);
            }
            Raylib.DrawText(epitaphPart1, (Raylib.GetScreenWidth()-(epitaphPart1.Length*16))/2, 220, 30, BLACK);
            Raylib.DrawText(epitaphPart2, (Raylib.GetScreenWidth()-(epitaphPart1.Length*16))/2, 250, 30, BLACK);

            if (firstRun) 
            {
                firstRun = false;
                Raylib.PlaySound(sound);
            }
            
        }
        // Moon
        private static Texture2D moonTexture = new Texture2D();
        private static void Moon()
        {
            if (moonTexture.height == 0) {
                Image moon = LoadImage(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Images/moon.png"));
                moonTexture = LoadTextureFromImage(moon);
                UnloadImage(moon);
            }
            DrawTextureEx(moonTexture, new Vector2(Raylib.GetScreenWidth()/5*4, 0), 0f, 0.05f, WHITE);
        }
    }
}
