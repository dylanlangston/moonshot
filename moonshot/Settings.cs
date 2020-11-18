using System.Collections.Generic;
using System.Text;
using Raylib_cs;
using moonshot.Screens;
using System.Xml;
using System.IO;
using System;

namespace moonshot
{
    // Save and Load Moonshot settings
    // It is also where the game tracks and stores it's own status.
    class Settings
    {
        // Track if the game should be running. Set to false to exit.
        internal bool Running = true;

        // Track the current session. Default to the welcome screen.
        internal string currentScreen = "welcome";
        
        // Track if settings have been saved.
        internal bool savedProgress = false;

        // All available screens in the game. Setting is intended to store the screens and not be changed durring runtime hence why it is marked as private.
        private List<screen> _screensList = new List<screen>(){ 
            new welcome(),
            new settingsScreen(),
            new saveScreen(),
            new loadScreen(),
            new chooseCharacter(),
            new aboutCharacters(),
            new enterName(),
            new suppliesScreenOne(),
            new suppliesScreenTwo(),
            new suppliesScreenThree(),
            new suppliesScreenFour(),
            new mattsStore(),
            new mattsStoreOxygenTanks(),
            new mattsStoreFuel(),
            new mattsStoreFood(),
            new mattsStoreBoxes(),
            new mattsStoreShipParts(),
            new LeavingMattsStore(),
            new leavingCapeKennedy(),
            new CheckStats(),
            new ContinueOnTrail(),
            new CheckSupplies(),
            new LookAtMap(),
            new ChangePace(),
            new ChangeFoodRation(),
            new TalkToPeople(),
            new BuySupplies(),
            new mainTrail()
        };
        internal List<screen> screensList {
            get { return _screensList; }
        }

        private List<Tuple<string, int>> _Locations = new List<Tuple<string, int>>() {
            new Tuple<string, int>("Cape Kennedy, Florida", 0),
            new Tuple<string, int>("Landing Site", 1),
            new Tuple<string, int>("Mare Tranquillitatis", 2),
            new Tuple<string, int>("Collapsed Pit of Mare Tranquillitatis", 3),
            new Tuple<string, int>("Mare Serenitatis", 4),
            new Tuple<string, int>("Posidonius", 5),
            new Tuple<string, int>("Sculptured Hills", 6),
            new Tuple<string, int>("Atlas & Hercules", 7),
            new Tuple<string, int>("Mare Frigoris", 8),
            new Tuple<string, int>("Anaxagoras", 9),
            new Tuple<string, int>("Peary", 10)
        };
        internal List<Tuple<string, int>> Locations
        {
            get { return _Locations; }
        }
        

        // Screens to load even if current game is in progress.
        internal List<string> nonGameScreens = new List<string>() {"load", "save", "settings", "welcome"};

        // Track if game should start fullscreen
        public bool StartFullscreen = true;

        // Track User Stats
        internal UserStats userStats = new UserStats();

        // Init Settings
        public Settings(bool useDefaults = false) 
        {
            // Load settings on startup
            if (!useDefaults) {LoadSettings();}
        }

        // Load Settings from save file if it exists
        // Not intended to be used except when the program is started.
        private void LoadSettings()
        {
            // Load last used full screen setting.
            try {
                StartFullscreen = bool.Parse(GetConfigValue("StartFullscreen"));
            } catch {}

            // Load the current screen (progress)
            string cScreenRaw = GetConfigValue("CurrentScreen");
            if (string.IsNullOrEmpty(cScreenRaw)) {
                currentScreen = "welcome"; // default 
            } else {
                currentScreen = cScreenRaw;
                savedProgress = true; // Loaded Saved Progress
            }

            // Load Userstats
            string ustat = GetConfigValue("UserStats");
            userStats.LoadUserStatsFromString(ustat, userStats);

        }
        
        // Save Settings
        // Only meant to be used on exit.
        internal void SaveSettings()
        {
            // Save last used full screen setting.
            SetConfigValue("StartFullscreen", StartFullscreen.ToString());
            
            // Save the current screen (progress)
            SetConfigValue("CurrentScreen", currentScreen.ToLower());

            // Save the Userstats
            SetConfigValue("UserStats", userStats.ToString());
        }

        // Reads the moonshot.xml file for the supplied tag value.
        private static string GetConfigValue(string configTag)
        {
            // Create moonshot folder in appdata if missing. 
            string AppDataFolder = Program.AppDataFolder();
            if (!Directory.Exists(AppDataFolder))
            {
                Directory.CreateDirectory(AppDataFolder);
            }

            // Specify config file.
            string configFile = Path.Combine(AppDataFolder, @"moonshot.xml");

            // Read config file.
            XmlDocument doc = new XmlDocument();
            if (!File.Exists(configFile))
            {
                return "";
            }
            doc.Load(configFile);
            foreach (XmlNode node in doc)
            {
                for (int i = 0; i < node.ChildNodes.Count; i++)
                {
                    if (node.ChildNodes[i].Name.ToLower() == configTag.ToLower())
                    {
                        return node.ChildNodes[i].InnerText;
                    }
                }
            }
            return "";
        }

        // Write to moonshot.xml file for the supplied tag value.
        private static bool SetConfigValue(string configTag, string configValue)
        {
        // Create moonshot folder in appdata if missing. 
            string AppDataFolder = Program.AppDataFolder();
            if (!Directory.Exists(AppDataFolder))
            {
                Directory.CreateDirectory(AppDataFolder);
            }

            // Specify config file.
            string configFile = Path.Combine(AppDataFolder, @"moonshot.xml");

            // Write to XML file
            XmlDocument doc = new XmlDocument();
            if (File.Exists(configFile))
            {
                doc.Load(configFile);
                foreach (XmlNode node in doc)
                {
                    for (int i = 0; i < node.ChildNodes.Count; i++)
                    {
                        if (node.ChildNodes[i].Name.ToLower() == configTag.ToLower())
                        {
                            node.ChildNodes[i].InnerText = configValue;
                            doc.Save(configFile);
                            return true;
                        }
                    }
                }
            }
            XmlElement element = doc.CreateElement(configTag);
            element.InnerText = configValue;
            XmlNode startTag = doc.GetElementsByTagName("Config")[0];
            if (startTag == null)
            {
                XmlElement newstartTag = doc.CreateElement("Config");
                newstartTag.AppendChild(element);
                doc.AppendChild(newstartTag);
            } else
            {
                startTag.AppendChild(element);
            }
            doc.Save(configFile);
            return true;
        }
    }
}
