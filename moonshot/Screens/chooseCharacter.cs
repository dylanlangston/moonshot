using Raylib_cs;
using static Raylib_cs.Raylib;
using static Raylib_cs.Color;
using System;
using System.Numerics;
using System.Windows.Input;

namespace moonshot.Screens
{
    class chooseCharacter : screen
    {
        public override string Name {
            get { return "Character Selection"; }
        }
        public override void Display()
        {
            ClearBackground(Colors.space);
            starscape();
            Menuline(Raylib.GetScreenWidth() / 128, Raylib.GetScreenHeight()/12);
            Menuline(Raylib.GetScreenWidth() / 128, Raylib.GetScreenHeight() / 24 * 20 );
            Menu();
        }
        internal static string selection = String.Empty;
        internal static void Menu() {
            Raylib.DrawText("Many kinds of people made the trip to\nthe Moon.", Raylib.GetScreenWidth()/8, Raylib.GetScreenHeight()/5, 30, WHITE);
            Raylib.DrawText("You may:", Raylib.GetScreenWidth()/8, Raylib.GetScreenHeight()/3 + 20, 30, WHITE);
            Raylib.DrawText("1.  Be with Neil A. Armstrong on Apollo 11", Raylib.GetScreenWidth()/7, Raylib.GetScreenHeight()/3 + 60, 30, WHITE);
            Raylib.DrawText("2. Be with Charles Conrad Jr. on Apollo 12", Raylib.GetScreenWidth()/7, Raylib.GetScreenHeight()/3 + 100, 30, WHITE);
            Raylib.DrawText("3. Be with Alan B. Shepard Jr. on Apollo 14", Raylib.GetScreenWidth()/7, Raylib.GetScreenHeight()/3 + 140, 30, WHITE);
            Raylib.DrawText("4. Find out the differences between\nthese choices", Raylib.GetScreenWidth()/7, Raylib.GetScreenHeight()/3 + 180, 30, WHITE);
            int keypress = Raylib.GetKeyPressed();
            if (Raylib.IsKeyPressed(KeyboardKey.KEY_BACKSPACE)) {
                keypress = 9000;
            } else if (Raylib.IsKeyPressed(KeyboardKey.KEY_ENTER)) {
                switch (selection) {
                    case "1":
                        MainWindow.settings.userStats.playerType = PlayerType.apollo11;
                        MainWindow.settings.userStats.crew =  new apollo11();
                        MainWindow.settings.currentScreen = "Enter Name";
                        break;
                    case "2":
                    MainWindow.settings.userStats.playerType = PlayerType.apollo12;
                        MainWindow.settings.userStats.crew =  new apollo12();
                        MainWindow.settings.currentScreen = "Enter Name";
                        break;
                    case "3":
                    MainWindow.settings.userStats.playerType = PlayerType.apollo14;
                        MainWindow.settings.userStats.crew =  new apollo14();
                        MainWindow.settings.currentScreen = "Enter Name";
                        break;
                    case "4":
                        MainWindow.settings.Running = false;
                        break;
                    default:
                        break;
                }
                selection = "";
            }
            switch (keypress){
                case 49:
                    selection = "1";
                    break;
                case 50:
                    selection = "2";
                    break;
                case 51:
                    selection = "3";
                    break;
                case 52:
                    selection = "4";
                    break;
                case 9000:
                    selection = String.Empty;
                    break;
                default:
                    break;
            }
            Raylib.DrawText("What is your choice? " + selection + "_", Raylib.GetScreenWidth()/8, Raylib.GetScreenHeight()/5*4, 30, WHITE);
        }
    }
}
