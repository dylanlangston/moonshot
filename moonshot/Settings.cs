using System.Collections.Generic;
using System.Text;
using Raylib_cs;
using moonshot.Screens;

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

        // All available screens in the game. Setting is intended to store the screens and not be changed durring runtime hence why it is marked as private.
        private List<screen> _screensList = new List<screen>(){ 
            new welcome(),
            new chooseCharacter()
        };
        internal List<screen> screensList {
            get { return _screensList; }
        }

        // Track if game should start fullscreen
        public bool StartFullscreen = true;

        // Track User Stats
        internal UserStats userStats = new UserStats();

        // Init Settings
        public Settings() 
        {
            LoadSettings();
        }

        // Load Settings from save file if it exists
        private void LoadSettings()
        {

        }
        
        // Save Settings
        internal void SaveSettings()
        {

        }
    }
}
