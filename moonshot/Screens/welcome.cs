using Raylib_cs;
using static Raylib_cs.Raylib;
using static Raylib_cs.Color;
using System;

namespace moonshot.Screens
{
    internal class StarGradient
    {
        public StarGradient(int x, int y, float radius)
        {
            this._x = x;
            this._y = y;
            this._radius = radius;
            this._color = StarTwinkle();
        }
        private int _x { get; set;}
        public int x { get { return _x; } }
        private int _y { get; set; }
        public int y { get { return _y; } }
        private float _radius { get; set; }
        public float radius { get { return _radius; } }
        private Color _color { get; set; }
        public Color color { get { return _color; } }
        private Color StarTwinkle()
        {
            Color starColor = new Color();
            Random rnd = new Random();
            switch (rnd.Next(0,9))
            {
                case 1:
                    starColor = WHITE;
                    break;
                case 2: 
                    starColor = BLUE;
                    break;
                case 3:
                    starColor = YELLOW;
                    break;
                case 4:
                    starColor = MAROON;
                    break;
                default:
                    starColor = DARKGRAY;
                    break;
            }
            return starColor;
        }
    }
    class welcome : screen
    {
        public override void Display()
        {
            ClearBackground(Colors.space);
            starscape();
            MoonshotLogo();
            Menu();
        }
        internal static void Menu() {
            Raylib.DrawText("You may:", Raylib.GetScreenWidth()/6, Raylib.GetScreenHeight()/3, 30, WHITE);
            Raylib.DrawText("1.  Blast off!", Raylib.GetScreenWidth()/5, Raylib.GetScreenHeight()/3 + 50, 30, WHITE);
            Raylib.DrawText("2. Learn about the Apollo Astronauts", Raylib.GetScreenWidth()/5, Raylib.GetScreenHeight()/3 + 90, 30, WHITE);
            Raylib.DrawText("3. See the Moon top ten", Raylib.GetScreenWidth()/5, Raylib.GetScreenHeight()/3 + 130, 30, WHITE);
            Raylib.DrawText("4. Settings", Raylib.GetScreenWidth()/5, Raylib.GetScreenHeight()/3 + 170, 30, WHITE);
            Raylib.DrawText("5. Quit", Raylib.GetScreenWidth()/5, Raylib.GetScreenHeight()/3 + 210, 30, WHITE);
            Raylib.DrawText("What is your choice? _", Raylib.GetScreenWidth()/6, Raylib.GetScreenHeight()/5*4, 30, WHITE);
        }
        // Logo
        internal static void MoonshotLogo()
        {
            Image logo = LoadImage("Images/moonshotlogo.png");
            Texture2D logoAsTexture = LoadTextureFromImage(logo);
            UnloadImage(logo);
            DrawTexture(logoAsTexture, 0, 0, WHITE);
        }
        // Draw Star background
        internal static void starscape()
        {
            foreach (StarGradient star in stars) {
                Raylib.DrawCircleGradient(star.x, star.y, star.radius, star.color, Colors.space);
            }
        }
        internal static StarGradient[] stars = StarGradientLoop();
        private static StarGradient[] StarGradientLoop(int howManyTimes = 125) {
            StarGradient[] stars = new StarGradient[howManyTimes];
            int count = 0;
            while (count < howManyTimes) {
                Random random = new Random();
                int x = random.Next(0,Raylib.GetScreenWidth());
                int y = random.Next(0,Raylib.GetScreenHeight());
                int radius = random.Next(2,12);
                stars[count] = new StarGradient(x, y, radius);
                count++;
            }
            return stars;
        }
    }
}
