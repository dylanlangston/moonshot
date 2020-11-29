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
        private static Random r = new Random();
        internal static void CalamityChance()
        {
            int Rand= r.Next(0,500);

            switch (Rand)
            {
                case 10:
                    MeteorShower();
                    break;
                default:
                    break;
            }

        }
        
        private static void MeteorShower()
        {
            DisplayNewPopUp("Warning: Meteor Shower!", false, "Avoid Meteor Game");
        }
    }
}