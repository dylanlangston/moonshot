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
    partial class avoidMeteorSelection : AvoidMeteorMiniGameScreen
    {
        public override string Name {
            get { return "Selection"; }
        }
        public override void Display()
        {
            ClearBackground(Colors.space);
            screen.starscape();
            screen.Menuline(Raylib.GetScreenWidth() / 128, Raylib.GetScreenHeight()/12);
            screen.Menuline(Raylib.GetScreenWidth() / 128, Raylib.GetScreenHeight() / 24 * 20 );
            Messages();
            Selection();
        }

        private static void Messages()
        {
            DateTime time = MainWindow.settings.userStats.currentTime;
            Raylib.DrawText("Meteor Shower Detected", 200, 10, 30 ,WHITE);
            Raylib.DrawText(time.ToString("MMMM dd, yyyy"), (Raylib.GetScreenWidth()-(time.ToString("MMMM dd, yyyy").Length * 16))/2, 40, 30, WHITE);
            Raylib.DrawText("You may:", 100, 150, 30, WHITE);

            Raylib.DrawText("1. Try to avoid the meteors", 110, 210, 30, WHITE);
            Raylib.DrawText("2. Wait for the storm to pass, lose 1 day", 110, 250, 30, WHITE);
        }
        
        private static string selection = String.Empty;
        private static void Selection()
        {
            int keypress = Raylib.GetKeyPressed();
            if (Raylib.IsKeyPressed(KeyboardKey.KEY_BACKSPACE)) {
                keypress = 9000;
            } else if (Raylib.IsKeyPressed(KeyboardKey.KEY_ENTER)) {
                switch (selection) {
                    case "1":
                        AvoidMeteorMiniGame.avoidMeteorState= "Game";
                        break;
                    case "2":
                        MainWindow.settings.userStats.currentTime = MainWindow.settings.userStats.currentTime.AddDays(1);
                        mainTrail.UseFood(3);
                        mainTrail.CheckHealth();
                        MainWindow.settings.currentScreen = "Main Trail";
                        mainTrail.StartAnimation = true;
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
                case 9000:
                    selection = String.Empty;
                    break;
                default:
                    break;
            }

            Raylib.DrawText("What is your choice? " + selection + "_", 100, 450, 30, WHITE);
        }
    }
}
