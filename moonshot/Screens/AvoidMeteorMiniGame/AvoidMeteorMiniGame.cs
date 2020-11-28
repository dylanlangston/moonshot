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
    partial class AvoidMeteorMiniGame : screen
    {
        public static int rocksCollected = 0;
        public override string Name {
            get { return "Avoid Meteor Game"; }
        }
        internal static string avoidMeteorState = "Selection";
        private static List<AvoidMeteorMiniGameScreen> avoidMeteorStates = new List<AvoidMeteorMiniGameScreen>() { 
            new avoidMeteorSelection(),
            new avoidMeteorGame()
         };
        public override void Display()
        {
            foreach (AvoidMeteorMiniGameScreen scrn in avoidMeteorStates)
            {
                if (avoidMeteorState.ToLower() == scrn.Name.ToLower())
                ((AvoidMeteorMiniGameScreen)scrn).Display();
            }
        }
    }
}
