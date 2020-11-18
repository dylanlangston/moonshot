using Raylib_cs;
using static Raylib_cs.Raylib;
using static Raylib_cs.Color;
using System;
using System.Numerics;
using System.IO;

namespace moonshot.Screens
{
    class mattsStoreFuel : screen
    {
        public override string Name {
            get { return "Matts Store Fuel"; }
        }
        public override void Display()
        {
            ClearBackground(Colors.space);
            starscape();
            Salesman();
            Title();
            Fuel();
            HowMany();
            CurrentBill();
        }
        private static void Title()
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
            Raylib.DrawLineV(new Vector2(220, 110), new Vector2(Raylib.GetScreenWidth()-20, 110), RED);
            Raylib.DrawLineV(new Vector2(220, 111), new Vector2(Raylib.GetScreenWidth()-20, 111), RED);
            Raylib.DrawLineV(new Vector2(220, 112), new Vector2(Raylib.GetScreenWidth()-20, 112), RED);
            Raylib.DrawLineV(new Vector2(220, 113), new Vector2(Raylib.GetScreenWidth()-20, 113), RED);
            Raylib.DrawLineV(new Vector2(220, 114), new Vector2(Raylib.GetScreenWidth()-20, 114), RED);
            Raylib.DrawLineV(new Vector2(220, 115), new Vector2(Raylib.GetScreenWidth()-20, 115), RED);
            Raylib.DrawLineV(new Vector2(220, 116), new Vector2(Raylib.GetScreenWidth()-20, 116), RED);
        }
        private static string _selection = String.Empty;
        private static string selection { 
            get { return _selection; } 
            set { 
                int tempInt = 0; 
                Int32.TryParse(value, out tempInt);
                if (tempInt < 181) 
                    if (value.Length < 4)
                        _selection = value;
                } 
            }
        internal static void HowMany()
        {
            Raylib.DrawText("I sell Fuel in gallons. Each\ngallon costs $1.00.", Raylib.GetScreenWidth()/3, Raylib.GetScreenHeight()/3, 30, WHITE);
            int keypress = Raylib.GetKeyPressed();
            if (Raylib.IsKeyPressed(KeyboardKey.KEY_BACKSPACE)) {
                keypress = 9000;
            } else if (Raylib.IsKeyPressed(KeyboardKey.KEY_ENTER)) {
                if (!String.IsNullOrEmpty(selection))
                {
                    int tempInt = 0; 
                    Int32.TryParse(selection, out tempInt);
                    try {
                        mattsStore.CurrentBill -= mattsStore.CurrentSelection.Items[mattsStore.CurrentSelection.Items.FindIndex(x => x.id == 102)].value;
                        mattsStore.CurrentSelection.Items[mattsStore.CurrentSelection.Items.FindIndex(x => x.id == 102)].value = tempInt;
                    } catch {
                        mattsStore.CurrentSelection.Items.Add(new Fuel(tempInt));
                    }
                    mattsStore.CurrentBill += tempInt;
                    MainWindow.settings.currentScreen = "Matts Store";
                    selection = String.Empty;
                }
            }
            switch (keypress){
                case '1':
                    selection += "1";
                    break;
                case '2':
                    selection += "2";
                    break;
                case '3':
                    selection += "3";
                    break;
                case '4':
                    selection += "4";
                    break;
                case '5':
                    selection += "5";
                    break;
                case '6':
                    selection += "6";
                    break;
                case '7':
                    selection += "7";
                    break;
                case '8':
                    selection += "8";
                    break;
                case '9':
                    selection += "9";
                    break;
                case '0':
                    selection += "0";
                    break;
                case 9000:
                    try {selection = selection.Remove(selection.Length-1, 1);} catch {}
                    break;
                default:
                    break;
            }
            Raylib.DrawText("How many gallons do you\nwant? " + selection + "_", Raylib.GetScreenWidth()/3, Raylib.GetScreenHeight()/2, 30, WHITE);
        }
        internal static void CurrentBill() {
            Raylib.DrawText("Bill so far: $" + mattsStore.CurrentBill + ".00", Raylib.GetScreenWidth()/3, Raylib.GetScreenHeight()/24*21, 30, WHITE);
        }
        // Fuel
        internal static Texture2D fuelTexture = new Texture2D();
        internal static void Fuel()
        {
            if (fuelTexture.height == 0) {
                Image img = LoadImage(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Images/fuel.png"));
                fuelTexture = LoadTextureFromImage(img);
                UnloadImage(img);
            }
            DrawTextureEx(fuelTexture, new Vector2((Raylib.GetScreenWidth()/2)-30, Raylib.GetScreenHeight()/5*3), 0f, 1f, WHITE);
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
