using System.Reflection.Metadata.Ecma335;
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
        private static int coolDown = 50;
        internal static void CalamityChance()
        {
            if (coolDown < 1) {
                coolDown = 50;
                int Rand= r.Next(0,100);
                switch (Rand)
                {
                    case 10:
                        MeteorShower();
                        break;
                    case 20:
                        MeteorShower();
                        break;
                    case 30:
                        EngineOverheated();
                        break;
                    case 40:
                        ShipMalfunction();
                        break;
                    case 50:
                        EnvironmentalMalfunction();
                        break;
                    case 60:
                        RadiationExposure();
                        break;
                    case 70:
                        GetSpareParts();
                        break;
                    case 80:
                        GravityAssist();
                        break;
                    default:
                        break;
                }
            }
            else
            {
                coolDown--;
            }
        }
        
        private static void MeteorShower()
        {
            DisplayNewPopUp("Warning: Meteor Shower!", false, "Avoid Meteor Game");
        }

        private static void EngineOverheated()
        {
            DisplayNewPopUp("Warning: Engine Overheated!\nSlowed down to let engine cool.\nLost 1 day.", false, "Engine Overheat Calamity");
        }

        private static void ShipMalfunction()
        {
            DisplayNewPopUp("Warning: Ship engine malfunction!", false, "Ship Malfunction Calamity");
        }

        private static void EnvironmentalMalfunction()
        {
            if (MainWindow.settings.userStats.inventory.Items.Find(s => s.id == 101).value > 0)
                DisplayNewPopUp("Warning: Environmental malfunction!\nLost 1 oxygen tank.", false, "Environmental Malfunction Calamity");
        }
        private static void RadiationExposure()
        {
            DisplayNewPopUp("Warning: Radiation Exposure!", false, "Radiation Exposure Calamity");
        }

        private static void GetSpareParts()
        {
            DisplayNewPopUp("Salvadged ship wreckage, found\nspare ship part.", false, "Get Spare Parts Calamity");
        }

        private static void GravityAssist()
        {
            DisplayNewPopUp("Nice Piloting! Used Gravity\nAssist to gain 30 miles.", false, "Gravity Assist Calamity");
        }
    }

    class EngineOverheatCalamity : screen
    {
        public override string Name {
            get { return "Engine Overheat Calamity"; }
        }
        public override void Display()
        {
            MainWindow.settings.userStats.currentTime = MainWindow.settings.userStats.currentTime.AddDays(1);
            mainTrail.UseFood(3);
            mainTrail.CheckHealth();
            mainTrail.StartAnimation = true;
            MainWindow.settings.currentScreen = "Main Trail";
        }
    }
    class ShipMalfunctionCalamity : screen
    {
        public override string Name {
            get { return "Ship Malfunction Calamity"; }
        }
        public override void Display()
        {
            MainWindow.settings.userStats.ShipWorking = false;
            MainWindow.settings.currentScreen = "Main Trail";
        }
    }
    class EnvironmentalMalfunctionCalamity : screen
    {
        public override string Name {
            get { return "Environmental Malfunction Calamity"; }
        }
        public override void Display()
        {
            MainWindow.settings.userStats.inventory.Items.Find(s => s.id == 101).value -= 1;
            mainTrail.StartAnimation = true;
            MainWindow.settings.currentScreen = "Main Trail";
        }
    }
    class RadiationExposureCalamity : screen
    {
        public override string Name {
            get { return "Radiation Exposure Calamity"; }
        }
        private static PartyMember whosHurt = null;
        private static int loopCount = 0;
        public override void Display()
        {
            if (whosHurt == null) {
                bool healthNeedsReduced = true;
                if (healthNeedsReduced)
                {
                    foreach (PartyMember member in MainWindow.settings.userStats.crew.Party.FindAll(c => c.status == PlayerStatus.good))
                    {
                        whosHurt = member;
                        whosHurt.status = PlayerStatus.poor;
                        healthNeedsReduced = false;
                        break;
                    }
                }
                if (healthNeedsReduced)
                {
                    foreach (PartyMember member in MainWindow.settings.userStats.crew.Party.FindAll(c => c.status == PlayerStatus.fair))
                    {
                        whosHurt = member;
                        whosHurt.status = PlayerStatus.veryPoor;
                        healthNeedsReduced = false;
                        break;
                    }
                }
                if (healthNeedsReduced)
                {
                    foreach (PartyMember member in MainWindow.settings.userStats.crew.Party.FindAll(c => c.status == PlayerStatus.poor))
                    {
                        whosHurt = member;
                        whosHurt.status = PlayerStatus.dead;
                        healthNeedsReduced = false;
                        break;
                    }
                }
                if (healthNeedsReduced)
                {
                    foreach (PartyMember member in MainWindow.settings.userStats.crew.Party.FindAll(c => c.status == PlayerStatus.veryPoor))
                    {
                        whosHurt = member;
                        whosHurt.status = PlayerStatus.dead;
                        healthNeedsReduced = false;
                        break;
                    }
                }
            }
            MessageBox(whosHurt.name + " was exposed\nand is now " + (whosHurt.status == PlayerStatus.dead ? whosHurt.status : "is in " + whosHurt.status + " health") + ".");
            if (loopCount > 5) {
                Confirmation();
            } else {
                loopCount++;
            }
        }
        private static void Confirmation(){
            if (PressSPACEBAR()) {
                loopCount = 0;
                whosHurt = null;
                mainTrail.StartAnimation = true;
                MainWindow.settings.currentScreen = "Main Trail";
            }
        }
        private static void MessageBox(string message)
        {
            string[] messageArray = message.Split("\n");
            Raylib.DrawRectangleRounded(new Rectangle(Raylib.GetScreenWidth()/8, Raylib.GetScreenHeight()/3, Raylib.GetScreenWidth()/8*6, 40+(messageArray.Length*30)), 0.25f, 10, WHITE);
            Raylib.DrawRectangle(Raylib.GetScreenWidth()/8+10, Raylib.GetScreenHeight()/3+10, Raylib.GetScreenWidth()/8*6-20, 20+(messageArray.Length*30), BLACK);
            for (int i = 0;i < messageArray.Length;i++)
            {
                Raylib.DrawText(messageArray[i], Raylib.GetScreenWidth()/8+50, Raylib.GetScreenHeight()/3+20+(30*i), 30, WHITE);
            }
        }
    }
    class GetSparePartsCalamity : screen
    {
        public override string Name {
            get { return "Get Spare Parts Calamity"; }
        }
        public override void Display()
        {
            MainWindow.settings.userStats.inventory.Items.Find(s => s.id == 105).value += 1;
            mainTrail.StartAnimation = true;
            MainWindow.settings.currentScreen = "Main Trail";
        }
    }
    class GravityAssist : screen
    {
        public override string Name {
            get { return "Gravity Assist Calamity"; }
        }
        public override void Display()
        {
            MainWindow.settings.userStats.milesTraveled += 30;
            mainTrail.StartAnimation = true;
            MainWindow.settings.currentScreen = "Main Trail";
        }
    }
}