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
    partial class collectRocksControls : collectRocksMiniGameScreen
    {
        public override string Name {
            get { return "Controls"; }
        }
        private static int loopCount = 0;
        public override void Display()
        {
            ClearBackground(Colors.space);
            screen.starscape();

            Raylib.DrawText("Rock Collecting Instructions", 170, 30, 30, WHITE);

            Raylib.DrawText("Enter Key", 30, 100, 30, WHITE);
            Raylib.DrawText("To start or stop walking", 350, 100, 30, WHITE);

            // Arrow Keys
            Raylib.DrawRectangleRounded(new Rectangle(85, 170, 40, 40), 0.25f, 10, WHITE);
            Raylib.DrawRectangleRounded(new Rectangle(40, 215, 40, 40), 0.25f, 10, WHITE);
            Raylib.DrawRectangleRounded(new Rectangle(85, 215, 40, 40), 0.25f, 10, WHITE);
            Raylib.DrawRectangleRounded(new Rectangle(130, 215, 40, 40), 0.25f, 10, WHITE);
            Raylib.DrawTriangle(new Vector2(105, 180), new Vector2(95, 200), new Vector2(115, 200), BLACK);
            Raylib.DrawTriangle(new Vector2(95, 225), new Vector2(105, 245), new Vector2(115, 225), BLACK);
            Raylib.DrawTriangle(new Vector2(50, 235), new Vector2(70, 245), new Vector2(70, 225), BLACK);
            Raylib.DrawTriangle(new Vector2(140, 225), new Vector2(140, 245), new Vector2(160, 235), BLACK);
            Raylib.DrawText("To change direction", 350, 180, 30, WHITE);
            Raylib.DrawText("(novice collectors)", 350, 210, 30, WHITE);

            //WASD
            Raylib.DrawRectangleRounded(new Rectangle(70, 300, 40, 40), 0.25f, 10, WHITE);
            Raylib.DrawRectangleRounded(new Rectangle(40, 345, 40, 40), 0.25f, 10, WHITE);
            Raylib.DrawRectangleRounded(new Rectangle(85, 345, 40, 40), 0.25f, 10, WHITE);
            Raylib.DrawRectangleRounded(new Rectangle(130, 345, 40, 40), 0.25f, 10, WHITE);
            Raylib.DrawText("W", 80, 305, 30, BLACK);
            Raylib.DrawText("A", 50, 350, 30, BLACK);
            Raylib.DrawText("S", 95, 350, 30, BLACK);
            Raylib.DrawText("D", 140, 350, 30, BLACK);
            Raylib.DrawText("To change direction", 350, 310, 30, WHITE);
            Raylib.DrawText("(expert collectors)", 350, 340, 30, WHITE);


            Raylib.DrawText("Space Bar", 30, 450, 30, WHITE);
            Raylib.DrawText("To pick up rock", 350, 450, 30, WHITE);

            if (loopCount > 5) {
                Confirmation();
            } else {
                loopCount++;
            }
        }
        private static void Confirmation(){
            if (screen.PressSPACEBAR()) {
                collectRocks.CollectRocksState = "Game";
                loopCount = 0;
            }
        }
    }
}
