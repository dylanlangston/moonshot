using Raylib_cs;
using static Raylib_cs.Raylib;
using static Raylib_cs.Color;
using System;
using System.Numerics;
using System.Collections.Generic;

namespace moonshot.Screens
{
    class editTopTen : screen
    {
        public override string Name {
            get { return "Edit Top Ten"; }
        }
        public override void Display()
        {
            ClearBackground(Colors.space);
            starscape();
            DisplayTopTen();
        }
        private static string selection = String.Empty;
        private static string confirm = String.Empty;
        private static int backSpaceLoop = 0;
        private static bool confirmName = false;
        private static void DisplayTopTen() {

            Raylib.DrawText("The Moon Top Ten", (Raylib.GetScreenWidth()-("The Moon Top Ten".Length*18))/2, 10, 30, WHITE);

            Raylib.DrawText("Name", 100, 50, 30, WHITE);
            Raylib.DrawText("Points", 390, 50, 30, WHITE);
            Raylib.DrawText("Rating", 600, 50, 30, WHITE);

            List<string> ranks = new List<string>() { "Commander", "Command Pilot", "Command Pilot", "Pilot", "Pilot", "Pilot", "Pilot", "Pilot", "Pilot", "Pilot", "Pilot", "Pilot" };

            bool qualifyForTopTen = false;
            int playerToReplace = 0;
            if (arrivalPoints.Score == 0)
                arrivalPoints.CalcPoints();
            int Score = arrivalPoints.Score * (MainWindow.settings.userStats.playerType == PlayerType.apollo12 ? 2 : 1) * (MainWindow.settings.userStats.playerType == PlayerType.apollo14 ? 3 : 1);
            for(int i = 0;i < MainWindow.settings.topTen.Leaders.Count;i++)
            {
                if (Score > MainWindow.settings.topTen.Leaders[i].Item2 && !qualifyForTopTen)
                {
                    qualifyForTopTen = true;
                    playerToReplace = i;
                    Raylib.DrawText(selection + (confirmName ? "" : "_"), 30, 100 + (i * 30), 30, WHITE);
                    if (confirmName)
                    {
                        Raylib.DrawText(Score.ToString(), 400, 100 + (i * 30), 30, WHITE);
                        Raylib.DrawText(ranks[i], 575, 100 + (i * 30), 30, WHITE);
                    }
                } 
                else {
                    Raylib.DrawText(MainWindow.settings.topTen.Leaders[i].Item1, 30, 100 + (i * 30), 30, WHITE);
                    Raylib.DrawText(MainWindow.settings.topTen.Leaders[i].Item2.ToString(), 400, 100 + (i * 30), 30, WHITE);
                    Raylib.DrawText(ranks[i], 575, 100 + (i * 30), 30, WHITE);
                }
            }

            if (qualifyForTopTen) {
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
                if (!confirmName) {
                    switch (selection) {
                        default:
                            if (!string.IsNullOrEmpty(selection)) {
                                confirmName = true;
                            }
                            break;
                    }
                } else {
                    switch (confirm) {
                        case "y":
                            selection = "";
                            confirmName = false;
                            break;
                        default:
                            MainWindow.settings.topTen.Leaders[playerToReplace] = (selection, Score);
                            MainWindow.settings.SaveSettings();
                            MainWindow.settings.currentScreen = "welcome";
                            selection = "";
                            confirmName = false;
                            break;
                    }
                    confirm = "";
                }
            }
            switch (keypress){
                case 0:
                    break;
                case 9000:
                    if (!confirmName) {
                        if (selection.Length > 0)
                            selection = selection.Remove(selection.Length-1, 1);

                    } else {
                        confirm = "";
                    }
                    break;
                case '<':
                    break;
                case '>':
                    break;
                default:
                    if (!confirmName) {
                        if (selection.Length < 16)
                        selection += ((Char)keypress);
                    } else {
                        if (keypress == 121)
                            confirm = "y";
                        else if (keypress == 110)
                            confirm = "n";
                    }
                    break;
            }


            if (confirmName) {
                Raylib.DrawText("Would you like to make any changes? " + confirm + "_", Raylib.GetScreenWidth()/8, (Raylib.GetScreenHeight()/10*9), 30, WHITE);
            }
            else {
                Raylib.DrawText("Congradulations! Type your name as you would\nlike to see it on the Moon Top Ten list.", 30, 500, 30, WHITE);
            }
            } 
            else {
                Raylib.DrawText("You have accumulated " + Score + " points. This is\nnot enough to qualify for the Moon Top Ten.", 30, 470, 30, WHITE);
                if (PressSPACEBAR()) {
                    MainWindow.settings.currentScreen = "welcome";
                }
            }
        }
    }
}
