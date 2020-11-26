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
                rationsModifier = 4;
                break;
                default:
                break;
            }

            return new Tuple<int, int>(paceModifier, rationsModifier);
        }

        public static void UseFood(int amount)
        {
            
                foodCounter = 0;
                if (MainWindow.settings.userStats.inventory.Items.Find(s => s.id == 103).value > 0)
                {
                    bool healthBeingIncreased = false;
                    int loopCount = 0;
                    switch (MainWindow.settings.userStats.rations)
                    {
                        case (PlayerRations.filling):
                            loopCount = 2;
                            break;
                        case (PlayerRations.meager):
                            loopCount = 1;
                            break;
                    }
                    for (int c = 0; c < loopCount; c++)
                    {
                    if (!healthBeingIncreased)
                    {
                        foreach (PartyMember member in MainWindow.settings.userStats.crew.Party.FindAll(c => c.status == PlayerStatus.veryPoor))
                        {
                            member.status = PlayerStatus.poor;
                            healthBeingIncreased = true;
                            break;
                        }
                    }
                    if (!healthBeingIncreased)
                    {
                        foreach (PartyMember member in MainWindow.settings.userStats.crew.Party.FindAll(c => c.status == PlayerStatus.poor))
                        {
                            member.status = PlayerStatus.fair;
                            healthBeingIncreased = true;
                            break;
                        }
                    }
                    if (!healthBeingIncreased)
                    {
                        foreach (PartyMember member in MainWindow.settings.userStats.crew.Party.FindAll(c => c.status == PlayerStatus.fair))
                        {
                            member.status = PlayerStatus.good;
                            healthBeingIncreased = true;
                            break;
                        }
                    }
                    }
                    MainWindow.settings.userStats.inventory.Items.Find(s => s.id == 103).value -= (amount + (healthBeingIncreased ? 1 : 0));
                }
                else
                {
                    ReduceHealth();
                }
        }
    }
}