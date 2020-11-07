using Raylib_cs;
using static Raylib_cs.Raylib;
using static Raylib_cs.Color;

namespace moonshot.Screens
{
    abstract class screen
    {
        public screen()
        {
            Display();
        }
        public abstract void Display();
    }
}
