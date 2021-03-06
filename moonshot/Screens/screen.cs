﻿using Raylib_cs;
using static Raylib_cs.Raylib;
using static Raylib_cs.Color;
using System;
using System.Numerics;
using System.Collections.Generic;
using System.IO;

namespace moonshot.Screens
{
    abstract class screen
    {
        public screen()
        {
        }
        public abstract string Name {
            get;
        }
        public abstract void Display();
        public void RunOnExit() {}

        // Press SPACEBAR to continue text and logic
        internal static bool PressSPACEBAR() {
            Raylib.DrawText("Press SPACE BAR to continue", Raylib.GetScreenWidth()/5, Raylib.GetScreenHeight()-35, 30, WHITE);
            if (Raylib.IsKeyReleased(KeyboardKey.KEY_SPACE)) {
                return true;
            }
            return false;
        }
        // Logo
        internal static Texture2D logoTexture = new Texture2D();
        internal static void MoonshotLogo()
        {
            if (logoTexture.height == 0) {
                Image logo = LoadImage(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Images/moonshotlogo.png"));
                logoTexture = LoadTextureFromImage(logo);
                UnloadImage(logo);
            }
            DrawTexture(logoTexture, 0, 0, WHITE);
        }

        // Menuline
        internal static Texture2D menulineTexture = new Texture2D();
        internal static void Menuline(int x, int y)
        {
            if (menulineTexture.height == 0) {
                Image menuline = LoadImage(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Images/menuline.png"));
                menulineTexture = LoadTextureFromImage(menuline);
                UnloadImage(menuline);
            }
            DrawTextureEx(menulineTexture, new Vector2(x, y), 0f, 0.55f, WHITE);
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
}
