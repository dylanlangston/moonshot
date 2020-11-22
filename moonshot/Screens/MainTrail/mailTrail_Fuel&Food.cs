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
        public static Tuple<int, int> GetFoodAndFuelMod()
        {
            string Rations = MainWindow.settings.userStats.rations;
            string Pace = MainWindow.settings.userStats.pace;

            int paceModifier = 0;
            switch (Pace)
            {
                case (PlayerPace.grueling):
                paceModifier = 3;
                break;
                case (PlayerPace.strenuous):
                paceModifier = 2;
                break;
                case (PlayerPace.steady):
                paceModifier = 1;
                break;
                default: 
                break;
            }

            int rationsModifier = 0;
            switch (Rations)
            {
                case (PlayerRations.bareBones):
                rationsModifier = 1;
                break;
                case (PlayerRations.meager):
                rationsModifier = 2;
                break;
                case (PlayerRations.filling):
                rationsModifier = 3;
                break;
                default:
                break;
            }

            return new Tuple<int, int>(paceModifier, rationsModifier);
        }

        public static void UseFood(int amount)
        {
            if (MainWindow.settings.userStats.inventory.Items.Find(s => s.id == 103).value > 0)
                MainWindow.settings.userStats.inventory.Items.Find(s => s.id == 103).value -= amount;
            else
                MainWindow.settings.currentScreen = "tombstone";
        }
    }
}