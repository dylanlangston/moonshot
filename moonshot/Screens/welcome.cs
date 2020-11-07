using Raylib_cs;
using static Raylib_cs.Raylib;
using static Raylib_cs.Color;
using System;

namespace moonshot.Screens
{
    class welcome : screen
    {
        public override void Display()
        {
            starscape();
            DrawText("Welcome to Moonshot!", 220, 200, 40, MAROON);
        }
        private static void starscape()
        {
            int Width = GetScreenWidth();
            int Height = GetScreenHeight();

            for (int w = 0; w < Width; w=NumberGenerator(w, 50, 200))
            {
                for (int h = 0; h < Height; h=NumberGenerator(h, 10, 100))
                {
                    Random random = new Random(w+h);
                    Raylib.DrawCircle(h, w, random.Next(1,10), WHITE);
                }
            }
            for (int w = 0; w < Width; w = NumberGenerator(w, 10, 100))
            {
                for (int h = 0; h < Height; h = NumberGenerator(h, 50, 200))
                {
                    Random random = new Random(w + h);
                    Raylib.DrawCircle(h, w, random.Next(1, 5), BLUE);
                }
            }
            for (int w = 0; w < Width; w = NumberGenerator(w, 100, 150))
            {
                for (int h = 0; h < Height; h = NumberGenerator(h, 100, 150))
                {
                    Random random = new Random(w + h);
                    Raylib.DrawCircle(h, w, random.Next(5, 10), YELLOW);
                }
            }
        }

        private static int NumberGenerator(int inputNumber, int minIncrease, int maxIncrease)
        {
            Random random = new Random(inputNumber);
            int rn = random.Next(((int)inputNumber + minIncrease), ((int)inputNumber + maxIncrease));
            Console.WriteLine(inputNumber + " " + rn);
            return rn;
        }
    }
}
