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
        internal static void CheckHealth()
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
        private static void ReduceHealth(int loopCount = 2)
        {
            List<string> deadPlayers = new List<string>(){};
            for (int c = 0; c < loopCount; c++)
            {
                bool healthNeedsReduced = true;
                if (healthNeedsReduced)
                {
                    foreach (PartyMember member in MainWindow.settings.userStats.crew.Party.FindAll(c => c.status == PlayerStatus.good))
                    {
                        member.status = PlayerStatus.fair;
                        healthNeedsReduced = false;
                        break;
                    }
                }
                if (healthNeedsReduced)
                {
                    foreach (PartyMember member in MainWindow.settings.userStats.crew.Party.FindAll(c => c.status == PlayerStatus.fair))
                    {
                        member.status = PlayerStatus.poor;
                        healthNeedsReduced = false;
                        break;
                    }
                }
                if (healthNeedsReduced)
                {
                    foreach (PartyMember member in MainWindow.settings.userStats.crew.Party.FindAll(c => c.status == PlayerStatus.poor))
                    {
                        member.status = PlayerStatus.veryPoor;
                        healthNeedsReduced = false;
                        break;
                    }
                }
                if (healthNeedsReduced)
                {
                    foreach (PartyMember member in MainWindow.settings.userStats.crew.Party.FindAll(c => c.status == PlayerStatus.veryPoor))
                    {
                        member.status = PlayerStatus.dead;
                        deadPlayers.Add(member.name);
                        healthNeedsReduced = false;
                        c = loopCount;
                        break;
                    }
                }
            }
            if (deadPlayers.Count > 0)
            {
                
                string whoDead = String.Empty;
                for (int c = 0; c < deadPlayers.Count; c++)
                {
                    whoDead += deadPlayers[c] + " ";
                    if (!(c == deadPlayers.Count-1))
                        whoDead += "and ";
                }
                DisplayNewPopUp(whoDead + (deadPlayers.Count > 1 ? "have" : "has") + " died.");
            }
            int deadPlayersCount = 0;
            foreach (PartyMember member in MainWindow.settings.userStats.crew.Party.FindAll(c => c.status == PlayerStatus.dead))
            {
                deadPlayersCount++;
            }
            // Kill the user if everyone is dead
            if (deadPlayersCount == MainWindow.settings.userStats.crew.Party.Count)
                MainWindow.settings.currentScreen = "tombstone";
        }
    }
}