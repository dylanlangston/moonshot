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
        private static void CheckHealth()
        {
            int totalStatus = 0;
            foreach (PartyMember member in MainWindow.settings.userStats.crew.Party)
            {
                switch (member.status)
                {
                    case PlayerStatus.good:
                        totalStatus += 4;
                        break;
                    case PlayerStatus.fair:
                        totalStatus += 3;
                        break;
                    case PlayerStatus.poor:
                        totalStatus += 2;
                        break;
                    default:
                        totalStatus += 1;
                        break;
                }
            }
            Console.WriteLine(totalStatus);
            if (totalStatus < 6)
            {
                MainWindow.settings.userStats.status = PlayerStatus.veryPoor;
            } else if (totalStatus < 9)
            {
                MainWindow.settings.userStats.status = PlayerStatus.poor;
            } else if (totalStatus < 13)
            {
                MainWindow.settings.userStats.status = PlayerStatus.fair;
            } else {
                MainWindow.settings.userStats.status = PlayerStatus.good;
            }
        }
    }
}