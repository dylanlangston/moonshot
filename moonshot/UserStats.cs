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
        public const string veryPoor = "Very Poor";
        public const string dead = "Dead";
    }
    public class PlayerPace
    {
        public const string steady = "Steady";
        public const string strenuous = "Strenuous";
        public const string grueling = "Grueling";
    }

    public class PlayerRations
    {
        public const string filling = "Filling";
        public const string meager = "Meager";
        public const string bareBones = "Bare Bones";
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
        internal OxygenTank(int valueIn = 0) {
            name = "Oxygen Tanks";
            id = 101;
            value = valueIn;
        }
    }
    public class Fuel : InventoryItem 
    {
        internal Fuel(int valueIn = 0) {
            name = "Fuel";
            id = 102;
            value = valueIn;
        }
    }
    public class Food : InventoryItem 
    {
        internal Food(int valueIn = 0) {
            name = "Food";
            id = 103;
            value = valueIn;
        }
    }
    public class Boxes: InventoryItem 
    {
        internal Boxes(int valueIn = 0) {
            name = "Boxes";
            id = 104;
            value = valueIn;
        }
    }
    public class ShipParts : InventoryItem 
    {
        internal ShipParts(int valueIn = 0) {
            name = "Space Ship Parts";
            id = 105;
            value = valueIn;
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

        public void AddItem(int id, int value) {
            foreach (InventoryItem item in Items)
            {
                if (item.id == id)
                    item.value += value;
            }
        }
        public void RemoveItem(int id, int value) {
            foreach (InventoryItem item in Items)
            {
                if (item.id == id)
                    item.value -= value;
                if (item.value < 0)
                    item.value = 0;
            }
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
        public DateTime currentTime = new DateTime();
        public int currentLocation = 0;
        public int milesTraveled = 0;
        public int Money = 0;
        public String playerType = PlayerType.apollo11;
        public PartyMembers crew = new apollo11();
        public Inventory inventory = new Inventory();
        public string status = PlayerStatus.good;
        public string pace = PlayerPace.steady;
        public string rations = PlayerRations.filling;
        public override string ToString()
        {
            return "<Stats><CurrentTime>" + currentTime.ToString() + "</CurrentTime><CurrentLocation>" + currentLocation + "</CurrentLocation><MilesTraveled>" + milesTraveled + "</MilesTraveled><Money>" + Money.ToString() + "</Money><PlayerType>" + playerType + "</PlayerType><Status>" + status + "</Status><Pace>" + pace + "</Pace><Rations>" + rations + "</Rations>" + inventory.ToString() + crew.ToString() + "</Stats>";
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
                        case "CurrentTime":
                            DateTime.TryParse(node.ChildNodes[i].InnerText, out stats.currentTime);
                            break;
                        case "CurrentLocation":
                            Int32.TryParse(node.ChildNodes[i].InnerText, out stats.currentLocation);
                            break;
                        case "MilesTraveled":
                            Int32.TryParse(node.ChildNodes[i].InnerText, out stats.milesTraveled);
                            break;
                        case "Money":
                            Int32.TryParse(node.ChildNodes[i].InnerText, out stats.Money);
                            break;
                        case "PlayerType":
                            stats.playerType = node.ChildNodes[i].InnerText;
                            break;
                        case "Status":
                            stats.status = node.ChildNodes[i].InnerText;
                            break;
                        case "Pace":
                            stats.pace = node.ChildNodes[i].InnerText;
                            break;
                        case "Rations":
                            stats.rations = node.ChildNodes[i].InnerText;
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
