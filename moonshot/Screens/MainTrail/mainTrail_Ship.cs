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
    partial class mainTrail : screen
    {
        public static void ShipBroke()
        {
            LargePopUp("Ship is broken. Continue to try\nand repair.", false, "repair ship");
        }
    }

    class RepairShip : screen
    {
        public override string Name {
            get { return "Repair Ship"; }
        }
        private static bool confirmed = false;
        private static int switchInt = 0;
        public override void Display()
        {
            if (switchInt == 0)
            {
                Random r = new Random();
                switchInt = r.Next(1,3);
            }
            if (confirmed) {
                switch (switchInt)
                {
                    case 1:
                        if (MainWindow.settings.userStats.inventory.Items.Find(s => s.id == 105).value > 0)
                        {
                            DisplayMessage("Ship repaired using spare part!\nPress Space Bar to continue.");
                            if (screen.PressSPACEBAR())
                            {
                                confirmed = false;
                                MainWindow.settings.userStats.inventory.Items.Find(s => s.id == 105).value--;
                                MainWindow.settings.userStats.ShipWorking = true;
                                mainTrail.StartAnimation = true;
                                MainWindow.settings.currentScreen = "main trail";
                                switchInt = 0;
                            }
                        }
                        else {
                            DisplayMessage("Unable to repair!\n\n                    ...\n\nYou're stranded!");
                            if (screen.PressSPACEBAR())
                            {
                                confirmed = false;
                                MainWindow.settings.currentScreen = "tombstone";
                                switchInt = 0;
                            }
                        }
                        break;
                    default:
                        DisplayMessage("Ship repaired! Press Space Bar\nto continue.");
                        if (screen.PressSPACEBAR())
                        {
                            confirmed = false;
                            MainWindow.settings.userStats.ShipWorking = true;
                            mainTrail.StartAnimation = true;
                            MainWindow.settings.currentScreen = "main trail";
                            switchInt = 0;
                        }
                        break;
                }
            } else{
                switch (switchInt)
                {
                    case 1:
                        DisplayMessage("Unable to repair, press space bar\nto use spare ship part.");
                        if (screen.PressSPACEBAR())
                            confirmed = true;
                        break;
                    default:
                        confirmed = true;
                        break;
                }
            }
        }
        private static void DisplayMessage(string message)
        {
            string[] messageArray = message.Split("\n");
            Raylib.DrawRectangleRounded(new Rectangle(Raylib.GetScreenWidth()/8, Raylib.GetScreenHeight()/3, Raylib.GetScreenWidth()/8*6, 40+(messageArray.Length*30)), 0.25f, 10, WHITE);
            Raylib.DrawRectangle(Raylib.GetScreenWidth()/8+10, Raylib.GetScreenHeight()/3+10, Raylib.GetScreenWidth()/8*6-20, 20+(messageArray.Length*30), BLACK);
            for (int i = 0;i < messageArray.Length;i++)
            {
                Raylib.DrawText(messageArray[i], Raylib.GetScreenWidth()/8+50, Raylib.GetScreenHeight()/3+20+(30*i), 30, WHITE);
            }
        }
    }
}