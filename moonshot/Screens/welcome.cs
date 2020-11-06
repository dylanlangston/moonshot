using Raylib_cs;
using static Raylib_cs.Raylib;
using static Raylib_cs.Color;

namespace moonshot.Screens
{
    class welcome : screen
    {
        public override void Display()
        {
            DrawText("Welcome to Moonshot!", 220, 200, 20, MAROON);
        }
    }
}
