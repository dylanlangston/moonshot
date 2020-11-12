using System;
using System.Collections.Generic;
using System.Xml;

namespace moonshot
{

    // Extension of the settings. This tracks all the user stats through the game.
    // Provides a way to save and load usersettings but converting them to/from XML.

    public class PlayerType
    {
        public const string apollo11 = "Apollo 11";
        public const string apollo12 = "Apollo 12";
        public const string apollo14 = "Apollo 14";

        public static DateTime GetLaunchDate(string playerType){
            switch (playerType){
                default: // Apollo 11
                    return new DateTime(1969, 7, 16);
                case PlayerType.apollo12:
                    return new DateTime(1969, 11, 14);
                case PlayerType.apollo14:
                    return new DateTime(1971, 1, 31);
                
            }
        }
    }
    public class PlayerStatus
    {
        public const string good = "Good";
        public const string fair = "Fair";
        public const string poor = "Poor";
    }
    public class PartyMembers {
        internal List<PartyMember> Party = new List<PartyMember>(){};

        public override string ToString()
        {
            string output = "<PartyMembers>";
            foreach (PartyMember member in this.Party) {
                output += "<Member><Name>" + member.name + "</Name><Status>" + member.status + "</Status></Member>";
            }
            output += "</PartyMembers>";
            return output;
        }
        public void LoadPartyMembersFromString(string partyString, PartyMembers members) {
            members.Party.RemoveRange(0, members.Party.Count);
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(partyString);
            foreach (XmlNode node in doc)
            {
                for (int i = 0; i < node.ChildNodes.Count; i++)
                {
                    string name = "";
                    string status = "";
                    switch (node.ChildNodes[i].Name) {
                        case "Member":
                            name = node.ChildNodes[i].FirstChild.InnerText.ToString();
                            status = node.ChildNodes[i].LastChild.InnerText.ToString();
                            break;
                        default:
                            break;
                    }
                    if (!String.IsNullOrEmpty(name+status))
                        members.Party.Add(new PartyMember(name, status));
                }
            }
        }
    }
    public class PartyMember
    {
        internal PartyMember(string nameIn, string statusIn) {
            name = nameIn;
            status = statusIn;
        }
        internal string name;
        internal string status; 
    }
    public class apollo11 : PartyMembers {
        internal apollo11() {
            Party = new List<PartyMember>()
            {
                new PartyMember("Neil", PlayerStatus.good),
                new PartyMember("Michael", PlayerStatus.good),
                new PartyMember("Edwin (Buzz)", PlayerStatus.good)
            };
        }
    }
    public class apollo12 : PartyMembers {
        internal apollo12() {
            Party = new List<PartyMember>()
            {
                new PartyMember("Charles", PlayerStatus.good),
                new PartyMember("Richard", PlayerStatus.good),
                new PartyMember("Alan", PlayerStatus.good)
            };
        }
    }
    public class apollo14 : PartyMembers {
        internal apollo14() {
            Party = new List<PartyMember>()
            {
                new PartyMember("Alan", PlayerStatus.good),
                new PartyMember("Stuart", PlayerStatus.good),
                new PartyMember("Edgar", PlayerStatus.good)
            };
        }
    }
    public class InventoryItem
    {
        public InventoryItem() { }
        public InventoryItem(string nameIn, int idIn, int valueIn) {
            name = nameIn;
            id = idIn;
            value = valueIn;
        }
        public string name;
        public int id;
        public int value;
    }
    public class OxygenTank : InventoryItem 
    {
        internal OxygenTank() {
            name = "Oxygen Tanks";
            id = 101;
            value = 0;
        }
    }
    public class Fuel : InventoryItem 
    {
        internal Fuel() {
            name = "Fuel";
            id = 102;
            value = 0;
        }
    }
    public class Food : InventoryItem 
    {
        internal Food() {
            name = "Food";
            id = 103;
            value = 0;
        }
    }
    public class Boxes: InventoryItem 
    {
        internal Boxes() {
            name = "Boxes";
            id = 104;
            value = 0;
        }
    }
    public class ShipParts : InventoryItem 
    {
        internal ShipParts() {
            name = "Space Ship Parts";
            id = 105;
            value = 0;
        }

    }
    public class Inventory
    {
        public List<InventoryItem> Items = new List<InventoryItem>(){}; //new List<InventoryItem>(){new InventoryItem("test", 1, 1)};
        public override string ToString()
        {
            string output = "<Items>";
            foreach (InventoryItem item in Items) {
                output += "<Item><Name>" + item.name + "</Name><Id>" + item.id + "</Id><Value>" + item.value + "</Value></Item>";
            }
            output += "</Items>";
            return output;
        }
        public void LoadInventoryFromString(string inventoryString, Inventory inventory) {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(inventoryString);
            List<InventoryItem> Items = new List<InventoryItem>(){};
            foreach (XmlNode node in doc.ChildNodes)
            {
                string name = "";
                int id = 0;
                int value = 0;
                for (int i = 0; i < node.ChildNodes.Count; i++)
                {
                    foreach (XmlNode item in node.ChildNodes[i]) {
                        switch (item.Name) {
                            case "Name":
                                name = item.InnerText;
                                break;
                            case "Id":
                                Int32.TryParse(item.InnerText, out id);
                                break;
                            case "Value":
                                Int32.TryParse(item.InnerText, out value);
                                break;
                            default:
                                Console.WriteLine(item.Name);
                                break;
                        }
                    }
                    Items.Add(new InventoryItem(name, id, value));
                }
            }
            inventory.Items = Items;
        }
    }
    public class UserStats
    {
        public int Money = 0;
        public String playerType = PlayerType.apollo11;
        public PartyMembers crew = new apollo11();
        public Inventory inventory = new Inventory();
        public string status = PlayerStatus.good;
        public override string ToString()
        {
            return "<Stats><Money>" + Money.ToString() + "</Money><PlayerType>" + playerType + "</PlayerType><Status>" + status + "</Status>" + inventory.ToString() + crew.ToString() + "</Stats>";
        }
        public void LoadUserStatsFromString(string userStatsString, UserStats stats)
        {
            try {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(userStatsString);
            foreach (XmlNode node in doc)
            {
                for (int i = 0; i < node.ChildNodes.Count; i++)
                {
                    switch (node.ChildNodes[i].Name) {
                        case "Money":
                            Int32.TryParse(node.ChildNodes[i].InnerText, out stats.Money);
                            break;
                        case "PlayerType":
                            stats.playerType = node.ChildNodes[i].InnerText;
                            break;
                        case "Status":
                            stats.status = node.ChildNodes[i].InnerText;
                            break;
                        case "Items":
                            if (!String.IsNullOrEmpty(node.ChildNodes[i].OuterXml))
                                inventory.LoadInventoryFromString(node.ChildNodes[i].OuterXml, inventory);
                            break;
                        case "PartyMembers":
                            if (!String.IsNullOrEmpty(node.ChildNodes[i].OuterXml))
                                crew.LoadPartyMembersFromString(node.ChildNodes[i].OuterXml, crew);
                            break;
                        default:
                            break;
                    }
                }
            }
            } catch (Exception ex) {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
