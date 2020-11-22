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
    public class Location
    {
        public Location()
        {
            _location = String.Empty;
        }
        private string _location;
        public Location(string name)
        {
            _location = name;
        }
        public override string ToString()
        {
            return _location;
        }
    }
    public class Locations
    {
        private List<Location> _locations = new List<Location>() { 
            new Location("Landing Site"), 
            new Location("Mare Tranquillitatis"),
            new Location("MTP (collapsed pit of Mare Tranquillitatis)"),
            new Location("Mare Serenitatis"),
            new Location("Posidonius"),
            new Location("Montes Taurus"),
            new Location("Atlas & Hercules"),
            new Location("Mare Frigoris"),
            new Location("Anaxagoras"),
            new Location("Peary")
        };
        public Locations()
        {
            List<Location> locations = _locations;
        }
    }
    // The Locations and Distances between them.
    partial class mainTrail : screen
    {
        public static List<(Location, Location, int)> LocationsAndDistances = new  List<(Location, Location, int)>() { 
            (new Location("Landing Site"), new Location("Mare Tranquillitatis"), 209),
            (new Location("Mare Tranquillitatis"), new Location("MTP (collapsed pit of Mare Tranquillitatis)"), 34),
            (new Location("MTP (collapsed pit of Mare Tranquillitatis)"), new Location("Mare Serenitatis"), 459),
            (new Location("Mare Serenitatis"), new Location("Posidonius"), 215),
            (new Location("Posidonius"), new Location("Montes Taurus"), 193),
            (new Location("Montes Taurus"), new Location("Atlas & Hercules"), 346),
            (new Location("Atlas & Hercules"), new Location("Mare Frigoris"), 470),
            (new Location("Mare Frigoris"), new Location("Anaxagoras"), 339),
            (new Location("Anaxagoras"), new Location("Peary"), 294)
        };
        public static int GetMilesTraveled(int currentLocation)
        {
            if (currentLocation > LocationsAndDistances.Count)
                currentLocation = LocationsAndDistances.Count;
            int milesTraveled = 0;
            for (int i = 0; i < currentLocation; i++)
            {
                milesTraveled += LocationsAndDistances[i].Item3;
            }
            return milesTraveled;
        }

        private static int travelCounter = 0;
        public static void Travel(int paceModifier = 1)
        {
            travelCounter++;
            if (travelCounter > 100)
            {
                travelCounter = 0;
                Random r = new Random();
                MainWindow.settings.userStats.milesTraveled += r.Next(8,17)+paceModifier;
                MainWindow.settings.userStats.currentTime = MainWindow.settings.userStats.currentTime.AddHours(6);


                if (MainWindow.settings.userStats.inventory.Items.Find(s => s.id == 102).value > 0)
                    MainWindow.settings.userStats.inventory.Items.Find(f => f.id == 102).value -= r.Next(0,3)+paceModifier;
                else
                    MainWindow.settings.currentScreen = "tombstone";
                
            }
            int currentLocation = MainWindow.settings.userStats.currentLocation;
            int milesTraveled = MainWindow.settings.userStats.milesTraveled;
            int NextLandmarkDistance = ((int)(LocationsAndDistances[currentLocation].Item3)) - milesTraveled;
            if (NextLandmarkDistance < 1)
            {
                StartAnimation = false;
                travelCounter = 0;
                MainWindow.settings.userStats.milesTraveled = 0;
                MainWindow.settings.userStats.currentLocation++;
            }
        }
    }
}