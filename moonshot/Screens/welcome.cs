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
            switch (rnd.Next(0,6))
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
                    starColor = DARKGRAY;
                    break;
                case 5:
                    starColor = MAROON;
                    break;
                default:
                    starColor = WHITE;
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
        }
        internal static void starscape()
        {
            foreach (StarGradient star in stars) {
                Raylib.DrawCircleGradient(star.x, star.y, star.radius, star.color, Colors.space);
            }
        }
        internal static void MoonshotLogo()
        {
            Image logo = LoadImage("Images/moonshotlogo.png");
            Texture2D logoAsTexture = LoadTextureFromImage(logo);
            UnloadImage(logo);
            DrawTexture(logoAsTexture, 0, 0, WHITE);
        }
        internal static StarGradient[] stars = StarGradientLoop();
        private static StarGradient[] StarGradientLoop(int howManyTimes = 150) {
            StarGradient[] stars = new StarGradient[howManyTimes];
            int count = 0;
            while (count < howManyTimes) {
                Random random = new Random();
                int x = random.Next(0,Raylib.GetScreenWidth());
                int y = random.Next(0,Raylib.GetScreenHeight());
                int radius = random.Next(0,12);
                stars[count] = new StarGradient(x, y, radius);
                count++;
            }
            return stars;
        }
    }
}
