using Raylib_cs;
using static Raylib_cs.Raylib;
using static Raylib_cs.Color;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Windows.Input;

namespace moonshot.Screens
{
    class enterName : screen
    {
        public override string Name {
            get { return "Enter Name"; }
        }
        public override void Display()
        {
            ClearBackground(Colors.space);
            starscape();
            Menu();
        }
        internal static string selection = String.Empty;
        internal static string confirm = String.Empty;
        private static int backSpaceLoop = 0;
        private static bool confirmName = false;
        internal static void Menu() {
            Raylib.DrawText("What is the first name of your final\ncrew member?", Raylib.GetScreenWidth()/8,  (Raylib.GetScreenHeight()/10*6)-30, 30, WHITE);
            Raylib.DrawText("1.  " + MainWindow.settings.userStats.crew.Party[0].name, Raylib.GetScreenWidth()/8, (Raylib.GetScreenHeight()/10*9)-120, 30, WHITE);
            Raylib.DrawText("2. " + MainWindow.settings.userStats.crew.Party[1].name, Raylib.GetScreenWidth()/8, (Raylib.GetScreenHeight()/10*9)-90, 30, WHITE);
            Raylib.DrawText("3. " + MainWindow.settings.userStats.crew.Party[2].name, Raylib.GetScreenWidth()/8, (Raylib.GetScreenHeight()/10*9)-60, 30, WHITE);

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
                        case "4":
                            MainWindow.settings.Running = false;
                            break;
                        default:
                            if (!string.IsNullOrEmpty(selection)) {
                                confirmName = true;
                            }
                            break;
                    }
                } else {
                    switch (confirm) {
                        case "y":
                            MainWindow.settings.userStats.crew.Party.Add(new PartyMember(selection, PlayerStatus.good));
                            MainWindow.settings.currentScreen = "Supplies Screen One";
                            selection = "";
                            break;
                        default:
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

            Raylib.DrawText("4. " + selection + (confirmName ? "" : "_"), Raylib.GetScreenWidth()/8, (Raylib.GetScreenHeight()/10*9)-30, 30, WHITE);

            if (confirmName) {
                Raylib.DrawText("Are these names correct? " + confirm + "_", Raylib.GetScreenWidth()/8, (Raylib.GetScreenHeight()/10*9), 30, WHITE);
            }
        }
    }
}
