using Raylib_cs;
using static Raylib_cs.Raylib;
using static Raylib_cs.Color;
using System;
using System.Numerics;
using System.Collections.Generic;
using System.IO;

namespace moonshot.Screens
{
    class TalkToPeople : screen
    {
        public override string Name {
            get { return "Talk to People"; }
        }
        
        public override void Display()
        {
            ClearBackground(Colors.space);
            starscape();
            Menuline(Raylib.GetScreenWidth() / 128, 10);
            Menuline(Raylib.GetScreenWidth() / 128, Raylib.GetScreenHeight() / 24 * 20 );
            UserStats stats = MainWindow.settings.userStats;
            DisplayMessage(stats.currentLocation);
            Confirmation();
        }

        private static void Confirmation(){
            if (PressSPACEBAR()) {
                choosenMessage = 99;
                MainWindow.settings.currentScreen = "Check Stats";
            }
        }

        private static List<List<string>> missionControlMessages = new List<List<string>>(){
            // Landing Site - 0
            new List<string>(){
                "The target site for this landing was not very\nexact, in fact, it was 11 and a half miles wide\n(and three miles tall)!",
                "Congratulations on landing safely on the Moon!\nYou are one of very few people to do so.",
                "You made it to the Moon! Try collecting rocks\nto see the surface up close. "
            },
            // Mare Tranquillitatis - 1
            new List<string>(){
                "'Mare' is Latin for sea. Astronomers originally\ncalled them maria (plural of 'mare') because,\nfrom Earth, these dark regions look like vast\noceans. Nowadays, we know this is because of\ntheir iron-rich composition.",
                "Mare Tranquillitatis has a slight blue tint\nrelative to the rest of the Moon due to its high\nmetal content in the basaltic soil.",
                "'Mare Tranquillitatis' is Latin for Sea of\nTranquility and is located in the Tranquillitatis\nbasin."
            },
            // MTP - 2
            new List<string>(){
                "Lunar pits, like this one, could have caves\nin them! Scientists believe that these caves could\nhelp protect humans from radiation, extreme\ntemperature changes, and micrometeorites if we\nwere to live on the Moon.",
                "There are around 200 pits on the Moon, which\nis nothing compared to its millions of craters.\nPits on the Moon range in size from 5-900\nmeters in diameter (16.4-2952.8 feet)!",
                "Most pits on the Moon are located in areas\nthat have solidified lava flows. This is likely\nbecause lava can flow under the surface of\nthe Moon and, after it eventually drains away, it\nleaves a cave. Then, gravity takes course and\ncan cause parts of the cave to collapse."
            },
            // Mare Serenitatis - 3
            new List<string>(){
                "Mare Serenitatis has a gravitational anomaly\nknown as a mascon. This means that while the\nmare is topographically depressed, it contains\nmore gravity than is expected.",
                "'Mare Serenitatis' is Latin for Sea of Serenity\nand is located in the Serenitatis basin.",
                "Mare Serenitatis has more contrast in material\nthan most mares. This contrast is obvious\nin color, tone, and structure."
            },
            // Posidonius - 4
            new List<string>(){
                "This crater was named after the Greek\nphilosopher and geographer Posidonius of\nApamea.",
                "A rille is a long, narrow depression that\nresembles a canal and Posidonius is full of them!\nIt even has its own rille system named the\nRimae Posidonius.",
                "Posidonius has 13 satellite craters!\nSatellite craters are smaller craters near a\nbigger, named crater. For example,\nPosidonius A, B, C, etc. "
            },
            // Montes Taurus - 5
            new List<string>(){
                "This mountain range isn't the most impressive\non the moon but it still reaches heights of\n4.9 km (3 miles) above the Mare Serenitatis.",
                "'Montes Taurus' is named for the Taurus\nMountains in Turkey. This was done in the\n17th century by Johannes Hevelius who\nwas equating regions on the Moon with regions\nnear the Medditerrainian Sea.",
                "The Montes Taurus range a pretty long\ndistance, 170 kilometers (105.63 miles)!"
            },
            // Atlas & Hercules - 6
            new List<string>(){
                "Atlas and Hercules were named for the Greek\nmythological figures of the same names.",
                "Although they look similar, Atlas and Hercules\nwere made at different times, with Hercules\nbeing older. You can tell by Hercules being\nshallower and containing more small craters\nwithin it.",
                "There have been reports of lunar transient\nphenomena (LTP) at Hercules. LTPs are\nchanges of color or appearance on the Moon\nthat appear and disappear quickly.\nScientists aren't sure why this happens."
            },
            // Mare Frigoris - 7
            new List<string>(){
                "The Mare Frigoris, or the 'Sea of Cold', is the\nnorthernmost mare on the Moon and is right\nabove the Mare Serenitatis and Mare Imbrium.",
                "Mare Frigoris has a lower amount of iron and\ntitanium in its basalts than any other mare\ntested so far! This makes it more reflective\nthan other mares. ",
                "The Mare Frigoris has some areas that are so\nreflectant that they are called 'light plains'.\nLight plains can occur from volcanoes erupting,\nimpact basin ejecta filling in depressions,\nor highland material being displaced. "
            },
            // Anaxagoras - 8
            new List<string>(){
                "Anaxagoras is a pretty young crater and still\nhas a ray system that has not been eroded by\nweather. A ray system consists of radial\nstreaks coming from the impact of an asteroid.",
                "Anaxagoras has a relatively high albedo, which\nmeans it reflects most radiation that hits it. A\nhigh albedo also means that it is visible when the\nMoon is full.",
                "The interior walls of Anaxagoras are pretty\nsteep and form a terrace. A terrace consists of\nmultiple flat areas that, all together, form a\nslope. Terraces are usually man-made for\ncultivation but this one is natural."
            }
        };
        private static Random r = new Random();
        private static int choosenMessage = 99;
        private static void DisplayMessage(int Progress)
        {
            List<string> messages = missionControlMessages[Progress];
            if (choosenMessage == 99)
                choosenMessage = r.Next(0, messages.Count);
            string message = messages[choosenMessage];
            //message = missionControlMessages[8][2]; // For debugging, force a specific message.
            string[] messageArray = message.Split("\n");
            Raylib.DrawText("Mission Control:", (Raylib.GetScreenWidth()-225)/2, Raylib.GetScreenHeight()/5, 30, WHITE);
            for (int i = 0;i < messageArray.Length;i++)
            {
                Raylib.DrawText(messageArray[i], Raylib.GetScreenWidth()/32, Raylib.GetScreenHeight()/4+ + 30 +(50*i), 30, WHITE);
            }
        }
    }
}
