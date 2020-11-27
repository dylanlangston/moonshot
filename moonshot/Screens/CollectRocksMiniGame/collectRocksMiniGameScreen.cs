using Raylib_cs;
using static Raylib_cs.Raylib;
using static Raylib_cs.Color;
using System;
using System.Numerics;
using System.Collections.Generic;
using System.IO;

namespace moonshot.Screens
{
    abstract class collectRocksMiniGameScreen
    {
        public collectRocksMiniGameScreen()
        {
        }
        public abstract string Name {
            get;
        }
        public abstract void Display();
    }
}
