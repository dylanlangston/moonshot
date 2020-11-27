using Raylib_cs;
using static Raylib_cs.Raylib;
using static Raylib_cs.Color;
using System;
using System.Numerics;
using System.Windows.Input;
using System.IO;
using System.Linq;

namespace moonshot.Screens
{
    class arrivalPoints : screen
    {
        public override string Name {
            get { return "Arrival Points"; }
        }
        public override void Display()
        {
            ClearBackground(Colors.space);
            starscape();
            Message();
            CalcPoints();
            Confirmation();
        }
        private static void Message() {
            Raylib.DrawRectangle(Raylib.GetScreenWidth()/16, 10, Raylib.GetScreenWidth()/8*7, 35, WHITE);
            Raylib.DrawText("Points for arriving in Peary", (Raylib.GetScreenWidth()-("Points for arriving in Peary".Length*15))/2, 12, 30, BLACK);
        }
        public static int Score = 0;
        public static int multiplyMod = 1;
        internal static void CalcPoints()
        {
            Score = 0;
            int offsetY = 100;

            int PlayersGood = 0;
            int PlayersFair = 0;
            int PlayersPoor = 0;
            int PlayersVeryPoor = 0;
            foreach (PartyMember member in MainWindow.settings.userStats.crew.Party)
            {
                switch (member.status)
                {
                    case PlayerStatus.good:
                        PlayersGood++;
                        break;
                    case PlayerStatus.fair:
                        PlayersFair++;
                        break;
                    case PlayerStatus.poor:
                        PlayersPoor++;
                        break;
                    case PlayerStatus.veryPoor:
                        PlayersVeryPoor++;
                        break;
                    default:
                        break;
                }
            }

            if (PlayersGood > 0)
            {
                Raylib.DrawText(PlayersGood.ToString(), 70-(PlayersGood.ToString().Length * 16)+(PlayersGood.ToString().ToCharArray().Where(x => x == '1').Count()*6), offsetY, 30, WHITE);
                Raylib.DrawText((PlayersGood > 1 ? "people" : "person") + " in good health", 100, offsetY, 30, WHITE);

                int tempScore = PlayersGood*500*multiplyMod;
                Raylib.DrawText(tempScore.ToString(), 700-(tempScore.ToString().Length * 16)+(tempScore.ToString().ToCharArray().Where(x => x == '1').Count()*6), offsetY, 30, WHITE);
                Score += tempScore;
                offsetY += 33;
            }

            if (PlayersFair > 0)
            {
                Raylib.DrawText(PlayersFair.ToString(), 70-(PlayersFair.ToString().Length * 16)+(PlayersFair.ToString().ToCharArray().Where(x => x == '1').Count()*6), offsetY, 30, WHITE);
                Raylib.DrawText((PlayersFair > 1 ? "people" : "person") + " in fair health", 100, offsetY, 30, WHITE);
                
                int tempScore = PlayersFair*400*multiplyMod;
                Raylib.DrawText(tempScore.ToString(), 700-(tempScore.ToString().Length * 16)+(tempScore.ToString().ToCharArray().Where(x => x == '1').Count()*6), offsetY, 30, WHITE);
                Score += tempScore;
                offsetY += 33;
            }

            if (PlayersPoor > 0)
            {
                Raylib.DrawText(PlayersPoor.ToString(), 70-(PlayersPoor.ToString().Length * 16)+(PlayersPoor.ToString().ToCharArray().Where(x => x == '1').Count()*6), offsetY, 30, WHITE);
                Raylib.DrawText((PlayersPoor > 1 ? "people" : "person") + " in poor health", 100, offsetY, 30, WHITE);

                int tempScore = PlayersPoor*300*multiplyMod;
                Raylib.DrawText(tempScore.ToString(), 700-(tempScore.ToString().Length * 16)+(tempScore.ToString().ToCharArray().Where(x => x == '1').Count()*6), offsetY, 30, WHITE);
                Score += tempScore;
                offsetY += 33;
            }

            if (PlayersVeryPoor > 0)
            {
                Raylib.DrawText(PlayersVeryPoor.ToString(), 70-(PlayersVeryPoor.ToString().Length * 16)+(PlayersVeryPoor.ToString().ToCharArray().Where(x => x == '1').Count()*6), offsetY, 30, WHITE);
                Raylib.DrawText((PlayersVeryPoor > 1 ? "people" : "person") + " in very poor health", 100, offsetY, 30, WHITE);

                int tempScore = PlayersVeryPoor*200*multiplyMod;
                Raylib.DrawText(tempScore.ToString(), 700-(tempScore.ToString().Length * 16)+(tempScore.ToString().ToCharArray().Where(x => x == '1').Count()*6), offsetY, 30, WHITE);
                Score += tempScore;
                offsetY += 33;
            }

            Raylib.DrawText("1", 60, offsetY, 30, WHITE);
            Raylib.DrawText("Ship", 100, offsetY, 30, WHITE);

            Raylib.DrawText((50*multiplyMod).ToString(), 700-((50*multiplyMod).ToString().Length * 16)+((50*multiplyMod).ToString().ToCharArray().Where(x => x == '1').Count()*6), offsetY, 30, WHITE);
            Score += 50*multiplyMod;
            offsetY += 33;

            int oxygenTanks = MainWindow.settings.userStats.inventory.Items.Find(s => s.id == 101).value;
            if (oxygenTanks > 0)
            {
                Raylib.DrawText(oxygenTanks.ToString(), 70-(oxygenTanks.ToString().Length * 16)+(oxygenTanks.ToString().ToCharArray().Where(x => x == '1').Count()*6), offsetY, 30, WHITE);
                Raylib.DrawText("oxygen tanks", 100, offsetY, 30, WHITE);

                int tempScore = oxygenTanks*4*multiplyMod;
                Raylib.DrawText(tempScore.ToString(), 700-(tempScore.ToString().Length * 16)+(tempScore.ToString().ToCharArray().Where(x => x == '1').Count()*6), offsetY, 30, WHITE);
                Score += tempScore;
                offsetY += 33;
            }

            int shipParts = MainWindow.settings.userStats.inventory.Items.Find(s => s.id == 105).value;
            if (shipParts > 0)
            {
                Raylib.DrawText(shipParts.ToString(), 70-(shipParts.ToString().Length * 16)+(shipParts.ToString().ToCharArray().Where(x => x == '1').Count()*6), offsetY, 30, WHITE);
                Raylib.DrawText("spare ship parts", 100, offsetY, 30, WHITE);

                int tempScore = shipParts*2*multiplyMod;
                Raylib.DrawText(tempScore.ToString(), 700-(tempScore.ToString().Length * 16)+(tempScore.ToString().ToCharArray().Where(x => x == '1').Count()*6), offsetY, 30, WHITE);
                Score += tempScore;
                offsetY += 33;
            }

            int boxCounts = MainWindow.settings.userStats.inventory.Items.Find(s => s.id == 104).value;
            if (boxCounts > 0)
            {
                Raylib.DrawText(boxCounts.ToString(), 70-(boxCounts.ToString().Length * 16)+(boxCounts.ToString().ToCharArray().Where(x => x == '1').Count()*6), offsetY, 30, WHITE);
                Raylib.DrawText("boxes", 100, offsetY, 30, WHITE);

                int tempScore = boxCounts*2*multiplyMod;
                Raylib.DrawText(tempScore.ToString(), 700-(tempScore.ToString().Length * 16)+(tempScore.ToString().ToCharArray().Where(x => x == '1').Count()*6), offsetY, 30, WHITE);
                Score += tempScore;
                offsetY += 33;
            }

            int fuelCounts = MainWindow.settings.userStats.inventory.Items.Find(s => s.id == 102).value;
            if (fuelCounts > 0)
            {
                Raylib.DrawText(fuelCounts.ToString(), 70-(fuelCounts.ToString().Length * 16)+(fuelCounts.ToString().ToCharArray().Where(x => x == '1').Count()*6), offsetY, 30, WHITE);
                Raylib.DrawText("gallons of fuel", 100, offsetY, 30, WHITE);

                int tempScore = (fuelCounts / 25)*multiplyMod > 0 ? (fuelCounts / 25)*multiplyMod : 0;
                Raylib.DrawText(tempScore.ToString(), 700-(tempScore.ToString().Length * 16)+(tempScore.ToString().ToCharArray().Where(x => x == '1').Count()*6), offsetY, 30, WHITE);
                Score += tempScore;
                offsetY += 33;
            }

            int foodCounts = MainWindow.settings.userStats.inventory.Items.Find(s => s.id == 103).value;
            if (foodCounts > 0)
            {
                Raylib.DrawText(foodCounts.ToString(), 70-(foodCounts.ToString().Length * 16)+(foodCounts.ToString().ToCharArray().Where(x => x == '1').Count()*6), offsetY, 30, WHITE);
                Raylib.DrawText("pounds of food", 100, offsetY, 30, WHITE);

                int tempScore = (foodCounts / 25)*multiplyMod > 0 ? (foodCounts / 25)*multiplyMod : 0;
                Raylib.DrawText(tempScore.ToString(), 700-(tempScore.ToString().Length * 16)+(tempScore.ToString().ToCharArray().Where(x => x == '1').Count()*6), offsetY, 30, WHITE);
                Score += tempScore;
                offsetY += 33;
            }

            int cashRemaining = MainWindow.settings.userStats.Money;
            if (cashRemaining > 0)
            {
                Raylib.DrawText("$ "+cashRemaining+".00 cash", 100, offsetY, 30, WHITE);

                int tempScore = (cashRemaining / 5)*multiplyMod > 0 ? (cashRemaining / 5)*multiplyMod : 0;
                Raylib.DrawText(tempScore.ToString(), 700-(tempScore.ToString().Length * 16)+(tempScore.ToString().ToCharArray().Where(x => x == '1').Count()*6), offsetY, 30, WHITE);
                Score += tempScore;
                offsetY += 33;
            }

            offsetY += 10;
            Raylib.DrawText("Total:", 500, offsetY, 30, WHITE);
            Raylib.DrawText(Score.ToString(), 700-(Score.ToString().Length * 16)+(Score.ToString().ToCharArray().Where(x => x == '1').Count()*6), offsetY, 30, WHITE);
        
            offsetY += 40;
            if (MainWindow.settings.userStats.playerType == PlayerType.apollo12)
                Raylib.DrawText("For going as Apollo 12 your points are doubled.", 30, offsetY, 30, WHITE);
            if (MainWindow.settings.userStats.playerType == PlayerType.apollo14)
                Raylib.DrawText("For going as Apollo 14 your points are tripled.", 30, offsetY, 30, WHITE);
        }

        private static void Confirmation(){
            if (PressSPACEBAR()) {
                if (MainWindow.settings.userStats.playerType == PlayerType.apollo12 && multiplyMod == 1)
                    multiplyMod = 2;
                else if (MainWindow.settings.userStats.playerType == PlayerType.apollo14 && multiplyMod == 1)
                    multiplyMod = 3;
                else
                {
                    multiplyMod = 1;
                    MainWindow.settings.currentScreen = "Edit Top Ten";
                }
            }
        }
    }
}
