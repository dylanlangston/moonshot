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
    partial class collectRocks : screen
    {
        public override string Name {
            get { return "Collect Rocks Game"; }
        }
        internal static string CollectRocksState = "Controls";
        private static List<collectRocksMiniGameScreen> CollectRockStates = new List<collectRocksMiniGameScreen>() { 
            new collectRocksControls(),
            new collectRocksGame()
         };
        public override void Display()
        {
            foreach (collectRocksMiniGameScreen scrn in CollectRockStates)
            {
                if (CollectRocksState.ToLower() == scrn.Name.ToLower())
                ((collectRocksMiniGameScreen)scrn).Display();
            }
        }
    }
}
