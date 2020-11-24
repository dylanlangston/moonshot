using Raylib_cs;
using static Raylib_cs.Raylib;
using static Raylib_cs.Color;
using System;
using System.Numerics;
using System.Windows.Input;
using System.IO;

namespace moonshot.Screens
{
    class MTP : screen
    {
        public override string Name {
            get { return "MTP"; }
        }
        private static int loopCount = 0;
        public override void Display()
        {
            ClearBackground(Colors.space);
            starscape();
            mtp();
            Message();
            if (loopCount > 5) {
                Confirmation();
            } else {
                loopCount++;
            }
        }
        private static void Message() {
            DrawRectangle(Raylib.GetScreenWidth()/8, Raylib.GetScreenHeight()/5*4, Raylib.GetScreenWidth()/8*6, 80, WHITE);
            Raylib.DrawText("MTP", Raylib.GetScreenWidth()/2-45, Raylib.GetScreenHeight()/5*4+10, 30, BLACK);
            string currentDate = MainWindow.settings.userStats.currentTime.ToString("MMMM dd, yyyy");
            Raylib.DrawText(currentDate, (Raylib.GetScreenWidth()-(currentDate.Length*15))/2, Raylib.GetScreenHeight()/5*4+40, 30, BLACK);
        }
        private static void Confirmation(){
            if (PressSPACEBAR()) {
                mainTrail.popUpMessages[MainWindow.settings.userStats.currentLocation] = ("From MTP it is 459 Miles to Mare\nSerenitatis.", false, String.Empty);
                Raylib.StopSound(sound);
                MainWindow.settings.currentScreen = "Check Stats";
                MainWindow.settings.userStats.currentLocation = 2;
                loopCount = 0;
            }
        }

        // space center
        internal static Sound sound = LoadSound("Music/POL-find-the-exit-short.wav");
        internal static Texture2D mtpTexture = new Texture2D();
        internal static void mtp()
        {
            Raylib.SetMasterVolume(1f);
            if (mtpTexture.height == 0) {
                Image img = LoadImage(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Images/mtp.png"));
                mtpTexture = LoadTextureFromImage(img);
                UnloadImage(img);
            }
            DrawTextureEx(mtpTexture, new Vector2(0, 0), 0f, 1f, WHITE);
            if (!Raylib.IsSoundPlaying(sound)) 
                Raylib.PlaySound(sound);
        }
    }
}
