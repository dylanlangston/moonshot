using Raylib_cs;
using static Raylib_cs.Raylib;
using static Raylib_cs.Color;
using System;
using System.Numerics;
using System.IO;
using System.Linq;

namespace moonshot.Screens
{
    class mattsStore : screen
    {
        public override string Name {
            get { return "Matts Store"; }
        }

        public static int CurrentBill = 0;
        public static Inventory CurrentSelection = new Inventory();
        
        public override void Display()
        {
            ClearBackground(Colors.space);
            starscape();
            Salesman();
            Store();
            if (loopCount > 5) {
                if (selection == " ")
                { 
                    DontForget();
                } else {
                    GetInput();
                }
            } else {
                loopCount++;
            }
        }
        private static string selection = String.Empty;
        private static int loopCount = 0;
        private static void Store()
        {
            Raylib.DrawLineV(new Vector2(220, 25), new Vector2(Raylib.GetScreenWidth()-20, 25), RED);
            Raylib.DrawLineV(new Vector2(220, 26), new Vector2(Raylib.GetScreenWidth()-20, 26), RED);
            Raylib.DrawLineV(new Vector2(220, 27), new Vector2(Raylib.GetScreenWidth()-20, 27), RED);
            Raylib.DrawLineV(new Vector2(220, 28), new Vector2(Raylib.GetScreenWidth()-20, 28), RED);
            Raylib.DrawLineV(new Vector2(220, 29), new Vector2(Raylib.GetScreenWidth()-20, 29), RED);
            Raylib.DrawLineV(new Vector2(220, 30), new Vector2(Raylib.GetScreenWidth()-20, 30), RED);
            Raylib.DrawLineV(new Vector2(220, 31), new Vector2(Raylib.GetScreenWidth()-20, 31), RED);
            Raylib.DrawText("Matt's Commissary", Raylib.GetScreenWidth()/20*9, Raylib.GetScreenHeight()/12 - 10, 30, WHITE);
            Raylib.DrawText("Cape Kennedy, Florida", Raylib.GetScreenWidth()/80*33, Raylib.GetScreenHeight()/12 + 20, 30, WHITE);
            string launchDate = PlayerType.GetLaunchDate(MainWindow.settings.userStats.playerType).ToString("MMMM dd, yyyy");
            Raylib.DrawText(launchDate, (Raylib.GetScreenWidth()-(launchDate.Length*20)), Raylib.GetScreenHeight()/12 + 70, 30, WHITE);
            Raylib.DrawLineV(new Vector2(220, 160), new Vector2(Raylib.GetScreenWidth()-20, 160), RED);
            Raylib.DrawLineV(new Vector2(220, 161), new Vector2(Raylib.GetScreenWidth()-20, 161), RED);
            Raylib.DrawLineV(new Vector2(220, 162), new Vector2(Raylib.GetScreenWidth()-20, 162), RED);
            Raylib.DrawLineV(new Vector2(220, 163), new Vector2(Raylib.GetScreenWidth()-20, 163), RED);
            Raylib.DrawLineV(new Vector2(220, 164), new Vector2(Raylib.GetScreenWidth()-20, 164), RED);
            Raylib.DrawLineV(new Vector2(220, 165), new Vector2(Raylib.GetScreenWidth()-20, 165), RED);
            Raylib.DrawLineV(new Vector2(220, 166), new Vector2(Raylib.GetScreenWidth()-20, 166), RED);

            Raylib.DrawText("1.  Oxygen Tanks", Raylib.GetScreenWidth()/3, Raylib.GetScreenHeight()/24*7, 30, WHITE);
            Raylib.DrawText("2. Fuel", Raylib.GetScreenWidth()/3, Raylib.GetScreenHeight()/24*7+35, 30, WHITE);
            Raylib.DrawText("3. Food", Raylib.GetScreenWidth()/3, Raylib.GetScreenHeight()/24*7+70, 30, WHITE);
            Raylib.DrawText("4. Boxes", Raylib.GetScreenWidth()/3, Raylib.GetScreenHeight()/24*7+105, 30, WHITE);
            Raylib.DrawText("5. Spare Ship Parts", Raylib.GetScreenWidth()/3, Raylib.GetScreenHeight()/24*7+140, 30, WHITE);
            int OxygenAmount = 0;
            try {
                OxygenAmount = CurrentSelection.Items[CurrentSelection.Items.FindIndex(x => x.id == 101)].value * 15;
            } catch {}
            Raylib.DrawText("$" + OxygenAmount + ".00", (Raylib.GetScreenWidth()-(OxygenAmount.ToString().Length * 16)-140)+(OxygenAmount.ToString().ToCharArray().Where(x => x == '1').Count()*6), Raylib.GetScreenHeight()/24*7, 30, WHITE);
            int FuelAmount = 0;
            try {
                FuelAmount = CurrentSelection.Items[CurrentSelection.Items.FindIndex(x => x.id == 102)].value;
            } catch {}
            Raylib.DrawText("$" + FuelAmount + ".00", (Raylib.GetScreenWidth()-(FuelAmount.ToString().Length * 16)-140)+(FuelAmount.ToString().ToCharArray().Where(x => x == '1').Count()*6), Raylib.GetScreenHeight()/24*7+35, 30, WHITE);
            int FoodAmount = 0;
            try {
                FoodAmount = CurrentSelection.Items[CurrentSelection.Items.FindIndex(x => x.id == 103)].value;
            } catch {}
            Raylib.DrawText("$" + FoodAmount + ".00", (Raylib.GetScreenWidth()-(FoodAmount.ToString().Length * 16)-140)+(FoodAmount.ToString().ToCharArray().Where(x => x == '1').Count()*6), Raylib.GetScreenHeight()/24*7+70, 30, WHITE);
            int BoxesAmount = 0;
            try {
                BoxesAmount = CurrentSelection.Items[CurrentSelection.Items.FindIndex(x => x.id == 104)].value * 15;
            } catch {}
            Raylib.DrawText("$" + BoxesAmount + ".00", (Raylib.GetScreenWidth()-(BoxesAmount.ToString().Length * 16)-140)+(BoxesAmount.ToString().ToCharArray().Where(x => x == '1').Count()*6), Raylib.GetScreenHeight()/24*7+105, 30, WHITE);
            int PartsAmount = 0;
            try {
                PartsAmount = CurrentSelection.Items[CurrentSelection.Items.FindIndex(x => x.id == 105)].value * 15;
            } catch {}
            Raylib.DrawText("$" + PartsAmount + ".00", (Raylib.GetScreenWidth()-(PartsAmount.ToString().Length * 16)-140)+(PartsAmount.ToString().ToCharArray().Where(x => x == '1').Count()*6), Raylib.GetScreenHeight()/24*7+140, 30, WHITE);


            Raylib.DrawLineV(new Vector2(220, 355), new Vector2(Raylib.GetScreenWidth()-20, 355), RED);
            Raylib.DrawLineV(new Vector2(220, 356), new Vector2(Raylib.GetScreenWidth()-20, 356), RED);
            Raylib.DrawLineV(new Vector2(220, 357), new Vector2(Raylib.GetScreenWidth()-20, 357), RED);
            Raylib.DrawLineV(new Vector2(220, 358), new Vector2(Raylib.GetScreenWidth()-20, 358), RED);
            Raylib.DrawLineV(new Vector2(220, 359), new Vector2(Raylib.GetScreenWidth()-20, 359), RED);
            Raylib.DrawLineV(new Vector2(220, 360), new Vector2(Raylib.GetScreenWidth()-20, 360), RED);
            Raylib.DrawLineV(new Vector2(220, 361), new Vector2(Raylib.GetScreenWidth()-20, 361), RED);

            Raylib.DrawText("Total Bill:", Raylib.GetScreenWidth()/2, Raylib.GetScreenHeight()/96*61, 30, WHITE);
            Raylib.DrawText("$" + CurrentBill + ".00", (Raylib.GetScreenWidth()-(CurrentBill.ToString().Length * 16)-140)+(CurrentBill.ToString().ToCharArray().Where(x => x == '1').Count()*6), Raylib.GetScreenHeight()/96*61, 30, WHITE);

            //MainWindow.settings.userStats.inventory.
        }
        private static void GetInput() {
            Raylib.DrawText("Amount you have: ", Raylib.GetScreenWidth()/3, Raylib.GetScreenHeight()/96*69, 30, WHITE);
            Raylib.DrawText("$" + MainWindow.settings.userStats.Money + ".00", (Raylib.GetScreenWidth()-(MainWindow.settings.userStats.Money.ToString().Length * 16)-140)+(MainWindow.settings.userStats.Money.ToString().ToCharArray().Where(x => x == '1').Count()*6), Raylib.GetScreenHeight()/96*69, 30, WHITE);

            Raylib.DrawText("Which item would you like? " + selection + "_", Raylib.GetScreenWidth()/3 + 30, Raylib.GetScreenHeight()/96*77, 30, WHITE);

            Raylib.DrawText("Press SPACE BAR to leave\nstore", Raylib.GetScreenWidth()/3 + 30, Raylib.GetScreenHeight()/96*85, 30, WHITE);

            int keypress = Raylib.GetKeyPressed();
            if (Raylib.IsKeyPressed(KeyboardKey.KEY_BACKSPACE)) {
                keypress = 9000;
            } else if (Raylib.IsKeyPressed(KeyboardKey.KEY_ENTER)) {
                switch (selection) {
                    case "1":
                        MainWindow.settings.currentScreen = "Matts Store Oxygen Tanks";
                        break;
                    case "2":
                        MainWindow.settings.currentScreen = "Matts Store Fuel";
                        break;
                    case "3":
                        MainWindow.settings.currentScreen = "Matts Store Food";
                        break;
                    case "4":
                        MainWindow.settings.currentScreen = "Matts Store Boxes";
                        break;
                    case "5":
                        MainWindow.settings.currentScreen = "Matts Store Ship Parts";
                        break;
                    default:
                        break;
                }
                loopCount = 0;
                selection = "";
            } else if (Raylib.IsKeyReleased(KeyboardKey.KEY_SPACE)) {
                int tempInt = 0;
                try {
                    tempInt = CurrentSelection.Items[CurrentSelection.Items.FindIndex(x => x.id == 101)].value;
                } catch {}
                if (tempInt > 0)
                    selection = "";
                else
                    selection = " ";
            }
            switch (keypress){
                case '1':
                    selection = "1";
                    break;
                case '2':
                    selection = "2";
                    break;
                case '3':
                    selection = "3";
                    break;
                case '4':
                    selection = "4";
                    break;
                case '5':
                    selection = "5";
                    break;
                case 9000:
                    selection = String.Empty;
                    break;
                default:
                    break;
            }
        }

        private static void DontForget()
        {
            Raylib.DrawText("Don't forget, you'll need\noxygen to breath in space.", Raylib.GetScreenWidth()/3 + 30, Raylib.GetScreenHeight()/96*70, 30, WHITE);
            if (PressSPACEBAR()) {
                selection = "";
            }
        }

        // Salesman
        internal static Texture2D salesmanTexture = new Texture2D();
        internal static void Salesman()
        {
            if (salesmanTexture.height == 0) {
                Image img = LoadImage(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Images/salesman.png"));
                salesmanTexture = LoadTextureFromImage(img);
                UnloadImage(img);
            }
            DrawTextureEx(salesmanTexture, new Vector2(0, 0), 0f, 1f, WHITE);
        }
    }
}
