using System;
using System.Collections.Generic;
using System.Text;
using Raylib_cs;
using static Raylib_cs.Raylib;
using static Raylib_cs.Color;

namespace moonshot
{

    class Settings
    {
        private static Tuple<int, int, int> screensize = GetMaxScreenSize();
        public static int DefaultScreenNumber = screensize.Item1;
        public static int DefaultWidth = screensize.Item2;
        public static int DefaultHeight = screensize.Item3;
        public static bool StartFullscreen = true;
        private static Tuple<int, int, int> GetMaxScreenSize()
        {
            int screen = 0;
            int width =  0 ;
            int height = 0;
            for(int i=0; i < Raylib.GetMonitorCount(); i++)
            {
                if (width < Raylib.GetMonitorWidth(i))
                {
                    width = Raylib.GetMonitorWidth(i);
                    height = Raylib.GetMonitorHeight(i);
                    screen = i;
                }
                else if (height < Raylib.GetMonitorHeight(i))
                {
                    width = Raylib.GetMonitorWidth(i);
                    height = Raylib.GetMonitorHeight(i);
                    screen = i;
                }
            }
            return new Tuple<int, int, int>(screen, width, height);
        }
    }
}
