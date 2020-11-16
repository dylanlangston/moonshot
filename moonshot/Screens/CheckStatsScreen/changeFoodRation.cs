using Raylib_cs;
using static Raylib_cs.Raylib;
using static Raylib_cs.Color;
using System;
using System.Numerics;
using System.Collections.Generic;
using System.IO;

namespace moonshot.Screens
{
    class ChangeFoodRation : screen
    {
        public override string Name {
            get { return "Change Food Ration"; }
        }
        
        public override void Display()
        {
            ClearBackground(Colors.space);
            starscape();
            Menuline(Raylib.GetScreenWidth() / 128, Raylib.GetScreenHeight()/12);
            Menuline(Raylib.GetScreenWidth() / 128, Raylib.GetScreenHeight() / 24 * 20 );
            CurrentRations(MainWindow.settings.userStats.rations);
            ChooseRation();
        }
        private void CurrentRations(string Rations)
        {
            Raylib.DrawText("Change food rations", (Raylib.GetScreenWidth()-(304))/2, Raylib.GetScreenHeight()/5, 30, WHITE);
            string rationsFull = "(currently \"" + Rations + "\")";
            Raylib.DrawText(rationsFull, (Raylib.GetScreenWidth()-((rationsFull.Length) * 16))/2+4, Raylib.GetScreenHeight()/5+35, 30, WHITE);
            Raylib.DrawText("The amount of food that people in your", Raylib.GetScreenWidth()/8, Raylib.GetScreenHeight()/3, 30, WHITE); 
            Raylib.DrawText("crew eat each day can change. These", Raylib.GetScreenWidth()/8, Raylib.GetScreenHeight()/3 + 30, 30, WHITE);
            Raylib.DrawText("amounts are:", Raylib.GetScreenWidth()/8, Raylib.GetScreenHeight()/3 + 60, 30, WHITE);

            Raylib.DrawText("1.  Filling - meals are large & generous", Raylib.GetScreenWidth()/6, Raylib.GetScreenHeight()/3 + 100, 30, WHITE);
            Raylib.DrawText("2. Meager - meals are small, but", Raylib.GetScreenWidth()/6, Raylib.GetScreenHeight()/3 + 135, 30, WHITE);
            Raylib.DrawText("adequate", Raylib.GetScreenWidth()/6+35, Raylib.GetScreenHeight()/3 + 165, 30, WHITE);
            Raylib.DrawText("3. Bare Bones - meals are very small;", Raylib.GetScreenWidth()/6, Raylib.GetScreenHeight()/3 + 200, 30, WHITE);
            Raylib.DrawText("everyone stays hungry.", Raylib.GetScreenWidth()/6 + 35, Raylib.GetScreenHeight()/3 + 230, 30, WHITE);
        }
        private static string selection = String.Empty;
        private void ChooseRation()
        {
            Raylib.DrawText("What is your choice? " + selection + "_", Raylib.GetScreenWidth()/8, Raylib.GetScreenHeight()/2 + 170, 30, WHITE);
            int keypress = Raylib.GetKeyPressed();
            if (Raylib.IsKeyPressed(KeyboardKey.KEY_BACKSPACE)) {
                keypress = 9000;
            } else if (Raylib.IsKeyPressed(KeyboardKey.KEY_ENTER)) {
                switch (selection) {
                    case "1":
                        MainWindow.settings.userStats.rations = PlayerRations.filling;
                        MainWindow.settings.currentScreen = "Check Stats";
                        break;
                    case "2":
                        MainWindow.settings.userStats.rations = PlayerRations.meager;
                        MainWindow.settings.currentScreen = "Check Stats";
                        break;
                    case "3":
                        MainWindow.settings.userStats.rations = PlayerRations.bareBones;
                        MainWindow.settings.currentScreen = "Check Stats";
                        break;
                    default:
                        break;
                }
                selection = "";
            }
            switch (keypress){
                case '1':
                    selection = "1";
                    break;
                case '2':
                    selection = "2";
                    break;
                case '3':
                    selection = "3";
                    break;
                case 9000:
                    selection = String.Empty;
                    break;
                default:
                    break;
            }
        }
    }
}
