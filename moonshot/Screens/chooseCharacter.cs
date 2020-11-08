using Raylib_cs;
using static Raylib_cs.Raylib;
using static Raylib_cs.Color;
using System;
using System.Numerics;
using System.Windows.Input;

namespace moonshot.Screens
{
    class chooseCharacter : screen
    {
        public override string Name {
            get { return "Character Selection"; }
        }
        public override void Display()
        {
            ClearBackground(Colors.space);
            starscape();
            Menuline(Raylib.GetScreenWidth() / 128, 0);
            Menuline(Raylib.GetScreenWidth() / 128, Raylib.GetScreenHeight() / 24 * 21 );
            Menu();
        }
        internal static string selection = String.Empty;
        internal static void Menu() {
            Raylib.DrawText("Many kinds of people made the trip to\nthe Moon.", Raylib.GetScreenWidth()/8, Raylib.GetScreenHeight()/6, 30, WHITE);
            Raylib.DrawText("You may:", Raylib.GetScreenWidth()/8, Raylib.GetScreenHeight()/3, 30, WHITE);
            Raylib.DrawText("1.  Banker", Raylib.GetScreenWidth()/7, Raylib.GetScreenHeight()/3 + 50, 30, WHITE);
            Raylib.DrawText("2. carpenter", Raylib.GetScreenWidth()/7, Raylib.GetScreenHeight()/3 + 90, 30, WHITE);
            Raylib.DrawText("3. farmer", Raylib.GetScreenWidth()/7, Raylib.GetScreenHeight()/3 + 130, 30, WHITE);
            Raylib.DrawText("4. Find out the differences between\nthese choices", Raylib.GetScreenWidth()/7, Raylib.GetScreenHeight()/3 + 170, 30, WHITE);
            int keypress = Raylib.GetKeyPressed();
            if (Raylib.IsKeyPressed(KeyboardKey.KEY_BACKSPACE)) {
                keypress = 9000;
            } else if (Raylib.IsKeyPressed(KeyboardKey.KEY_ENTER)) {
                switch (selection) {
                    case "5":
                        MainWindow.Running = false;
                        break;
                    default:
                        break;
                }
            }
            switch (keypress){
                case 49:
                    selection = "1";
                    break;
                case 50:
                    selection = "2";
                    break;
                case 51:
                    selection = "3";
                    break;
                case 52:
                    selection = "4";
                    break;
                case 9000:
                    selection = String.Empty;
                    break;
                default:
                    break;
            }
            Raylib.DrawText("What is your choice? " + selection + "_", Raylib.GetScreenWidth()/8, Raylib.GetScreenHeight()/5*4, 30, WHITE);
        }
        // Logo
        internal static Texture2D logoTexture = new Texture2D();
        internal static void MoonshotLogo()
        {
            if (logoTexture.height == 0) {
                Image logo = LoadImage("Images/moonshotlogo.png");
                logoTexture = LoadTextureFromImage(logo);
                UnloadImage(logo);
            }
            DrawTexture(logoTexture, 0, 0, WHITE);
        }
        // Moon
        internal static Texture2D moonTexture = new Texture2D();
        internal static void Moon()
        {
            if (moonTexture.height == 0) {
                Image moon = LoadImage("Images/moon.png");
                moonTexture = LoadTextureFromImage(moon);
                UnloadImage(moon);
            }
            DrawTextureEx(moonTexture, new Vector2(Raylib.GetScreenHeight()/4*3, 0), 0f, 0.10f, WHITE);
        }
    }
}
