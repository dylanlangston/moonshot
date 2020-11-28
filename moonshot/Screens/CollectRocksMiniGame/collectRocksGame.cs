using System.Text.RegularExpressions;
using Raylib_cs;
using static Raylib_cs.Raylib;
using static Raylib_cs.Color;
using System;
using System.Numerics;
using System.Windows.Input;
using System.IO;
using System.Collections.Generic;
// Note: requires libgdiplus
using System.Drawing.Drawing2D;

namespace moonshot.Screens
{
    partial class collectRocksGame : collectRocksMiniGameScreen
    {
        public override string Name {
            get { return "Game"; }
        }
        private static collectRocksMiniGame game = new collectRocksMiniGame();
        private static bool playingGame = false;
        private static System.Timers.Timer theTimer;
        public override void Display()
        {
            if (!playingGame)
            {
                theTimer = new System.Timers.Timer(30000);
                theTimer.Elapsed += Quit;
                theTimer.Start();
                playingGame = true;
                game.StartGame();
            }
            else
                game.Continue();
        }
        private void Quit(object sender, EventArgs e)
        {
            theTimer.Stop();
            theTimer = null;
            playingGame = false;
            collectRocks.CollectRocksState = "Complete";
        }

    }
    class collectRocksMiniGame
    {
        private Rocks rocks = null;
        private Astronaut astronaut = null;
        private Decor decor = null;
        public collectRocksMiniGame()
        {   
        }
        public void StartGame()
        {
            decor = new Decor(Raylib.GetScreenWidth()-100, Raylib.GetScreenHeight()-100);
            rocks = new Rocks(Raylib.GetScreenWidth()-100, Raylib.GetScreenHeight()-100);
            astronaut = new Astronaut(350, 350, Raylib.GetScreenWidth(), Raylib.GetScreenHeight(), rocks);
            Continue();
        }
        public void Continue()
        {
            Raylib.ClearBackground(GRAY);
            decor.DisplayDecor();
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
        Rocks rocks = null;
        public Astronaut(int startX, int startY, int MaxWidth, int MaxHeight, Rocks RocksIn)
        {
            x = startX;
            y = startY;
            frontAstronautOneIcon(-500, -500);
            maxWidth = MaxWidth - frontAstronautOneTexture.width+100;
            maxHeight = MaxHeight - frontAstronautOneTexture.height;
            rocks = RocksIn;
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
            
            if (Raylib.IsKeyReleased(KeyboardKey.KEY_SPACE))
                TryPickUpRock();

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

        // https://stackoverflow.com/a/49886367
        private void TryPickUpRock()
        {
            List<(int index, int x, int y, float roation, float scale)> rocksToRemove = new List<(int index, int x, int y, float roation, float scale)>(){};
            foreach (var rock in rocks.rockTypesAndLocations)
            {
                // Old code, couldn't handle rotation of rocks
                //bool matchX = false;
                //bool matchY = false;
                //if (x > (rock.x - (rocks.GetTexture(rock.index).width*rock.scale)))
                    //if (x < rock.x)
                       // matchX = true;
                //if (y+40 > (rock.y - (rocks.GetTexture(rock.index).height*rock.scale)))
                    //if (y+40 < rock.y)
                        //matchY = true;
                //if (matchX && matchY)
                    //rocksToRemove.Add(rock);


                System.Drawing.RectangleF clientRectangle = new System.Drawing.RectangleF(rock.x,rock.y,(rocks.GetTexture(rock.index).width*rock.scale),(rocks.GetTexture(rock.index).height*rock.scale));

                // Create Matrix and rotate, create points.
                // Note: requires libgdiplus
                Matrix matrix = new Matrix();
                var p = new System.Drawing.PointF[] {
                clientRectangle.Location,
                new System.Drawing.PointF(clientRectangle.Right, clientRectangle.Top),
                new System.Drawing.PointF(clientRectangle.Right, clientRectangle.Bottom),
                new System.Drawing.PointF(clientRectangle.Left, clientRectangle.Bottom) };


                matrix.RotateAt(rock.rotation, new System.Drawing.PointF(clientRectangle.X, clientRectangle.Top));
                matrix.TransformPoints(p);

                var astronautRectangle = new Raylib_cs.Rectangle(x + 50, y + 10, 50, 110);

                // Detect if we're touching the corners of the rocks.
                if (Raylib.CheckCollisionPointRec(new Vector2(p[0].X, p[0].Y), astronautRectangle))
                    rocksToRemove.Add(rock);
                if (Raylib.CheckCollisionPointRec(new Vector2(p[1].X, p[1].Y), astronautRectangle))
                    rocksToRemove.Add(rock);
                if (Raylib.CheckCollisionPointRec(new Vector2(p[2].X, p[2].Y), astronautRectangle))
                    rocksToRemove.Add(rock);
                if (Raylib.CheckCollisionPointRec(new Vector2(p[3].X, p[3].Y), astronautRectangle))
                    rocksToRemove.Add(rock);

            }
            foreach (var rock in rocksToRemove)
            {
                collectRocks.rocksCollected++;
                rocks.rockTypesAndLocations.Remove(rock);
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
        public List<(int index, int x, int y, float rotation, float scale)> rockTypesAndLocations = new List<(int index, int x, int y, float rotation, float scale)>();
        public Rocks(int maxWidth, int maxHeight)
        {
            if (rockTypesAndLocations.Count == 0)
            {
                Random r = new Random();
                int loops = r.Next(1,7);
                for (int i = 0; i < loops; i++)
                {
                    int RandIndex = r.Next(1,6);
                    int RandX = r.Next(30,maxWidth-30);
                    int RandY = r.Next(30,maxHeight-30);
                    float RandRotation = (float)r.Next(1, 265);
                    float RandScale = (float)r.NextDouble();
                    if (RandScale < 0.45f) {RandScale+=0.35f; }
                    if (RandScale > 0.85f) {RandScale=0.85f; }
                    rockTypesAndLocations.Add((RandIndex, RandX, RandY, RandRotation, RandScale));
                }
            }
            else {
                foreach (var rock in rockTypesAndLocations)
                {
                    DisplayRock(rock.index, rock.x, rock.y, rock.rotation, rock.scale);
                }
            }
        }
        public void DisplayRocks()
        {
            foreach (var rock in rockTypesAndLocations)
            {
                DisplayRock(rock.index, rock.x, rock.y, rock.rotation, rock.scale);
            }
        }
        public void DisplayRock(int index, int x, int y, float rotation, float scale)
        {
            switch (index)
            {
                case 1:
                    rockOneIcon(x, y, rotation, scale);
                    break;
                case 2:
                    rockTwoIcon(x, y, rotation, scale);
                    break;
                case 3:
                    rockThreeIcon(x, y, rotation, scale);
                    break;
                case 4:
                    rockFourIcon(x, y, rotation, scale);
                    break;
                default:
                    rockFiveIcon(x, y, rotation, scale);
                    break;
            }
        }
        public Texture2D GetTexture(int index)
        {
            switch (index)
            {
                case 1:
                    return rockOneTexture;
                case 2:
                    return rockTwoTexture;
                case 3:
                    return rockThreeTexture;
                case 4:
                    return rockFourTexture;
                default:
                    return rockFiveTexture;
            }
        }

        private static Texture2D rockOneTexture = new Texture2D();
        private static void rockOneIcon(int x, int y, float rotation, float scale)
        {
            if (rockOneTexture.height == 0) {
                Image img = LoadImage(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Images/rockMiniGame/rock1.png"));
                rockOneTexture = LoadTextureFromImage(img);
                UnloadImage(img);
            }
            DrawTextureEx(rockOneTexture, new Vector2(x, y), rotation, scale, WHITE);
        }
        private static Texture2D rockTwoTexture = new Texture2D();
        private static void rockTwoIcon(int x, int y, float rotation, float scale)
        {
            if (rockTwoTexture.height == 0) {
                Image img = LoadImage(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Images/rockMiniGame/rock2.png"));
                rockTwoTexture = LoadTextureFromImage(img);
                UnloadImage(img);
            }
            DrawTextureEx(rockTwoTexture, new Vector2(x, y), rotation, scale, WHITE);
        }
        private static Texture2D rockThreeTexture = new Texture2D();
        private static void rockThreeIcon(int x, int y, float rotation, float scale)
        {
            if (rockThreeTexture.height == 0) {
                Image img = LoadImage(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Images/rockMiniGame/rock3.png"));
                rockThreeTexture = LoadTextureFromImage(img);
                UnloadImage(img);
            }
            DrawTextureEx(rockThreeTexture, new Vector2(x, y), rotation, scale, WHITE);
        }
        private static Texture2D rockFourTexture = new Texture2D();
        private static void rockFourIcon(int x, int y, float rotation, float scale)
        {
            if (rockFourTexture.height == 0) {
                Image img = LoadImage(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Images/rockMiniGame/rock4.png"));
                rockFourTexture = LoadTextureFromImage(img);
                UnloadImage(img);
            }
            DrawTextureEx(rockFourTexture, new Vector2(x, y), rotation, scale, WHITE);
        }
        private static Texture2D rockFiveTexture = new Texture2D();
        private static void rockFiveIcon(int x, int y, float rotation, float scale)
        {
            if (rockFiveTexture.height == 0) {
                Image img = LoadImage(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Images/rockMiniGame/rock5.png"));
                rockFiveTexture = LoadTextureFromImage(img);
                UnloadImage(img);
            }
            DrawTextureEx(rockFiveTexture, new Vector2(x, y), rotation, scale, WHITE);
        }
    }
    class Decor
    {
        public List<(int index, int x, int y, float rotation, float scale)> decorTypesAndLocations = new List<(int index, int x, int y, float rotation, float scale)>();
        public Decor(int maxWidth, int maxHeight)
        {
            if (decorTypesAndLocations.Count == 0)
            {
                Random r = new Random();
                int loops = r.Next(8,12);
                for (int i = 0; i < loops; i++)
                {
                    int RandIndex = r.Next(0,4);
                    int RandX = r.Next(30,maxWidth-30);
                    int RandY = r.Next(30,maxHeight-30);
                    float RandRotation = (float)r.Next(0, 365);
                    float RandScale = (float)r.NextDouble();
                    if (RandScale < 0.3f) { RandScale += 0.2f;}
                    decorTypesAndLocations.Add((RandIndex, RandX, RandY, RandRotation, RandScale));
                }
            }
            else {
                foreach (var decor in decorTypesAndLocations)
                {
                    DisplayDecor(decor.index, decor.x, decor.y, decor.rotation, decor.scale);
                }
            }
        }
        public void DisplayDecor()
        {
            foreach (var decor in decorTypesAndLocations)
            {
                DisplayDecor(decor.index, decor.x, decor.y, decor.rotation, decor.scale);
            }
        }
        public void DisplayDecor(int index, int x, int y,float rotation, float scale)
        {
            switch (index)
            {
                case 1:
                    cratorDecorIcon(x ,y, rotation, scale);
                    break;
                default:
                    ridgesDecorIcon(x ,y, rotation, scale);
                    break;
            }
        }

        private static Texture2D cratorDecorTexture = new Texture2D();
        private static void cratorDecorIcon(int x, int y, float rotation, float scale)
        {
            if (cratorDecorTexture.height == 0) {
                Image img = LoadImage(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Images/rockMiniGame/craterDecor.png"));
                cratorDecorTexture = LoadTextureFromImage(img);
                UnloadImage(img);
            }
            DrawTextureEx(cratorDecorTexture, new Vector2(x, y), rotation, scale, WHITE);
        }
        private static Texture2D ridgesDecorTexture = new Texture2D();
        private static void ridgesDecorIcon(int x, int y, float rotation, float scale)
        {
            if (ridgesDecorTexture.height == 0) {
                Image img = LoadImage(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Images/rockMiniGame/ridgesDecor.png"));
                ridgesDecorTexture = LoadTextureFromImage(img);
                UnloadImage(img);
            }
            DrawTextureEx(ridgesDecorTexture, new Vector2(x, y), rotation, scale, WHITE);
        }
    }
}
