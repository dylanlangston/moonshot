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
    partial class collectRocksGame : collectRocksMiniGameScreen
    {
        public override string Name {
            get { return "Game"; }
        }
        private static collectRocksMiniGame game = new collectRocksMiniGame();
        private static bool playingGame = false;
        public override void Display()
        {
            if (!playingGame)
            {
                playingGame = true;
                game.StartGame();
            }
            else
                game.Continue();
        }

    }
    class collectRocksMiniGame
    {
        private Rocks rocks = null;
        public collectRocksMiniGame()
        {   
        }
        public void StartGame()
        {
            rocks = new Rocks(Raylib.GetScreenWidth()-100, Raylib.GetScreenHeight()-100);
            Continue();
        }
        public void Continue()
        {
            rocks.DisplayRocks();
            Raylib.ClearBackground(GRAY);
        }
    }
    class SpaceGuy
    {
        int x = 0;
        int y = 0;
        int maxWidth = 0;
        int maxHeight = 0;
    }
    class Rocks
    {
        public List<(int index, int x, int y)> rockTypesAndLocations = new List<(int index, int x, int y)>();
        public Rocks(int maxWidth, int maxHeight)
        {
            if (rockTypesAndLocations.Count == 0)
            {
                Random r = new Random();
                int loops = r.Next(0,7);
                for (int i = 0; i < loops; i++)
                {
                    int RandIndex = r.Next(0,5);
                    int RandX = r.Next(1,maxWidth);
                    int RandY = r.Next(1,maxHeight);
                    rockTypesAndLocations.Add((RandIndex, RandX, RandY));
                }
            }
            else {
                foreach (var rock in rockTypesAndLocations)
                {
                    DisplayRock(rock.index, rock.x, rock.y);
                }
            }
        }
        public void DisplayRocks()
        {
            foreach (var rock in rockTypesAndLocations)
            {
                DisplayRock(rock.index, rock.x, rock.y);
            }
        }
        public void DisplayRock(int index, int x, int y)
        {
            Console.WriteLine(index);
            switch (index)
            {
                case 1:
                    rockOneIcon(x ,y);
                    break;
                case 2:
                    rockTwoIcon(x ,y);
                    break;
                case 3:
                    rockThreeIcon(x ,y);
                    break;
                case 4:
                    rockOneIcon(x ,y);
                    break;
                default:
                    rockFiveIcon(x ,y);
                    break;
            }
        }
        private static Texture2D rockOneTexture = new Texture2D();
        private static void rockOneIcon(int x, int y)
        {
            if (rockOneTexture.height == 0) {
                Image img = LoadImage(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Images/rockMiniGame/rock1.png"));
                rockOneTexture = LoadTextureFromImage(img);
                UnloadImage(img);
            }
            DrawTextureEx(rockOneTexture, new Vector2(x, y), 0f, 0.5f, WHITE);
        }
        private static Texture2D rockTwoTexture = new Texture2D();
        private static void rockTwoIcon(int x, int y)
        {
            if (rockTwoTexture.height == 0) {
                Image img = LoadImage(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Images/rockMiniGame/rock2.png"));
                rockTwoTexture = LoadTextureFromImage(img);
                UnloadImage(img);
            }
            DrawTextureEx(rockTwoTexture, new Vector2(x, y), 0f, 0.5f, WHITE);
        }
        private static Texture2D rockThreeTexture = new Texture2D();
        private static void rockThreeIcon(int x, int y)
        {
            if (rockThreeTexture.height == 0) {
                Image img = LoadImage(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Images/rockMiniGame/rock3.png"));
                rockThreeTexture = LoadTextureFromImage(img);
                UnloadImage(img);
            }
            DrawTextureEx(rockThreeTexture, new Vector2(x, y), 0f, 0.5f, WHITE);
        }
        private static Texture2D rockFourTexture = new Texture2D();
        private static void rockFourIcon(int x, int y)
        {
            if (rockFourTexture.height == 0) {
                Image img = LoadImage(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Images/rockMiniGame/rock4.png"));
                rockFourTexture = LoadTextureFromImage(img);
                UnloadImage(img);
            }
            DrawTextureEx(rockFourTexture, new Vector2(x, y), 0f, 0.5f, WHITE);
        }
        private static Texture2D rockFiveTexture = new Texture2D();
        private static void rockFiveIcon(int x, int y)
        {
            if (rockFiveTexture.height == 0) {
                Image img = LoadImage(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Images/rockMiniGame/rock5.png"));
                rockFiveTexture = LoadTextureFromImage(img);
                UnloadImage(img);
            }
            DrawTextureEx(rockFiveTexture, new Vector2(x, y), 0f, 0.5f, WHITE);
        }
    }
}
