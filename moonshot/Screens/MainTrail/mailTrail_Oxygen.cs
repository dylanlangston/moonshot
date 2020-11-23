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
        public static void CheckOxygen()
        {
            if (MainWindow.settings.userStats.inventory.Items.Find(s => s.id == 101).value == 0)
            {
                StartAnimation = false;
                MainWindow.settings.currentScreen = "tombstone";
            }
        }
    }
}