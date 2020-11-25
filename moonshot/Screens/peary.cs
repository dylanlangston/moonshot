using Raylib_cs;
using static Raylib_cs.Raylib;
using static Raylib_cs.Color;
using System;
using System.Numerics;
using System.Windows.Input;
using System.IO;

namespace moonshot.Screens
{
    class peary : screen
    {
        public override string Name {
            get { return "Peary"; }
        }
        private static int loopCount = 0;
        private static bool enterPressed = false;
        public override void Display()
        {
            ClearBackground(Colors.space);
            starscape();
            img();
            Message();
            if (enterPressed)
                LargePopUp("Congratulations! You have\ncompleted your journey! Let's see\nhow many points you received.");
            if (loopCount > 5 && !enterPressed) {
                Confirmation();
            } else {
                loopCount++;
            }
        }
        private static void Message() {
            DrawRectangle(Raylib.GetScreenWidth()/8, Raylib.GetScreenHeight()/5*4, Raylib.GetScreenWidth()/8*6, 80, WHITE);
            Raylib.DrawText("Peary", (Raylib.GetScreenWidth()-("Peary".Length * 15))/2, Raylib.GetScreenHeight()/5*4+10, 30, BLACK);
            string currentDate = MainWindow.settings.userStats.currentTime.ToString("MMMM dd, yyyy");
            Raylib.DrawText(currentDate, (Raylib.GetScreenWidth()-(currentDate.Length*15))/2, Raylib.GetScreenHeight()/5*4+40, 30, BLACK);
        }
        private static void LargePopUp(string message = "")
        {
            string[] messageArray = message.Split("\n");
            Raylib.DrawRectangleRounded(new Rectangle(Raylib.GetScreenWidth()/8, Raylib.GetScreenHeight()/3, Raylib.GetScreenWidth()/8*6, 40+(messageArray.Length*30)), 0.25f, 10, WHITE);
            Raylib.DrawRectangle(Raylib.GetScreenWidth()/8+10, Raylib.GetScreenHeight()/3+10, Raylib.GetScreenWidth()/8*6-20, 20+(messageArray.Length*30), BLACK);
            for (int i = 0;i < messageArray.Length;i++)
            {
                Raylib.DrawText(messageArray[i], Raylib.GetScreenWidth()/8+50, Raylib.GetScreenHeight()/3+20+(30*i), 30, WHITE);
            }
            if (PressSPACEBAR()) {
                loopCount = 0;
                enterPressed = false;
                MainWindow.settings.currentScreen = "welcome";
            }
        }
        private static void Confirmation(){
            if (PressSPACEBAR()) {
                enterPressed = true;
                Raylib.StopSound(sound);
            }
        }

        // space center
        internal static Sound sound = LoadSound("Music/POL-sky-sanctuary-short.wav");
        internal static Texture2D imgTexture = new Texture2D();
        internal static void img()
        {
            Raylib.SetMasterVolume(1f);
            if (imgTexture.height == 0) {
                Image img = LoadImage(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Images/peary.png"));
                imgTexture = LoadTextureFromImage(img);
                UnloadImage(img);
            }
            DrawTextureEx(imgTexture, new Vector2(0, 0), 0f, 1f, WHITE);
            if (!Raylib.IsSoundPlaying(sound) && !enterPressed) 
                Raylib.PlaySound(sound);
        }
    }
}
