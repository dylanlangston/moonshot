using Raylib_cs;
using static Raylib_cs.Raylib;
using static Raylib_cs.Color;
using System;
using System.Numerics;
using System.Collections.Generic;
using System.IO;

namespace moonshot.Screens
{
    class BuySupplies : screen
    {
        public override string Name {
            get { return "Buy Supplies"; }
        }
        
        public override void Display()
        {
            ClearBackground(Colors.space);
            starscape();
            UserStats stats = MainWindow.settings.userStats;
        }
    }
}
