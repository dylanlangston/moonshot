using System.Text.RegularExpressions;
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
    partial class avoidMeteorGame : AvoidMeteorMiniGameScreen
    {
        public override string Name {
            get { return "Game"; }
        }
        private static avoidMeteorMiniGame game = new avoidMeteorMiniGame();
        private static bool playingGame = false;
        internal static bool gameComplete = false;
        private static System.Timers.Timer theTimer;
        public override void Display()
        {
            if (!playingGame && !gameComplete)
            {
                RandomCrash = 0;
                theTimer = new System.Timers.Timer(20000);
                theTimer.Elapsed += Quit;
                theTimer.Start();
                playingGame = true;
                game.StartGame();
            }
            game.Continue(gameComplete);
            if (gameComplete)
                GameOver();
        }
        private void Quit(object sender, EventArgs e)
        {
            theTimer.Stop();
            theTimer = null;
            gameComplete = true;
        }
        private static int RandomCrash = 0;
        private static PartyMember whosHurt = null;
        private static void GameOver()
        {
            if (theTimer != null)
            {
                theTimer.Stop();
                if (RandomCrash == 0)
                {
                    Random r = new Random();
                    RandomCrash = r.Next(1,10);
                }
                switch (RandomCrash)
                {
                    case 1:
                        MessageBox("You crashed! Ship was damaged on\nimpact.");
                        break;
                    case 2:
                        MessageBox("You crashed! Lost 1 oxygen tank.");
                        break;
                    case 3:
                        MessageBox("You crashed! Lost 1 oxygen tank.");
                        break;
                    default:
                        if (whosHurt == null) {
                        bool healthNeedsReduced = true;
                        if (healthNeedsReduced)
                        {
                            foreach (PartyMember member in MainWindow.settings.userStats.crew.Party.FindAll(c => c.status == PlayerStatus.good))
                            {
                                whosHurt = member;
                                whosHurt.status = PlayerStatus.poor;
                                healthNeedsReduced = false;
                                break;
                            }
                        }
                        if (healthNeedsReduced)
                        {
                            foreach (PartyMember member in MainWindow.settings.userStats.crew.Party.FindAll(c => c.status == PlayerStatus.fair))
                            {
                                whosHurt = member;
                                whosHurt.status = PlayerStatus.veryPoor;
                                healthNeedsReduced = false;
                                break;
                            }
                        }
                        if (healthNeedsReduced)
                        {
                            foreach (PartyMember member in MainWindow.settings.userStats.crew.Party.FindAll(c => c.status == PlayerStatus.poor))
                            {
                                whosHurt = member;
                                whosHurt.status = PlayerStatus.dead;
                                healthNeedsReduced = false;
                                break;
                            }
                        }
                        if (healthNeedsReduced)
                        {
                            foreach (PartyMember member in MainWindow.settings.userStats.crew.Party.FindAll(c => c.status == PlayerStatus.veryPoor))
                            {
                                whosHurt = member;
                                whosHurt.status = PlayerStatus.dead;
                                healthNeedsReduced = false;
                                
                                break;
                            }
                        }
                        }
                        MessageBox("You crashed! " + whosHurt.name + " was\ninjured and is now " + (whosHurt.status == PlayerStatus.dead ? whosHurt.status : "is in\n" + whosHurt.status + " health") + ".");
                        break;
                }
                
            }
            else
                MessageBox("Nice Job! You survived the meteor\nshower.");
            if (screen.PressSPACEBAR()) {
                if (theTimer != null)
                {
                    switch (RandomCrash)
                    {
                        case 1:
                            MainWindow.settings.userStats.ShipWorking = false;
                            break;
                        case 2:
                            MainWindow.settings.userStats.inventory.Items.Find(s => s.id == 101).value--;
                            mainTrail.StartAnimation = true;
                            break;
                        case 3:
                            MainWindow.settings.userStats.inventory.Items.Find(s => s.id == 101).value--;
                            mainTrail.StartAnimation = true;
                            break;
                        default:
                            MainWindow.settings.userStats.crew.Party.Find(member => member.name == whosHurt.name).status = whosHurt.status;
                            mainTrail.StartAnimation = true;
                            break;
                    }
                } else {
                    mainTrail.StartAnimation = true;
                }
                whosHurt = null;
                RandomCrash = 0;
                AvoidMeteorMiniGame.avoidMeteorState = "Selection";
                MainWindow.settings.currentScreen = "Main Trail";
                playingGame = false;
                gameComplete = false;
            }
        }
        private static void MessageBox(string message)
        {
            string[] messageArray = message.Split("\n");
            Raylib.DrawRectangleRounded(new Rectangle(Raylib.GetScreenWidth()/8, Raylib.GetScreenHeight()/3, Raylib.GetScreenWidth()/8*6, 40+(messageArray.Length*30)), 0.25f, 10, WHITE);
            Raylib.DrawRectangle(Raylib.GetScreenWidth()/8+10, Raylib.GetScreenHeight()/3+10, Raylib.GetScreenWidth()/8*6-20, 20+(messageArray.Length*30), BLACK);
            for (int i = 0;i < messageArray.Length;i++)
            {
                Raylib.DrawText(messageArray[i], Raylib.GetScreenWidth()/8+50, Raylib.GetScreenHeight()/3+20+(30*i), 30, WHITE);
            }
        }
    }
    
    class avoidMeteorMiniGame
    {
        private Ship ship = null;
        private Meteors meteors = null;
        public void StartGame()
        {
            meteors = new Meteors(Raylib.GetScreenWidth()-100, Raylib.GetScreenHeight()-100);
            ship = new Ship(100, 375, 50, 750, meteors);
        }
        public void Continue(bool gameComplete)
        {
            ClearBackground(Colors.space);
            screen.starscape();
            ship.DisplayShip(gameComplete);
            meteors.DisplayMeteor(gameComplete);
            Raylib.DrawRectangle(0, 0, 50, 600, BLACK);
            Raylib.DrawRectangle(0, 0, 800, 50, BLACK);
            Raylib.DrawRectangle(750, 0, 50, 600, BLACK);
            Raylib.DrawRectangle(0, 550, 800, 50, BLACK);
            gameBox(50, 50);
        }
        
        private static Texture2D gameBoxTexture = new Texture2D();
        private static void gameBox(int x, int y)
        {
            
            if (gameBoxTexture.height == 0) {
                Image img = LoadImage(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Images/meteorMiniGame/background.png"));
                gameBoxTexture = LoadTextureFromImage(img);
                UnloadImage(img);
            }
            DrawTextureEx(gameBoxTexture, new Vector2(x, y), 0f, 1.0f, WHITE);
        }
    }
    
    class Ship
    {
        int x = 0;
        int y = 0;
        int minWidth = 0;
        int maxWidth = 0;
        int currentDirection = 0;
        Meteors meteors = null;
        public Ship(int startX, int startY, int MinWidth, int MaxWidth, Meteors meteorsIn)
        {
            meteors = meteorsIn;
            x = startX;
            y = startY;
            lunarModule(-500, -500);
            maxWidth = MaxWidth - (int)(lunarModuleTexture.width*0.25f);
            minWidth = MinWidth;
            DisplayShip();
        }
        public void DisplayShip(bool gameComplete = false)
        {
            if (!gameComplete)
            {
                GetKeyPress();
                DetectCrash();
            }
            lunarModule(x, y);
        }
        private void GetKeyPress()
        {
            int keypress = 0;
            
            if (Raylib.IsKeyDown(KeyboardKey.KEY_LEFT))
                keypress = 7001;
            else if (Raylib.IsKeyDown(KeyboardKey.KEY_RIGHT))
                keypress = 7003;
            else if (Raylib.IsKeyDown(KeyboardKey.KEY_A))
                keypress = 'a';
            else if (Raylib.IsKeyDown(KeyboardKey.KEY_D))
                keypress = 'd';

            switch (keypress){
                case 7001:
                    currentDirection = 0;
                    MoveShip();
                    break;
                case 'a':
                    currentDirection = 0;
                    MoveShip();
                    break;
                case 7003:
                    currentDirection = 1;
                    MoveShip();
                    break;
                case 'd':
                    currentDirection = 1;
                    MoveShip();
                    break;
                default:
                    break;
            }

        }

        private void MoveShip()
        {
            switch (currentDirection)
            {
                case 0:
                    x = x - 3;
                    break;
                case 1:
                    x = x + 3;
                    break;
            }
            if (x < minWidth)
            {
                x = minWidth + 10;
            }
            if (x > maxWidth)
            {
                x = maxWidth - 10;
            }
        }

        public class ParametricLine{
            System.Drawing.PointF p1;
            System.Drawing.PointF p2;
            
            public ParametricLine(System.Drawing.PointF p1, System.Drawing.PointF p2) {
                this.p1 = p1;
                this.p2 = p2;
            }
            
            public System.Drawing.PointF Fraction(float frac) {
                return new System.Drawing.PointF( p1.X + frac*(p2.X-p1.X), p1.Y + frac*(p2.Y-p1.Y));
            }
        }

        private void DetectCrash()
        {
            foreach (var meteor in meteors.meteorTypesAndLocations)
            {
                int centerpointX = meteor.x + (int)((meteors.GetTexture(meteor.index).width/2)*meteor.scale);
                int centerpointY = meteor.y + (int)((meteors.GetTexture(meteor.index).height-(meteors.GetTexture(meteor.index).height*(1/3)))*meteor.scale);
                int radius = (int)((meteors.GetTexture(meteor.index).width/2)*meteor.scale);

                if (Raylib.CheckCollisionCircles(new Vector2(centerpointX, centerpointY-radius), radius, new Vector2(x+40, y+45), (lunarModuleTexture.width*(0.20f))/2))
                    avoidMeteorGame.gameComplete = true;
            }

        }

        private static Texture2D lunarModuleTexture = new Texture2D();
        private static void lunarModule(int x, int y)
        {
            if (lunarModuleTexture.height == 0) {
                Image img = LoadImage(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Images/meteorMiniGame/lunar Module.png"));
                lunarModuleTexture = LoadTextureFromImage(img);
                UnloadImage(img);
            }
            DrawTextureEx(lunarModuleTexture, new Vector2(x, y), 0f, 0.25f, WHITE);
        }
    }

    class Meteors
    {
        public List<(int index, int x, int y, float scale, int speed)> meteorTypesAndLocations = new List<(int index, int x, int y, float scale, int speed)>();
        private static int MaxWidth = 0;
        private static int MaxHeight = 0;
        public Meteors(int maxWidth, int maxHeight)
        {
            MaxWidth = maxWidth;
            MaxHeight = maxHeight;
            if (meteorTypesAndLocations.Count == 0)
            {
                Random r = new Random();
                int loops = r.Next(5,10);
                for (int i = 0; i < loops; i++)
                {
                    int RandIndex = r.Next(1,4);
                    int RandX = r.Next(30,maxWidth-30);
                    int RandY = r.Next(-1000,-50);
                    int RandSpeed = r.Next(1,6);
                    float RandScale = (float)r.NextDouble();
                    if (RandScale < 0.45f) {RandScale+=0.35f; }
                    if (RandScale > 0.85f) {RandScale=0.85f; }
                    meteorTypesAndLocations.Add((RandIndex, RandX, RandY, RandScale, RandSpeed));
                }
            }
            else {
                DisplayMeteor();
            }
        }
        public void DisplayMeteor(bool gameComplete = false)
        {
            Random r = new Random();
            for (int c = 0; c < meteorTypesAndLocations.Count; c++)
            {
                if (!gameComplete)
                {
                if (meteorTypesAndLocations[c].y > MaxHeight + 200)
                    meteorTypesAndLocations[c] = (meteorTypesAndLocations[c].index, r.Next(30,MaxWidth-30), r.Next(-1000,-50), meteorTypesAndLocations[c].scale, r.Next(1,6));
                else
                    meteorTypesAndLocations[c] = (meteorTypesAndLocations[c].index, meteorTypesAndLocations[c].x, meteorTypesAndLocations[c].y + meteorTypesAndLocations[c].speed, meteorTypesAndLocations[c].scale, meteorTypesAndLocations[c].speed);
                }
                DisplayMeteor(meteorTypesAndLocations[c].index, meteorTypesAndLocations[c].x, meteorTypesAndLocations[c].y, meteorTypesAndLocations[c].scale);
            }
        }
        public void DisplayMeteor(int index, int x, int y, float scale)
        {
            switch (index)
            {
                case 1:
                    meteorOneIcon(x, y, scale);
                    break;
                case 2:
                    meteorTwoIcon(x, y, scale);
                    break;
                default:
                    meteorThreeIcon(x, y, scale);
                    break;
            }
        }
        public Texture2D GetTexture(int index)
        {
            switch (index)
            {
                case 1:
                    return meteorOneTexture;
                case 2:
                    return meteorTwoTexture;
                default:
                    return meteorThreeTexture;
            }
        }

        private static Texture2D meteorOneTexture = new Texture2D();
        private static void meteorOneIcon(int x, int y, float scale)
        {
            if (meteorOneTexture.height == 0) {
                Image img = LoadImage(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Images/meteorMiniGame/meteor1.png"));
                meteorOneTexture = LoadTextureFromImage(img);
                UnloadImage(img);
            }
            DrawTextureEx(meteorOneTexture, new Vector2(x, y), 0f, scale, WHITE);
        }
        private static Texture2D meteorTwoTexture = new Texture2D();
        private static void meteorTwoIcon(int x, int y, float scale)
        {
            if (meteorTwoTexture.height == 0) {
                Image img = LoadImage(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Images/meteorMiniGame/meteor2.png"));
                meteorTwoTexture = LoadTextureFromImage(img);
                UnloadImage(img);
            }
            DrawTextureEx(meteorTwoTexture, new Vector2(x, y), 0f, scale, WHITE);
        }
        private static Texture2D meteorThreeTexture = new Texture2D();
        private static void meteorThreeIcon(int x, int y, float scale)
        {
            if (meteorThreeTexture.height == 0) {
                Image img = LoadImage(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Images/meteorMiniGame/meteor3.png"));
                meteorThreeTexture = LoadTextureFromImage(img);
                UnloadImage(img);
            }
            DrawTextureEx(meteorThreeTexture, new Vector2(x, y), 0f, scale, WHITE);
        }
    }
}
