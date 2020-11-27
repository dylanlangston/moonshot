using Raylib_cs;
using static Raylib_cs.Raylib;
using static Raylib_cs.Color;
using System;


namespace moonshot.Screens
{
    class originalTopTenSettings : screen
    {
        public override string Name {
            get { return "Original Top Ten Settings"; }
        }
        public override void Display()
        {
            ClearBackground(Colors.space);
            starscape();
            DisplayTopTen();
        }
        private static string selection = String.Empty;
        private static void DisplayTopTen() {

            Raylib.DrawText("Original Top Ten List", (Raylib.GetScreenWidth()-("Original Top Ten List".Length*17))/2, 45, 30, WHITE);

            Raylib.DrawText("Name", 100, 110, 30, WHITE);
            Raylib.DrawText("Points", 390, 110, 30, WHITE);
            Raylib.DrawText("Rating", 600, 110, 30, WHITE);

            Raylib.DrawText("Commander", 575, 160, 30, WHITE);
            Raylib.DrawText("Command Pilot", 575, 190, 30, WHITE);
            Raylib.DrawText("Command Pilot", 575, 220, 30, WHITE);
            Raylib.DrawText("Pilot", 575, 250, 30, WHITE);
            Raylib.DrawText("Pilot", 575, 280, 30, WHITE);
            Raylib.DrawText("Pilot", 575, 310, 30, WHITE);
            Raylib.DrawText("Pilot", 575, 340, 30, WHITE);
            Raylib.DrawText("Pilot", 575, 370, 30, WHITE);
            Raylib.DrawText("Pilot", 575, 400, 30, WHITE);
            Raylib.DrawText("Pilot", 575, 430, 30, WHITE);

            TopTen originalTopTen = new TopTen();
            for(int i = 0;i < originalTopTen.Leaders.Count;i++)
            {
                Raylib.DrawText(originalTopTen.Leaders[i].Item1, 30, 160 + (i * 30), 30, WHITE);
                Raylib.DrawText(originalTopTen.Leaders[i].Item2.ToString(), 400, 160 + (i * 30), 30, WHITE);
            }
            if (PressSPACEBAR()) {
                MainWindow.settings.currentScreen = "Settings";
            }
        }
    }
}
