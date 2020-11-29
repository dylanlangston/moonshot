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
        private static int oxygenCounter = 0;
        public static void CheckOxygen()
        {
            oxygenCounter++;
            if (oxygenCounter > 100) {
                oxygenCounter = 0;
                if (MainWindow.settings.userStats.inventory.Items.Find(s => s.id == 101).value < MainWindow.settings.userStats.crew.Party.Count)
                {
                    int reduceBy = MainWindow.settings.userStats.crew.Party.Count - MainWindow.settings.userStats.inventory.Items.Find(s => s.id == 101).value;
                    ReduceHealth(reduceBy);
                }
            }
        }
    }
}