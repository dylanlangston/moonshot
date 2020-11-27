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
        private Astronaut astronaut = null;
        public collectRocksMiniGame()
        {   
        }
        public void StartGame()
        {
            rocks = new Rocks(Raylib.GetScreenWidth()-100, Raylib.GetScreenHeight()-100);
            astronaut = new Astronaut(100, 100, Raylib.GetScreenWidth(), Raylib.GetScreenHeight());
            Continue();
        }
        public void Continue()
        {
            Raylib.ClearBackground(GRAY);
            rocks.DisplayRocks();
            astronaut.DisplayAstronaut();
        }
    }
    class Astronaut
    {
        int x = 0;
        int y = 0;
        int maxWidth = 0;
        int maxHeight = 0;
        int currentFrame = 1;
        int currentDirection = 0;
        public Astronaut(int startX, int startY, int MaxWidth, int MaxHeight)
        {
            x = startX;
            y = startY;
            frontAstronautOneIcon(-500, -500);
            maxWidth = MaxWidth - frontAstronautOneTexture.width+100;
            maxHeight = MaxHeight - frontAstronautOneTexture.height;
            currentFrame = 1;
            currentDirection = 0;
            DisplayAstronaut();
        }
        private int realFrameCount = 0;
        private bool Reverser = false;
        public void DisplayAstronaut()
        {
            GetKeyPress();
            if (IsMoving)
            {
                MoveAstronaut();
                realFrameCount++;
                if (realFrameCount > 20)
                {
                    realFrameCount = 0;
                    if (Reverser)
                        currentFrame--;
                    else 
                        currentFrame++;

                    if (currentFrame == 2 || currentFrame == 0)
                        Reverser = !Reverser; 
                }
            }
            else {
                realFrameCount = 0;
                Reverser = false;
                currentFrame = 1;
            }
            switch (currentFrame)
            {
                case 0:
                    switch (currentDirection)
                    {
                        case 0:
                            frontAstronautOneIcon(x, y);
                            break;
                        case 1:
                            lsideAstronautOneIcon(x, y);
                            break;
                        case 2:
                            rsideAstronautOneIcon(x, y);
                            break;
                        case 3:
                            backAstronautOneIcon(x, y);
                            break;
                    }
                    break;
                case 1:
                    switch (currentDirection)
                    {
                        case 0:
                            frontAstronautTwoIcon(x, y);
                            break;
                        case 1:
                            lsideAstronautTwoIcon(x, y);
                            break;
                        case 2:
                            rsideAstronautTwoIcon(x, y);
                            break;
                        case 3:
                            backAstronautTwoIcon(x, y);
                            break;
                    }
                    break;
                case 2:
                    switch (currentDirection)
                    {
                        case 0:
                            frontAstronautThreeIcon(x, y);
                            break;
                        case 1:
                            lsideAstronautThreeIcon(x, y);
                            break;
                        case 2:
                            rsideAstronautThreeIcon(x, y);
                            break;
                        case 3:
                            backAstronautThreeIcon(x, y);
                            break;
                    }
                    break;
            }
        }
        private bool IsMoving = false;
        private void GetKeyPress()
        {

            if (Raylib.IsKeyReleased(KeyboardKey.KEY_ENTER))
                IsMoving = !IsMoving;

            int keypress = Raylib.GetKeyPressed();
            
            if (Raylib.IsKeyPressed(KeyboardKey.KEY_UP))
                keypress = 7000;
            else if (Raylib.IsKeyPressed(KeyboardKey.KEY_LEFT))
                keypress = 7001;
            else if (Raylib.IsKeyPressed(KeyboardKey.KEY_DOWN))
                keypress = 7002;
            else if (Raylib.IsKeyPressed(KeyboardKey.KEY_RIGHT))
                keypress = 7003;

            switch (keypress){
                case 7000:
                    currentDirection = 3;
                    break;
                case 'w':
                    currentDirection = 3;
                    break;
                case 7001:
                    currentDirection = 1;
                    break;
                case 'a':
                    currentDirection = 1;
                    break;
                case 7002:
                    currentDirection = 0;
                    break;
                case 's':
                    currentDirection = 0;
                    break;
                case 7003:
                    currentDirection = 2;
                    break;
                case 'd':
                    currentDirection = 2;
                    break;
                default:
                    break;
            }

        }

        private void MoveAstronaut()
        {
            switch (currentDirection)
            {
                case 0:
                    y = y + 3;
                    break;
                case 1:
                    x = x - 3;
                    break;
                case 2:
                    x = x + 3;
                    break;
                case 3:
                    y = y - 3;
                    break;
            }
            if (y < -10)
            {
                IsMoving = false;
                y = 0;
            }
            if (y > maxHeight+80)
            {
                IsMoving = false;
                y = maxHeight+70;
            }
            if (x < -50)
            {
                IsMoving = false;
                x = -40;
            }
            if (x > maxWidth)
            {
                IsMoving = false;
                x = maxWidth - 10;
            }
        }

        private static Texture2D frontAstronautOneTexture = new Texture2D();
        private static void frontAstronautOneIcon(int x, int y)
        {
            if (frontAstronautOneTexture.height == 0) {
                Image img = LoadImage(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Images/rockMiniGame/frontAstronaut1.png"));
                frontAstronautOneTexture = LoadTextureFromImage(img);
                UnloadImage(img);
            }
            DrawTextureEx(frontAstronautOneTexture, new Vector2(x, y), 0f, 0.75f, WHITE);
        }
        private static Texture2D frontAstronautTwoTexture = new Texture2D();
        private static void frontAstronautTwoIcon(int x, int y)
        {
            if (frontAstronautTwoTexture.height == 0) {
                Image img = LoadImage(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Images/rockMiniGame/frontAstronaut2.png"));
                frontAstronautTwoTexture = LoadTextureFromImage(img);
                UnloadImage(img);
            }
            DrawTextureEx(frontAstronautTwoTexture, new Vector2(x, y), 0f, 0.75f, WHITE);
        }
        private static Texture2D frontAstronautThreeTexture = new Texture2D();
        private static void frontAstronautThreeIcon(int x, int y)
        {
            if (frontAstronautThreeTexture.height == 0) {
                Image img = LoadImage(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Images/rockMiniGame/frontAstronaut3.png"));
                frontAstronautThreeTexture = LoadTextureFromImage(img);
                UnloadImage(img);
            }
            DrawTextureEx(frontAstronautThreeTexture, new Vector2(x, y), 0f, 0.75f, WHITE);
        }
        private static Texture2D lsideAstronautOneTexture = new Texture2D();
        private static void lsideAstronautOneIcon(int x, int y)
        {
            if (lsideAstronautOneTexture.height == 0) {
                Image img = LoadImage(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Images/rockMiniGame/LsideAstronaut1.png"));
                lsideAstronautOneTexture = LoadTextureFromImage(img);
                UnloadImage(img);
            }
            DrawTextureEx(lsideAstronautOneTexture, new Vector2(x, y), 0f, 0.75f, WHITE);
        }
        private static Texture2D lsideAstronautTwoTexture = new Texture2D();
        private static void lsideAstronautTwoIcon(int x, int y)
        {
            if (lsideAstronautTwoTexture.height == 0) {
                Image img = LoadImage(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Images/rockMiniGame/LsideAstronaut2.png"));
                lsideAstronautTwoTexture = LoadTextureFromImage(img);
                UnloadImage(img);
            }
            DrawTextureEx(lsideAstronautTwoTexture, new Vector2(x, y), 0f, 0.75f, WHITE);
        }
        private static Texture2D lsideAstronautThreeTexture = new Texture2D();
        private static void lsideAstronautThreeIcon(int x, int y)
        {
            if (lsideAstronautThreeTexture.height == 0) {
                Image img = LoadImage(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Images/rockMiniGame/LsideAstronaut3.png"));
                lsideAstronautThreeTexture = LoadTextureFromImage(img);
                UnloadImage(img);
            }
            DrawTextureEx(lsideAstronautThreeTexture, new Vector2(x, y), 0f, 0.75f, WHITE);
        }
        private static Texture2D rsideAstronautOneTexture = new Texture2D();
        private static void rsideAstronautOneIcon(int x, int y)
        {
            if (rsideAstronautOneTexture.height == 0) {
                Image img = LoadImage(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Images/rockMiniGame/RsideAstronaut1.png"));
                rsideAstronautOneTexture = LoadTextureFromImage(img);
                UnloadImage(img);
            }
            DrawTextureEx(rsideAstronautOneTexture, new Vector2(x, y), 0f, 0.75f, WHITE);
        }
        private static Texture2D rsideAstronautTwoTexture = new Texture2D();
        private static void rsideAstronautTwoIcon(int x, int y)
        {
            if (rsideAstronautTwoTexture.height == 0) {
                Image img = LoadImage(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Images/rockMiniGame/RsideAstronaut2.png"));
                rsideAstronautTwoTexture = LoadTextureFromImage(img);
                UnloadImage(img);
            }
            DrawTextureEx(rsideAstronautTwoTexture, new Vector2(x, y), 0f, 0.75f, WHITE);
        }
        private static Texture2D rsideAstronautThreeTexture = new Texture2D();
        private static void rsideAstronautThreeIcon(int x, int y)
        {
            if (rsideAstronautThreeTexture.height == 0) {
                Image img = LoadImage(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Images/rockMiniGame/RsideAstronaut3.png"));
                rsideAstronautThreeTexture = LoadTextureFromImage(img);
                UnloadImage(img);
            }
            DrawTextureEx(rsideAstronautThreeTexture, new Vector2(x, y), 0f, 0.75f, WHITE);
        }
        private static Texture2D backAstronautOneTexture = new Texture2D();
        private static void backAstronautOneIcon(int x, int y)
        {
            if (backAstronautOneTexture.height == 0) {
                Image img = LoadImage(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Images/rockMiniGame/backAstronaut1.png"));
                backAstronautOneTexture = LoadTextureFromImage(img);
                UnloadImage(img);
            }
            DrawTextureEx(backAstronautOneTexture, new Vector2(x, y), 0f, 0.75f, WHITE);
        }
        private static Texture2D backAstronautTwoTexture = new Texture2D();
        private static void backAstronautTwoIcon(int x, int y)
        {
            if (backAstronautTwoTexture.height == 0) {
                Image img = LoadImage(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Images/rockMiniGame/backAstronaut2.png"));
                backAstronautTwoTexture = LoadTextureFromImage(img);
                UnloadImage(img);
            }
            DrawTextureEx(backAstronautTwoTexture, new Vector2(x, y), 0f, 0.75f, WHITE);
        }
        private static Texture2D backAstronautThreeTexture = new Texture2D();
        private static void backAstronautThreeIcon(int x, int y)
        {
            if (backAstronautThreeTexture.height == 0) {
                Image img = LoadImage(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Images/rockMiniGame/backAstronaut3.png"));
                backAstronautThreeTexture = LoadTextureFromImage(img);
                UnloadImage(img);
            }
            DrawTextureEx(backAstronautThreeTexture, new Vector2(x, y), 0f, 0.75f, WHITE);
        }
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
