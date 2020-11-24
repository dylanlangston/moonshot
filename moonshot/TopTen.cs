using System;
using System.Collections.Generic;
using System.Xml;

namespace moonshot
{
    public class TopTen
    {
        private List<(string, int)> defaultLeaders = new List<(string, int)>(){
            ("Mia Johnson",7650),
            ("Dylan Langston", 5694),
            ("Neil Armstrong", 5178),
            ("Buzz Aldrin", 4794),
            ("Pete Conrad", 4342),
            ("Alan Bean", 4138),
            ("Alan Shepard", 2945),
            ("Edgar Mitchell", 2152),
            ("David Scott", 2052),
            ("James Irwin", 1401)
        };
        public List<(string, int)> Leaders = new List<(string, int)>(){};
        public TopTen()
        {
            Leaders = defaultLeaders;
        }
        public TopTen(List<(string, int)> leaders)
        {
            Leaders = leaders;
        }
        public override string ToString()
        {
            string output = "<TopTen>";
            foreach ((string, int) leader in Leaders) {
                output += "<Leaders><Name>" + leader.Item1 + "</Name><Score>" + leader.Item2 + "</Score></Leaders>";
            }
            output += "</TopTen>";
            return output;
        }
        public void LoadTopTenFromString(string topTenString) {
            try {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(topTenString);
            List<(string, int)> Items = new List<(string, int)>(){};
            foreach (XmlNode node in doc.ChildNodes)
            {
                string name = "";
                int score = 0;
                for (int i = 0; i < node.ChildNodes.Count; i++)
                {
                    foreach (XmlNode item in node.ChildNodes[i]) {
                        switch (item.Name) {
                            case "Name":
                                name = item.InnerText;
                                break;
                            case "Score":
                                Int32.TryParse(item.InnerText, out score);
                                break;
                            default:
                                Console.WriteLine(item.Name);
                                break;
                        }
                    }
                    Items.Add((name, score));
                }
            }
            Leaders = Items;
            } catch {}
        }
    }
}
